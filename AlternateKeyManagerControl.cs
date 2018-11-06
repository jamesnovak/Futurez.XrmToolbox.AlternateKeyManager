using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

using McTools.Xrm.Connection;

using Futurez.XrmToolBox.Controls;

namespace Futurez.XrmToolBox
{
    public partial class AlternateKeyManagerControl : PluginControlBase, IStatusBarMessenger, IGitHubPlugin, IHelpPlugin, IPayPalPlugin
    {
        private Settings _mySettings;

        // List of entities whose Keys have been loaded
        private List<EntityMetadata> _expandedEntities = new List<EntityMetadata>();

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public string HelpUrl => Properties.Resources.github_help_url;

        public string RepositoryName => Properties.Resources.github_repo_name;

        public string UserName => Properties.Resources.github_user;

        public string DonationDescription => Properties.Resources.paypal_message;

        public string EmailAccount => Properties.Resources.paypal_email;

        #region Plugin general methods 
        public AlternateKeyManagerControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Main loading event for the User Control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlternateKeyManagerControl_Load(object sender, EventArgs e)
        {
            // initialize the user control with the connection and parent reference
            EntitiesListControl.Initialize(this, Service);
            EntityDropDown.Initialize(this, Service);

            // udpate some UI elements on load
            UpdateKeysToolbar();
            ToggleNewKeyPane(false);
            ToggleNewKeyPaneEnabled(false);
        }

        /// <summary>
        /// Close the control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ToolButtonClose_Click(object sender, EventArgs args)
        {
            if (MessageBox.Show(this, "Would you like to save your current Entity selection and filter text?", "Confirm Save Settings", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes) {
            }
            SaveSettings();
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), _mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            // update the connection and clear out the related data
            EntitiesListControl.UpdateConnection(Service);
            EntityDropDown.UpdateConnection(Service);
            ClearSelectedEntitiesList();


            if (_mySettings != null && detail != null) {
                _mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        private void ClearSelectedEntitiesList()
        {
            _expandedEntities.Clear();
            PopulateKeysListView();
        }

        #endregion

        #region Load and Retrieve methods
        /// <summary>
        /// Load the Alternate Key info for the selected Entitie 
        /// </summary>
        private void LoadSelectedEntityKeys() {

            ToggleMainUIControlsEnabled(false);
            var selectedEntities = EntitiesListControl.CheckedEntities;

            WorkAsync(new WorkAsyncInfo {
                Message = "Retrieving the list of Entities",
                AsyncArgument = selectedEntities,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150,
                Work = (w, e) => {

                    var entExpanded = new List<EntityMetadata>();
                    // then reload details for each
                    var count = 0;
                    var total = selectedEntities.Count;

                    // build the list of keys that we want to retrieve
                    var entLogicalNameList = new List<string>();
                    foreach (var ent in selectedEntities) {
                        w.ReportProgress(100 * count++ / total, $"Loading Entities: {ent.LogicalName}");
                        entLogicalNameList.Add(ent.LogicalName);
                    }

                    entExpanded = CrmActions.RetrieveEntity(Service, entLogicalNameList);

                    e.Result = entExpanded;

                    w.ReportProgress(100, $"Loading Entities complete");
                },
                ProgressChanged = e => {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
                },
                PostWorkCallBack = e => 
                {
                    _expandedEntities.Clear();
                    _expandedEntities = e.Result as List<EntityMetadata>;

                    // now load up the list view with the keys 
                    PopulateKeysListView();

                    ToggleMainUIControlsEnabled(true);
                }
            });
        }

        /// <summary>
        /// Populate the Alternate Key details pane
        /// </summary>
        private void PopulateNewKeyControls()
        {
            ToggleNewKeyPaneEnabled(false);
            ToggleMainUIControlsEnabled(false);

            // load the entities into the drop down control 
            EntityDropDown.LoadData();

            WorkAsync(new WorkAsyncInfo {
                Message = "Retrieving Publishers",
                AsyncArgument = null,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150,
                Work = (w, e) => {

                    var entLogicalName = e.Argument as string;

                    w.ReportProgress(0, $"Retrieving Publishers");

                    var results = CrmActions.RetrievePublishers(Service);

                    w.ReportProgress(100, $"Retrieving Publishers complete!");

                    e.Result = results;
                },
                ProgressChanged = e => {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
                },
                PostWorkCallBack = e => {
                    var pubs = e.Result as List<Entity>;
                    comboBoxPrefixes.Items.Clear();

                    // load the publishers 
                    foreach (var pub in pubs) {
                        comboBoxPrefixes.Items.Add(pub.Attributes["customizationprefix"]);
                    }
                }
            });
        }

        /// <summary>
        /// Retrieve the Attribute metadata for the given EntityLogicalName
        /// </summary>
        /// <param name="entityLogicalName"></param>
        private void RetrieveEntityAttributes(string entityLogicalName)
        {
            // lock thing down
            ToggleNewKeyPaneEnabled(false);

            WorkAsync(new WorkAsyncInfo {
                Message = "Retriveing Entity Attributes",
                AsyncArgument = entityLogicalName,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150,
                Work = (w, e) => {

                    var entLogicalName = e.Argument as string;

                    w.ReportProgress(0, $"Retriveing Entity Attributes");

                    var results = CrmActions.RetrieveEntity(Service, entLogicalName, true);

                    w.ReportProgress(100, $"Retriveing Entity Attributes complete!");

                    e.Result = results;
                },
                ProgressChanged = e => {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
                },
                PostWorkCallBack = e => {

                    var entity = e.Result as EntityMetadata;
                    PopulateEntityAttributes(entity);
                }
            });
        }


        /// <summary>
        /// Load the Entities into the list view, binding the columns based on the control properties
        /// </summary>
        private void PopulateKeysListView()
        {
            ListViewKeyList.Items.Clear();
            ListViewKeyList.Refresh();
            ListViewKeyList.Groups.Clear();
            ListViewKeyList.SuspendLayout();
            ListViewKeyList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            if (_expandedEntities.Count > 0) {
                // persist the list of list view items for the filtering
                var lvItemsColl = new List<ListViewItem>();
                var entityCount = 0;
                foreach (var entity in _expandedEntities) {
                    if (entity.Keys.Length == 0)
                        continue;

                    // compact mode uses display name and schema name
                    var labels = entity.DisplayName.LocalizedLabels;
                    var entityName = (labels.Count > 0) ? labels[0].Label : entity.SchemaName;

                    var grp = new ListViewGroup(entity.LogicalName, entityName);
                    ListViewKeyList.Groups.Add(grp);

                    foreach (var key in entity.Keys) {
                        labels = key.DisplayName.LocalizedLabels;
                        var keyName = (labels.Count > 0) ? labels[0].Label : key.SchemaName;

                        var lvItem = new ListViewItem() {
                            Name = "KeyName",
                            ImageIndex = 0,
                            StateImageIndex = 0,
                            Text = keyName,
                            Tag = key,
                            Group = grp,
                            BackColor = (key.EntityKeyIndexStatus == EntityKeyIndexStatus.Failed) ? SystemColors.Highlight : SystemColors.Window
                        };
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, key.EntityKeyIndexStatus.ToString()) { Tag = "Status", Name = "Status" });
                        lvItem.SubItems.Add(new ListViewItem.ListViewSubItem(lvItem, string.Join(",", key.KeyAttributes)) { Tag = "Attributes", Name = "Attributes" });

                        lvItemsColl.Add(lvItem);
                    }

                    toolLabelSummary.Text = $"Loaded Entities: {++entityCount}, Total Keys: {lvItemsColl.Count}";
                }


                ListViewKeyList.Items.AddRange(lvItemsColl.ToArray<ListViewItem>());
                ListViewKeyList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            ListViewKeyList.ResumeLayout();
        }

        #endregion

        /// <summary>
        /// Batch update those failed keys!
        /// </summary>
        /// <param name="keys"></param>
        private void ReactivateKeys(List<EntityKeyMetadata> keys)
        {
            WorkAsync(new WorkAsyncInfo {
                Message = "Reactivating Entity Keys",
                AsyncArgument = keys,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150,
                Work = (w, e) => 
                {
                    w.ReportProgress(0, $"Reactivating Entity Keys");

                    var results = CrmActions.ReactivateEntityKey(Service, keys);

                    w.ReportProgress(100, $"Reactivating Entity Keys complete!");
                    e.Result = results;

                },
                ProgressChanged = e => {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
                },
                PostWorkCallBack = e => {
                    // reload the list of selected entities
                    if (e.Result != null) 
                    {
                        var errors = (List<string>)e.Result;
                        MessageBox.Show(this, $"The following errors occurred while attempting to activate\n{string.Join("\n", errors)}");
                    }

                    // reload the list now that have made the call
                    LoadSelectedEntityKeys();
                }
            });
        }

        /// <summary>
        /// Load the entity attributes into the list 
        /// </summary>
        private void PopulateEntityAttributes() {
            PopulateEntityAttributes(listBoxAttrbutes.Tag as EntityMetadata);
        }
        /// <summary>
        /// Load the list of attributes that are allowed to be included as keys
        /// </summary>
        /// <param name="entity"></param>
        private void PopulateEntityAttributes(EntityMetadata entity)
        {
            // save the entity reference so we can do some validation. 
            listBoxAttrbutes.Tag = entity;

            listBoxAttrbutes.Items.Clear();
            listBoxAttrbutes.DisplayMember = "Name";

            var attributes = entity.Attributes.ToList();

            if (attributes != null) {

                // only allowed certain types: 
                // https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/define-alternate-keys-entity
                var allowed = attributes.Where(a =>
                        (a is DecimalAttributeMetadata) 
                        || (a is IntegerAttributeMetadata) 
                        || (a is StringAttributeMetadata) 
                        // || (a is DateTimeAttributeMetadata) // these will be available in v9.1
                        // || (a is LookupAttributeMetadata) 
                        // ||(a is PicklistAttributeMetadata)
                        );

                // add the items to the checked list 
                foreach (var attrib in allowed) 
                {
                    if ((attrib.AttributeOf == null) && (attrib.AttributeType != AttributeTypeCode.Virtual)) {

                        listBoxAttrbutes.Items.Add(new AttributeListItem() {
                                Name = $"{CrmActions.GetLocalizedLabel(attrib.DisplayName, attrib.SchemaName)} ({attrib.SchemaName})",
                                SchemaName = attrib.SchemaName
                            });
                    }
                }        
            }

            // reenable 
            ToggleNewKeyPaneEnabled(true);

            ValidateNewKeyInputs(true);
        }

        #region Control events

        /// <summary>
        /// Load the saved settings 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonLoadSettings_Click(object sender, EventArgs e)
        {
            ApplySavedSettings();
        }

        /// <summary>
        /// Save the current selection settings to the common settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolButtonSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        /// <summary>
        /// Cancel the New Alternate Key action 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelNew_Click(object sender, EventArgs e)
        {
            ToggleNewKeyPane(false);
            ToggleNewKeyPaneEnabled(false, true);
        }

        /// <summary>
        /// Handle the event when the list of Checked Items has changed, update the available actions from the toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntitiesListControl_CheckedItemsChanged(object sender, EventArgs e)
        {
            UpdateKeysToolbar();
        }

        /// <summary>
        /// Handle the event when the list of Checked Items has changed, update the available actions from the toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntitiesListControl_SelectedItemChanged(object sender, EntitiesListControl.SelectedItemChangedEventArgs e)
        {
            UpdateKeysToolbar();
        }

        /// <summary>
        /// Now that the data has been loaded in the Entities List control, apply the saved settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntitiesListControl_LoadDataComplete(object sender, EventArgs e)
        {
            // load the settings.
            ToggleMainUIControlsEnabled(true);

            ClearSelectedEntitiesList();

            ApplySavedSettings();
        }

        /// <summary>
        /// Load the Alternate Keys for the list of selected Entities
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonLoadSelected_Click(object sender, EventArgs e)
        {
            LoadSelectedEntityKeys();
        }

        /// <summary>
        /// Update the other UI elements now that the selected Alternate Key has changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewKeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityKeyMetadata key = null;

            UpdateKeysToolbar();

            var selected = ListViewKeyList.SelectedItems;

            if (selected.Count == 1) {
                key = selected[0].Tag as EntityKeyMetadata;
            }

            UpdateKeyDetailsPane(key);
        }

        /// <summary>
        /// Activate the FAILED Alternate Keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonActivate_Click(object sender, EventArgs e)
        {
            // get the selected item from the list 
            var items = ListViewKeyList.SelectedItems;
            var failedKeys = new List<EntityKeyMetadata>();
            var approveMessage = new StringBuilder();

            foreach (ListViewItem item in items) {
                var key = item.Tag as EntityKeyMetadata;
                if (key.EntityKeyIndexStatus == EntityKeyIndexStatus.Failed) {
                    var keyName = CrmActions.GetLocalizedLabel(key.DisplayName, key.SchemaName);
                    failedKeys.Add(key);
                    approveMessage.AppendLine($"{keyName}");
                }
            }

            if (failedKeys.Count == 0) {
                MessageBox.Show(this, $"No Failed Keys Selected. Activate is only supported for Failed Entity Keys", "No Failed Keys Selected", MessageBoxButtons.OK);
            }
            else {
                if (MessageBox.Show(this, $"Activate the following keys?\n{approveMessage.ToString()}", "Reactivate Key?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    ReactivateKeys(failedKeys);
                }
            }
        }

        /// <summary>
        /// Kick off the process to crate a new Alternate Key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ToolButtonNew_Click(object sender, EventArgs args)
        {
            if (EntityDropDown.AllEntities.Count == 0) {
                PopulateNewKeyControls();
            }
            else {

                PopulateEntityAttributes();

                ToggleNewKeyPaneEnabled(true);
                ToggleMainUIControlsEnabled(true);
            }

            ToggleNewKeyPane(true);
        }

        /// <summary>
        /// Delete selected Keys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonDelete_Click(object sender, EventArgs e)
        {
            // get the selected item from the list 
            var items = ListViewKeyList.SelectedItems;
            var approveMessage = new StringBuilder();
            var keys = new List<EntityKeyMetadata>();

            foreach (ListViewItem item in items) {
                var key = item.Tag as EntityKeyMetadata;
                var keyName = CrmActions.GetLocalizedLabel(key.DisplayName, key.SchemaName);
                keys.Add(key);
                approveMessage.AppendLine($"\t{keyName}");
            }

            var message = $"Are you sure you would like to delete Alternate Key(s)?\n{approveMessage.ToString()}?" +
                $"\nNOTE: This cannot be undone!";

            if (MessageBox.Show(this, message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                CrmActions.DeleteEntityKey(Service, keys);

                LoadSelectedEntityKeys();
            }
        }

        /// <summary>
        /// Load of the Entities list is complete, so update the UI accordingly 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityDropDown_LoadDataComplete(object sender, EventArgs e)
        {
            ToggleMainUIControlsEnabled(true);
        }

        /// <summary>
        /// Selected entity changed, so update the Attributes list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityDropDown_SelectedItemChanged(object sender, EntitiesDropdownControl.SelectedItemChangedEventArgs e)
        {
            if (e.Entity != null) {
                RetrieveEntityAttributes(e.Entity.LogicalName);
            }
        }

        /// <summary>
        /// Save the new key!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveNew_Click(object sender, EventArgs e)
        {
            ValidateNewKeyInputs(false);
            if (!AllowSaveNewKey())
                return;

            if (MessageBox.Show(this, "Create new Alternate Key?", "Confirm Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                var ent = EntityDropDown.SelectedEntity;
                var attributes = new List<string>();
                // grab the list of schema names from the list box
                foreach (var item in listBoxAttrbutes.SelectedItems) {
                    attributes.Add(item.ToString().ToLower());
                }

                var newName = $"{comboBoxPrefixes.SelectedItem}_{textNewKeyName.Text}";

                var errors = CrmActions.CreateEntityKey(Service, ent.LogicalName, newName, textNewKeyDisplayName.Text, attributes);

                if (errors != null) {
                    MessageBox.Show(this, errors, "Error Creating Key", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ToggleNewKeyPane(false);
                ToggleNewKeyPaneEnabled(false, true);

                LoadSelectedEntityKeys();
            }
        }

        /// <summary>
        /// Shared method for edit controls on change events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_ValueChanged(object sender, EventArgs e)
        {
            ValidateNewKeyInputs(false);
        }
        #endregion

        #region Helpers

        /// <summary>
        /// Load your saved settings 
        /// </summary>
        private void LoadSettings()
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out _mySettings)) {
                _mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else {
                LogInfo("Settings found and loaded");
            }
        }

        /// <summary>
        /// Save current Selection, filter string, sort to your settings
        /// </summary>
        private void SaveSettings()
        {
            _mySettings.CheckedEntityNames = EntitiesListControl.CheckedEntities.Select(e => e.SchemaName.ToLower()).ToList();
            _mySettings.EntityListFilter = EntitiesListControl.ListFilterString;
            _mySettings.ListSortColumn = EntitiesListControl.ListSortColumn;
            _mySettings.ListSortOrder = EntitiesListControl.ListSortOrder;

            SettingsManager.Instance.Save(GetType(), _mySettings);
        }

        /// <summary>
        /// Apply the saved settings 
        /// </summary>
        private void ApplySavedSettings()
        {
            LoadSettings();

            EntitiesListControl.CheckEntities(_mySettings.CheckedEntityNames);
            EntitiesListControl.FilterEntitiesList(_mySettings.EntityListFilter);
            EntitiesListControl.SortEntitiesList(_mySettings.ListSortColumn, _mySettings.ListSortOrder);
        }

        /// <summary>
        /// Helper class for the list of attributes
        /// </summary>
        private class AttributeListItem
        {
            public string Name { get; set; }
            public string SchemaName { get; set; }

            public override string ToString()
            {
                return SchemaName.ToLower();
            }
        }
        #endregion

        #region UI Helper methods 

        /// <summary>
        /// Check to see if it's ok to Save the new Alternate Key
        /// </summary>
        /// <returns></returns>
        private bool AllowSaveNewKey()
        {
            bool allowSave = false;

            allowSave = string.IsNullOrEmpty(errorProviderNewKey.GetError(textNewKeyDisplayName)) &&
                        string.IsNullOrEmpty(errorProviderNewKey.GetError(textNewKeyName)) &&
                        string.IsNullOrEmpty(errorProviderNewKey.GetError(listBoxAttrbutes)) &&
                        string.IsNullOrEmpty(errorProviderNewKey.GetError(comboBoxPrefixes)) &&
                        string.IsNullOrEmpty(errorProviderNewKey.GetError(EntityDropDown));

            buttonSaveNew.Enabled = allowSave;

            return allowSave;
        }

        /// <summary>
        /// Validate the new Alternate Key inputs before saving
        /// </summary>
        /// <param name="entityOnly"></param>
        private void ValidateNewKeyInputs(bool entityOnly)
        {
            errorProviderNewKey.Clear();

            if (!entityOnly) {
                if (string.IsNullOrEmpty(textNewKeyName.Text)) {
                    errorProviderNewKey.SetError(textNewKeyName, "Name is required");
                }

                if (string.IsNullOrEmpty(textNewKeyDisplayName.Text)) {
                    errorProviderNewKey.SetError(textNewKeyDisplayName, "Display Name is required");
                }

                if (string.IsNullOrEmpty(comboBoxPrefixes.SelectedItem as string)) {
                    errorProviderNewKey.SetError(comboBoxPrefixes, "Prefix is required");
                }

                if (listBoxAttrbutes.SelectedItems.Count == 0) {
                    errorProviderNewKey.SetError(listBoxAttrbutes, "Select at least one Attribute for the key");
                }
            }

            var entity = listBoxAttrbutes.Tag as EntityMetadata;

            if (entity != null) {
                if (entity.Keys.Length == 5) {
                    errorProviderNewKey.SetError(EntityDropDown, "Maximum number of keys set for the Entity");
                }
            }

            AllowSaveNewKey();
        }

        /// <summary>
        /// Helper method to set the button states on the toolbar based on UIL selections 
        /// </summary>
        private void UpdateKeysToolbar()
        {

            var selectedCount = EntitiesListControl.CheckedEntities.Count;
            bool enable = selectedCount > 0;
            toolButtonLoadSelected.Enabled = enable;

            selectedCount = ListViewKeyList.SelectedIndices.Count;
            toolButtonActivate.Enabled = selectedCount > 0;
            toolButtonDelete.Enabled = selectedCount > 0;
        }

        /// <summary>
        /// Populate the Key Details pane controls with the Key Info
        /// </summary>
        /// <param name="key"></param>
        private void UpdateKeyDetailsPane(EntityKeyMetadata key)
        {
            labelKeyLogicalNameValue.Text = null;
            labelKeyIsManagedValue.Text = null;
            labelKeyMetadataIdValue.Text = null;
            labelKeySchemaNameValue.Text = null;
            labelKeyNameValue.Text = null;
            labelKeyStatusValue.Text = null;
            labelScheduledJobValue.Text = null;

            if (key != null) {
                labelKeyNameValue.Text = CrmActions.GetLocalizedLabel(key.DisplayName, key.SchemaName);
                labelKeyLogicalNameValue.Text = key.LogicalName;
                labelKeySchemaNameValue.Text = key.SchemaName;
                labelKeyStatusValue.Text = key.EntityKeyIndexStatus.ToString();
                labelKeyIsManagedValue.Text = key.IsManaged.Value.ToString();
                labelKeyMetadataIdValue.Text = key.MetadataId.Value.ToString("b");
                labelScheduledJobValue.Text = (key.AsyncJob != null) ? ((EntityReference)key.AsyncJob).Name :  null;
            }
        }

        /// <summary>
        /// Toggle the display of the New Alternate Key pane
        /// </summary>
        /// <param name="showNewPane"></param>
        private void ToggleNewKeyPane(bool showNewPane)
        {
            if (showNewPane) {
                panelKeyDetails.SendToBack();
                panelNewKey.BringToFront();
            }
            else {
                panelKeyDetails.BringToFront();
                panelNewKey.SendToBack();
            }
        }

        /// <summary>
        /// Helper method to lock down the new Key Pane when stuff loads
        /// </summary>
        /// <param name="enabled"></param>
        private void ToggleNewKeyPaneEnabled(bool enabled, bool clearAttribs = false)
        {
            textNewKeyDisplayName.Enabled = enabled;
            textNewKeyName.Enabled = enabled;
            listBoxAttrbutes.Enabled = enabled;
            buttonCancelNew.Enabled = enabled;

            buttonSaveNew.Enabled = enabled;
            EntityDropDown.Enabled = enabled;

            if (clearAttribs) {
                listBoxAttrbutes.Items.Clear();
            }
        }

        /// <summary>
        /// Toggle the main UI controls while data loads
        /// </summary>
        /// <param name="enable"></param>
        private void ToggleMainUIControlsEnabled(bool enable)
        {
            ToggleNewKeyPaneEnabled(enable);
            UpdateKeysToolbar();
            ListViewKeyList.Enabled = enable;
            EntitiesListControl.Enabled = enable;
        }
        #endregion
    }
}