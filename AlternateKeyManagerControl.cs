using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;

using McTools.Xrm.Connection;

using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.Args;

using Futurez.XrmToolbox.Controls;

namespace Fururez.XrmToolbox.KeyChecker
{
    public partial class AlternateKeyManagerControl : PluginControlBase, IStatusBarMessenger
    {
        private Settings _mySettings;

        // List of entities whose Keys have been loaded
        private List<EntityMetadata> _expandedEntities = new List<EntityMetadata>();
        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        // private delegate void PopulateEntityAttributesDelegate(List<AttributeMetadata> attributes);

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

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out _mySettings)) {
                _mySettings = new Settings();
                LogWarning("Settings not found => a new settings file has been created!");
            }
            else {
                LogInfo("Settings found and loaded");
            }
        }

        private void ToolButtonClose_Click(object sender, EventArgs args)
        {
            _mySettings.CheckedEntityNames = EntitiesListControl.CheckedEntities.Select(e => e.SchemaName).ToList();
            _mySettings.EntityListFilter = EntitiesListControl.ListFilterString;

            SaveSettings();
            CloseTool();
        }

        private void SaveSettings()
        {
            SettingsManager.Instance.Save(GetType(), _mySettings);
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

            if (_mySettings != null && detail != null) {
                _mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        #endregion

        /// <summary>
        /// Load the Alternate Key info for the selected Entitie 
        /// </summary>
        private void LoadSelectedEntityKeys() {

            ToggleMainUIControls(false);
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

                    ToggleMainUIControls(true);
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

            if (_expandedEntities != null) 
            {
                // persist the list of list view items for the filtering
                var lvItemsColl = new List<ListViewItem>();

                foreach (var entity in _expandedEntities) 
                {
                    if (entity.Keys.Length == 0)
                        continue;

                    // compact mode uses display name and schema name
                    var labels = entity.DisplayName.LocalizedLabels;
                    var entityName = (labels.Count > 0) ? labels[0].Label : entity.SchemaName;

                    var grp = new ListViewGroup(entity.LogicalName, entityName);
                    ListViewKeyList.Groups.Add(grp);

                    foreach (var key in entity.Keys) 
                    {
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
                }
                ListViewKeyList.Items.AddRange(lvItemsColl.ToArray<ListViewItem>());
                ListViewKeyList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            ListViewKeyList.ResumeLayout();
        }

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
                    // BeginInvoke(new Action(LoadSelectedEntityKeys));
                    LoadSelectedEntityKeys();
                }
            });
        }

        /// <summary>
        /// Helper method to set the button states on the toolbar based on UIL selections 
        /// </summary>
        private void UpdateKeysToolbar() {

            var selectedCount = EntitiesListControl.CheckedEntities.Count;
            bool enable = selectedCount > 0;
            toolButtonLoadSelected.Enabled = enable;

            selectedCount = ListViewKeyList.SelectedIndices.Count;
            toolButtonActivate.Enabled = selectedCount > 0;
            toolButtonDelete.Enabled = selectedCount == 1;
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

            if (key != null) {
                labelKeyNameValue.Text = CrmActions.GetLocalizedLabel(key.DisplayName, key.SchemaName);
                labelKeyLogicalNameValue.Text = key.LogicalName;
                labelKeySchemaNameValue.Text = key.SchemaName;
                labelKeyStatusValue.Text = key.EntityKeyIndexStatus.ToString();
                labelKeyIsManagedValue.Text = key.IsManaged.Value.ToString();
                labelKeyMetadataIdValue.Text = key.MetadataId.Value.ToString("b");
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
        private void ToggleNewKeyPaneEnabled(bool enabled)
        {
            textNewKeyDisplayName.Enabled = enabled;
            textNewKeyName.Enabled = enabled;
            listBoxAttrbutes.Enabled = enabled;
            buttonSaveNew.Enabled = enabled;
            buttonCancelNew.Enabled = enabled;
            EntityDropDown.Enabled = enabled;
        }

        private void ToggleMainUIControls(bool enable) {

            ToggleNewKeyPane(enable);
            UpdateKeysToolbar();
            ListViewKeyList.Enabled = enable;
            EntitiesListControl.Enabled = enable;
        }

        /// <summary>
        /// Load the list of attributes that are allowed to be included as keys
        /// </summary>
        /// <param name="attributes"></param>
        private void PopulateEntityAttributes(List<AttributeMetadata> attributes)
        {
            listBoxAttrbutes.Items.Clear();
            listBoxAttrbutes.DisplayMember = "Name";

            if (attributes != null) {

                // MessageBox.Show(attributes.Count.ToString());
                // only allowed certain types 
                // https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/define-alternate-keys-entity
                var allowed = attributes.Where(a =>
                        (a is DecimalAttributeMetadata) 
                        || (a is IntegerAttributeMetadata) 
                        || (a is StringAttributeMetadata) 
                        // || (a is DateTimeAttributeMetadata) // these blow up!
                        // || (a is LookupAttributeMetadata) 
                        // ||(a is PicklistAttributeMetadata)
                        );

                // MessageBox.Show(allowed.ToList().Count.ToString());
                // add the items to the checked list 

                foreach (var attrib in allowed) 
                {
                    if ((attrib.AttributeOf == null) && (attrib.AttributeType != AttributeTypeCode.Virtual)) {

                        listBoxAttrbutes.Items.Add(new CheckedItem() {
                                Name = $"{CrmActions.GetLocalizedLabel(attrib.DisplayName, attrib.SchemaName)} ({attrib.SchemaName})",
                                SchemaName = attrib.SchemaName
                            });
                    }
                }                
            }
        }

        /// <summary>
        /// Retrieve the Attribute metadata for the given EntityLogicalName
        /// </summary>
        /// <param name="entityLogicalName"></param>
        private void RetrieveEntityAttributes(string entityLogicalName)
        {
            WorkAsync(new WorkAsyncInfo {
                Message = "Retriveing Entity Attributes",
                AsyncArgument = entityLogicalName,
                IsCancelable = true,
                MessageWidth = 340,
                MessageHeight = 150,
                Work = (w, e) => {

                    var entLogicalName = e.Argument as string;

                    w.ReportProgress(0, $"Retriveing Entity Attributes");

                    var results = CrmActions.RetrieveEntityAttributes(Service, entLogicalName);

                    w.ReportProgress(100, $"Retriveing Entity Attributes complete!");

                    e.Result = results;
                },
                ProgressChanged = e => {
                    SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(e.ProgressPercentage, e.UserState.ToString()));
                },
                PostWorkCallBack = e => {
                    var attributes = e.Result as List<AttributeMetadata>;

                    PopulateEntityAttributes(attributes);
                }
            });
        }
        #region Control events

        private void ButtonCancelNew_Click(object sender, EventArgs e)
        {
            ToggleNewKeyPane(false);
            ToggleNewKeyPaneEnabled(false);
        }

        private void EntitiesListControl_CheckedItemsChanged(object sender, EventArgs e)
        {
            UpdateKeysToolbar();
        }

        private void EntitiesListControl_SelectedItemChanged(object sender, EntitiesListControl.SelectedItemChangedEventArgs e)
        {
            UpdateKeysToolbar();
        }

        private void EntitiesListControl_LoadDataComplete(object sender, EventArgs e)
        {
            // load the settings.
            EntitiesListControl.CheckEntities(_mySettings.CheckedEntityNames);
            EntitiesListControl.FilterEntitiesList(_mySettings.EntityListFilter);

            ToggleMainUIControls(true);

        }
        private void ToolButtonLoadSelected_Click(object sender, EventArgs e)
        {
            LoadSelectedEntityKeys();
        }

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
        private void ToolButtonNew_Click(object sender, EventArgs args)
        {
            if (EntityDropDown.AllEntities.Count == 0) {

                ToggleNewKeyPaneEnabled(false);
                ToggleMainUIControls(false);

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

                        ToggleNewKeyPaneEnabled(true);
                        ToggleMainUIControls(true);
                    }
                });
            }
            else {
                ToggleNewKeyPaneEnabled(true);
                ToggleMainUIControls(true);
            }

            ToggleNewKeyPane(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolButtonDelete_Click(object sender, EventArgs e)
        {
            var selected = ListViewKeyList.SelectedItems;
            var key = selected[0].Tag as EntityKeyMetadata;
            var message = $"Are you sure you would like to delete Alternate Key {CrmActions.GetLocalizedLabel(key.DisplayName, key.SchemaName)}?" +
                $"\nNOTE: This cannot be undone!";
            if (MessageBox.Show(this, message, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                CrmActions.DeleteEntityKey(Service, key);

                LoadSelectedEntityKeys();
            }
        }

        private void EntityDropDown_LoadDataComplete(object sender, EventArgs e)
        {
            // update the New Key panel
            ToggleNewKeyPaneEnabled(true);
        }

        private void EntityDropDown_SelectedItemChanged(object sender, EntitiesDropdownControl.SelectedItemChangedEventArgs e)
        {
            if (e.Entity != null) {
                RetrieveEntityAttributes(e.Entity.LogicalName);
            }
        }

        private void buttonSaveNew_Click(object sender, EventArgs e)
        {
            // TODO validate!
            // textNewKeyName.Text -- length > 0
            // textNewKeyDisplayName.Text -- length > 0
            // listBoxAttrbutes -- sel items > 0
            if (MessageBox.Show(this, "Create new Alternate Key?", "Confirm Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {

                var ent = EntityDropDown.SelectedEntity;
                var attributes = new List<string>();

                foreach (var item in listBoxAttrbutes.SelectedItems) {
                    attributes.Add(((CheckedItem)item).SchemaName.ToLower());
                }

                var newName = $"{comboBoxPrefixes.SelectedItem}_{textNewKeyName.Text}";

                CrmActions.CreateEntityKey(Service, ent.LogicalName, newName, textNewKeyDisplayName.Text, attributes);

                LoadSelectedEntityKeys();
            }
        }
        #endregion

        #region Helpers

        private class CheckedItem
        {
            public string Name { get; set; }
            public string SchemaName { get; set; }
        }
        #endregion

    }
}