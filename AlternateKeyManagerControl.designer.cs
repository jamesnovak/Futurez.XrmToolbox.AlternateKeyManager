namespace Fururez.XrmToolbox.KeyChecker
{
    partial class AlternateKeyManagerControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlternateKeyManagerControl));
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolButtonClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitPanelsMain = new System.Windows.Forms.SplitContainer();
            this.EntitiesListControl = new Futurez.XrmToolbox.Controls.EntitiesListControl();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.ListViewKeyList = new System.Windows.Forms.ListView();
            this.colHeadKeyName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadState = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeadAttribs = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelEditPane = new System.Windows.Forms.Panel();
            this.panelNewKey = new System.Windows.Forms.FlowLayoutPanel();
            this.panelNewSaveCancel = new System.Windows.Forms.Panel();
            this.buttonCancelNew = new System.Windows.Forms.Button();
            this.buttonSaveNew = new System.Windows.Forms.Button();
            this.labelNewLoadEntities = new System.Windows.Forms.Label();
            this.EntityDropDown = new Futurez.XrmToolbox.Controls.EntitiesDropdownControl();
            this.labelNames = new System.Windows.Forms.Label();
            this.labelNewName = new System.Windows.Forms.Label();
            this.comboBoxPrefixes = new System.Windows.Forms.ComboBox();
            this.textNewKeyName = new System.Windows.Forms.TextBox();
            this.labelNewDisplayName = new System.Windows.Forms.Label();
            this.textNewKeyDisplayName = new System.Windows.Forms.TextBox();
            this.labelNewSelectAttribs = new System.Windows.Forms.Label();
            this.listBoxAttrbutes = new System.Windows.Forms.ListBox();
            this.panelKeyDetails = new System.Windows.Forms.FlowLayoutPanel();
            this.labelKeyName = new System.Windows.Forms.Label();
            this.labelKeyNameValue = new System.Windows.Forms.Label();
            this.labelKeyStatus = new System.Windows.Forms.Label();
            this.labelKeyStatusValue = new System.Windows.Forms.Label();
            this.labelKeyIsManaged = new System.Windows.Forms.Label();
            this.labelKeyIsManagedValue = new System.Windows.Forms.Label();
            this.labelKeyLogicalName = new System.Windows.Forms.Label();
            this.labelKeyLogicalNameValue = new System.Windows.Forms.Label();
            this.labelKeySchemaName = new System.Windows.Forms.Label();
            this.labelKeySchemaNameValue = new System.Windows.Forms.Label();
            this.labelKeyMetadataId = new System.Windows.Forms.Label();
            this.labelKeyMetadataIdValue = new System.Windows.Forms.Label();
            this.toolStripKeysCommands = new System.Windows.Forms.ToolStrip();
            this.toolButtonLoadSelected = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolButtonActivate = new System.Windows.Forms.ToolStripButton();
            this.toolButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolButtonNew = new System.Windows.Forms.ToolStripButton();
            this.labelUnderscore = new System.Windows.Forms.Label();
            this.toolStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelsMain)).BeginInit();
            this.splitPanelsMain.Panel1.SuspendLayout();
            this.splitPanelsMain.Panel2.SuspendLayout();
            this.splitPanelsMain.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            this.panelEditPane.SuspendLayout();
            this.panelNewKey.SuspendLayout();
            this.panelNewSaveCancel.SuspendLayout();
            this.panelKeyDetails.SuspendLayout();
            this.toolStripKeysCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMain
            // 
            this.toolStripMain.AutoSize = false;
            this.toolStripMain.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonClose,
            this.toolStripSeparator1});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1003, 25);
            this.toolStripMain.TabIndex = 7;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolButtonClose
            // 
            this.toolButtonClose.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonClose.Image")));
            this.toolButtonClose.Name = "toolButtonClose";
            this.toolButtonClose.Size = new System.Drawing.Size(95, 22);
            this.toolButtonClose.Text = "Close Plugin";
            this.toolButtonClose.Click += new System.EventHandler(this.ToolButtonClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // splitPanelsMain
            // 
            this.splitPanelsMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanelsMain.Location = new System.Drawing.Point(0, 25);
            this.splitPanelsMain.Name = "splitPanelsMain";
            // 
            // splitPanelsMain.Panel1
            // 
            this.splitPanelsMain.Panel1.Controls.Add(this.EntitiesListControl);
            // 
            // splitPanelsMain.Panel2
            // 
            this.splitPanelsMain.Panel2.Controls.Add(this.tableLayoutMain);
            this.splitPanelsMain.Size = new System.Drawing.Size(1003, 626);
            this.splitPanelsMain.SplitterDistance = 333;
            this.splitPanelsMain.TabIndex = 9;
            // 
            // EntitiesListControl
            // 
            this.EntitiesListControl.Checkboxes = true;
            this.EntitiesListControl.ColumnDisplayMode = Futurez.XrmToolbox.Controls.ListViewColumnDisplayMode.Compact;
            this.EntitiesListControl.DisplayToolbar = true;
            this.EntitiesListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EntitiesListControl.EntityTypes = Futurez.XrmToolbox.Controls.EnumEntityTypes.BothCustomAndSystem;
            this.EntitiesListControl.GroupByType = true;
            this.EntitiesListControl.Location = new System.Drawing.Point(0, 0);
            this.EntitiesListControl.Name = "EntitiesListControl";
            this.EntitiesListControl.Padding = new System.Windows.Forms.Padding(3);
            this.EntitiesListControl.RetrieveAsIfPublished = true;
            this.EntitiesListControl.Size = new System.Drawing.Size(333, 626);
            this.EntitiesListControl.TabIndex = 15;
            this.EntitiesListControl.LoadDataComplete += new System.EventHandler(this.EntitiesListControl_LoadDataComplete);
            this.EntitiesListControl.SelectedItemChanged += new System.EventHandler<Futurez.XrmToolbox.Controls.EntitiesListControl.SelectedItemChangedEventArgs>(this.EntitiesListControl_SelectedItemChanged);
            this.EntitiesListControl.CheckedItemsChanged += new System.EventHandler(this.EntitiesListControl_CheckedItemsChanged);
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 3;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutMain.Controls.Add(this.ListViewKeyList, 1, 1);
            this.tableLayoutMain.Controls.Add(this.panelEditPane, 2, 1);
            this.tableLayoutMain.Controls.Add(this.toolStripKeysCommands, 1, 0);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 461F));
            this.tableLayoutMain.Size = new System.Drawing.Size(666, 626);
            this.tableLayoutMain.TabIndex = 9;
            // 
            // ListViewKeyList
            // 
            this.ListViewKeyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHeadKeyName,
            this.colHeadState,
            this.colHeadAttribs});
            this.ListViewKeyList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewKeyList.FullRowSelect = true;
            this.ListViewKeyList.Location = new System.Drawing.Point(3, 39);
            this.ListViewKeyList.Name = "ListViewKeyList";
            this.ListViewKeyList.Size = new System.Drawing.Size(310, 584);
            this.ListViewKeyList.TabIndex = 21;
            this.ListViewKeyList.UseCompatibleStateImageBehavior = false;
            this.ListViewKeyList.View = System.Windows.Forms.View.Details;
            this.ListViewKeyList.SelectedIndexChanged += new System.EventHandler(this.ListViewKeyList_SelectedIndexChanged);
            // 
            // colHeadKeyName
            // 
            this.colHeadKeyName.Text = "Key Name";
            this.colHeadKeyName.Width = 150;
            // 
            // colHeadState
            // 
            this.colHeadState.Text = "Status";
            this.colHeadState.Width = 100;
            // 
            // colHeadAttribs
            // 
            this.colHeadAttribs.Text = "Attributes";
            this.colHeadAttribs.Width = 250;
            // 
            // panelEditPane
            // 
            this.panelEditPane.Controls.Add(this.panelNewKey);
            this.panelEditPane.Controls.Add(this.panelKeyDetails);
            this.panelEditPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditPane.Location = new System.Drawing.Point(319, 39);
            this.panelEditPane.Name = "panelEditPane";
            this.panelEditPane.Size = new System.Drawing.Size(344, 584);
            this.panelEditPane.TabIndex = 20;
            // 
            // panelNewKey
            // 
            this.panelNewKey.Controls.Add(this.panelNewSaveCancel);
            this.panelNewKey.Controls.Add(this.labelNewLoadEntities);
            this.panelNewKey.Controls.Add(this.EntityDropDown);
            this.panelNewKey.Controls.Add(this.labelNames);
            this.panelNewKey.Controls.Add(this.labelNewName);
            this.panelNewKey.Controls.Add(this.comboBoxPrefixes);
            this.panelNewKey.Controls.Add(this.labelUnderscore);
            this.panelNewKey.Controls.Add(this.textNewKeyName);
            this.panelNewKey.Controls.Add(this.labelNewDisplayName);
            this.panelNewKey.Controls.Add(this.textNewKeyDisplayName);
            this.panelNewKey.Controls.Add(this.labelNewSelectAttribs);
            this.panelNewKey.Controls.Add(this.listBoxAttrbutes);
            this.panelNewKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNewKey.Location = new System.Drawing.Point(0, 0);
            this.panelNewKey.Name = "panelNewKey";
            this.panelNewKey.Padding = new System.Windows.Forms.Padding(6);
            this.panelNewKey.Size = new System.Drawing.Size(344, 584);
            this.panelNewKey.TabIndex = 1;
            // 
            // panelNewSaveCancel
            // 
            this.panelNewSaveCancel.Controls.Add(this.buttonCancelNew);
            this.panelNewSaveCancel.Controls.Add(this.buttonSaveNew);
            this.panelNewKey.SetFlowBreak(this.panelNewSaveCancel, true);
            this.panelNewSaveCancel.Location = new System.Drawing.Point(9, 9);
            this.panelNewSaveCancel.Name = "panelNewSaveCancel";
            this.panelNewSaveCancel.Size = new System.Drawing.Size(332, 38);
            this.panelNewSaveCancel.TabIndex = 30;
            // 
            // buttonCancelNew
            // 
            this.buttonCancelNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancelNew.Location = new System.Drawing.Point(254, 6);
            this.buttonCancelNew.Name = "buttonCancelNew";
            this.buttonCancelNew.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelNew.TabIndex = 22;
            this.buttonCancelNew.Text = "Cancel";
            this.buttonCancelNew.UseVisualStyleBackColor = true;
            this.buttonCancelNew.Click += new System.EventHandler(this.ButtonCancelNew_Click);
            // 
            // buttonSaveNew
            // 
            this.buttonSaveNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveNew.Location = new System.Drawing.Point(173, 6);
            this.buttonSaveNew.Name = "buttonSaveNew";
            this.buttonSaveNew.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveNew.TabIndex = 21;
            this.buttonSaveNew.Text = "Save";
            this.buttonSaveNew.UseVisualStyleBackColor = true;
            this.buttonSaveNew.Click += new System.EventHandler(this.buttonSaveNew_Click);
            // 
            // labelNewLoadEntities
            // 
            this.labelNewLoadEntities.Location = new System.Drawing.Point(9, 50);
            this.labelNewLoadEntities.Name = "labelNewLoadEntities";
            this.labelNewLoadEntities.Padding = new System.Windows.Forms.Padding(3);
            this.labelNewLoadEntities.Size = new System.Drawing.Size(322, 23);
            this.labelNewLoadEntities.TabIndex = 20;
            this.labelNewLoadEntities.Text = "Select the Entity for the Alternate Key";
            // 
            // EntityDropDown
            // 
            this.EntityDropDown.Location = new System.Drawing.Point(9, 76);
            this.EntityDropDown.Name = "EntityDropDown";
            this.EntityDropDown.Size = new System.Drawing.Size(322, 27);
            this.EntityDropDown.TabIndex = 19;
            this.EntityDropDown.LoadDataComplete += new System.EventHandler(this.EntityDropDown_LoadDataComplete);
            this.EntityDropDown.SelectedItemChanged += new System.EventHandler<Futurez.XrmToolbox.Controls.EntitiesDropdownControl.SelectedItemChangedEventArgs>(this.EntityDropDown_SelectedItemChanged);
            // 
            // labelNames
            // 
            this.labelNames.Location = new System.Drawing.Point(9, 106);
            this.labelNames.Name = "labelNames";
            this.labelNames.Padding = new System.Windows.Forms.Padding(3);
            this.labelNames.Size = new System.Drawing.Size(321, 23);
            this.labelNames.TabIndex = 25;
            this.labelNames.Text = "Provide a Name and Display Names for the Alternate Key";
            // 
            // labelNewName
            // 
            this.labelNewName.Location = new System.Drawing.Point(9, 129);
            this.labelNewName.Name = "labelNewName";
            this.labelNewName.Padding = new System.Windows.Forms.Padding(3);
            this.labelNewName.Size = new System.Drawing.Size(82, 23);
            this.labelNewName.TabIndex = 26;
            this.labelNewName.Text = "Name:";
            // 
            // comboBoxPrefixes
            // 
            this.comboBoxPrefixes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPrefixes.FormattingEnabled = true;
            this.comboBoxPrefixes.Location = new System.Drawing.Point(97, 132);
            this.comboBoxPrefixes.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.comboBoxPrefixes.Name = "comboBoxPrefixes";
            this.comboBoxPrefixes.Size = new System.Drawing.Size(51, 21);
            this.comboBoxPrefixes.TabIndex = 32;
            // 
            // textNewKeyName
            // 
            this.textNewKeyName.Location = new System.Drawing.Point(158, 132);
            this.textNewKeyName.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.textNewKeyName.MaxLength = 100;
            this.textNewKeyName.Name = "textNewKeyName";
            this.textNewKeyName.Size = new System.Drawing.Size(160, 20);
            this.textNewKeyName.TabIndex = 27;
            // 
            // labelNewDisplayName
            // 
            this.labelNewDisplayName.Location = new System.Drawing.Point(9, 156);
            this.labelNewDisplayName.Name = "labelNewDisplayName";
            this.labelNewDisplayName.Padding = new System.Windows.Forms.Padding(3);
            this.labelNewDisplayName.Size = new System.Drawing.Size(82, 23);
            this.labelNewDisplayName.TabIndex = 28;
            this.labelNewDisplayName.Text = "Display Name:";
            // 
            // textNewKeyDisplayName
            // 
            this.textNewKeyDisplayName.Location = new System.Drawing.Point(97, 159);
            this.textNewKeyDisplayName.MaxLength = 100;
            this.textNewKeyDisplayName.Name = "textNewKeyDisplayName";
            this.textNewKeyDisplayName.Size = new System.Drawing.Size(234, 20);
            this.textNewKeyDisplayName.TabIndex = 29;
            // 
            // labelNewSelectAttribs
            // 
            this.labelNewSelectAttribs.Location = new System.Drawing.Point(9, 182);
            this.labelNewSelectAttribs.Name = "labelNewSelectAttribs";
            this.labelNewSelectAttribs.Padding = new System.Windows.Forms.Padding(3);
            this.labelNewSelectAttribs.Size = new System.Drawing.Size(321, 23);
            this.labelNewSelectAttribs.TabIndex = 24;
            this.labelNewSelectAttribs.Text = "Select the Attributes for the Alternate Key";
            // 
            // listBoxAttrbutes
            // 
            this.listBoxAttrbutes.FormattingEnabled = true;
            this.listBoxAttrbutes.Location = new System.Drawing.Point(9, 208);
            this.listBoxAttrbutes.Name = "listBoxAttrbutes";
            this.listBoxAttrbutes.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxAttrbutes.Size = new System.Drawing.Size(322, 368);
            this.listBoxAttrbutes.TabIndex = 31;
            // 
            // panelKeyDetails
            // 
            this.panelKeyDetails.Controls.Add(this.labelKeyName);
            this.panelKeyDetails.Controls.Add(this.labelKeyNameValue);
            this.panelKeyDetails.Controls.Add(this.labelKeyStatus);
            this.panelKeyDetails.Controls.Add(this.labelKeyStatusValue);
            this.panelKeyDetails.Controls.Add(this.labelKeyIsManaged);
            this.panelKeyDetails.Controls.Add(this.labelKeyIsManagedValue);
            this.panelKeyDetails.Controls.Add(this.labelKeyLogicalName);
            this.panelKeyDetails.Controls.Add(this.labelKeyLogicalNameValue);
            this.panelKeyDetails.Controls.Add(this.labelKeySchemaName);
            this.panelKeyDetails.Controls.Add(this.labelKeySchemaNameValue);
            this.panelKeyDetails.Controls.Add(this.labelKeyMetadataId);
            this.panelKeyDetails.Controls.Add(this.labelKeyMetadataIdValue);
            this.panelKeyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKeyDetails.Location = new System.Drawing.Point(0, 0);
            this.panelKeyDetails.Margin = new System.Windows.Forms.Padding(6);
            this.panelKeyDetails.Name = "panelKeyDetails";
            this.panelKeyDetails.Padding = new System.Windows.Forms.Padding(6);
            this.panelKeyDetails.Size = new System.Drawing.Size(344, 584);
            this.panelKeyDetails.TabIndex = 0;
            // 
            // labelKeyName
            // 
            this.labelKeyName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeyName.Location = new System.Drawing.Point(10, 10);
            this.labelKeyName.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyName.Name = "labelKeyName";
            this.labelKeyName.Size = new System.Drawing.Size(80, 23);
            this.labelKeyName.TabIndex = 0;
            this.labelKeyName.Text = "Name:";
            // 
            // labelKeyNameValue
            // 
            this.labelKeyNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyNameValue.Location = new System.Drawing.Point(98, 10);
            this.labelKeyNameValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyNameValue.Name = "labelKeyNameValue";
            this.labelKeyNameValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeyNameValue.TabIndex = 1;
            // 
            // labelKeyStatus
            // 
            this.labelKeyStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeyStatus.Location = new System.Drawing.Point(10, 41);
            this.labelKeyStatus.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyStatus.Name = "labelKeyStatus";
            this.labelKeyStatus.Size = new System.Drawing.Size(80, 23);
            this.labelKeyStatus.TabIndex = 2;
            this.labelKeyStatus.Text = "Status:";
            // 
            // labelKeyStatusValue
            // 
            this.labelKeyStatusValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyStatusValue.Location = new System.Drawing.Point(98, 41);
            this.labelKeyStatusValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyStatusValue.Name = "labelKeyStatusValue";
            this.labelKeyStatusValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeyStatusValue.TabIndex = 3;
            // 
            // labelKeyIsManaged
            // 
            this.labelKeyIsManaged.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeyIsManaged.Location = new System.Drawing.Point(10, 72);
            this.labelKeyIsManaged.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyIsManaged.Name = "labelKeyIsManaged";
            this.labelKeyIsManaged.Size = new System.Drawing.Size(80, 23);
            this.labelKeyIsManaged.TabIndex = 4;
            this.labelKeyIsManaged.Text = "Is Managed:";
            // 
            // labelKeyIsManagedValue
            // 
            this.labelKeyIsManagedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyIsManagedValue.Location = new System.Drawing.Point(98, 72);
            this.labelKeyIsManagedValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyIsManagedValue.Name = "labelKeyIsManagedValue";
            this.labelKeyIsManagedValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeyIsManagedValue.TabIndex = 5;
            // 
            // labelKeyLogicalName
            // 
            this.labelKeyLogicalName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeyLogicalName.Location = new System.Drawing.Point(10, 103);
            this.labelKeyLogicalName.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyLogicalName.Name = "labelKeyLogicalName";
            this.labelKeyLogicalName.Size = new System.Drawing.Size(80, 23);
            this.labelKeyLogicalName.TabIndex = 6;
            this.labelKeyLogicalName.Text = "Logical Name:";
            // 
            // labelKeyLogicalNameValue
            // 
            this.labelKeyLogicalNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyLogicalNameValue.Location = new System.Drawing.Point(98, 103);
            this.labelKeyLogicalNameValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyLogicalNameValue.Name = "labelKeyLogicalNameValue";
            this.labelKeyLogicalNameValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeyLogicalNameValue.TabIndex = 7;
            // 
            // labelKeySchemaName
            // 
            this.labelKeySchemaName.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeySchemaName.Location = new System.Drawing.Point(10, 134);
            this.labelKeySchemaName.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeySchemaName.Name = "labelKeySchemaName";
            this.labelKeySchemaName.Size = new System.Drawing.Size(80, 23);
            this.labelKeySchemaName.TabIndex = 10;
            this.labelKeySchemaName.Text = "Schema Name:";
            // 
            // labelKeySchemaNameValue
            // 
            this.labelKeySchemaNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeySchemaNameValue.Location = new System.Drawing.Point(98, 134);
            this.labelKeySchemaNameValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeySchemaNameValue.Name = "labelKeySchemaNameValue";
            this.labelKeySchemaNameValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeySchemaNameValue.TabIndex = 11;
            // 
            // labelKeyMetadataId
            // 
            this.labelKeyMetadataId.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelKeyMetadataId.Location = new System.Drawing.Point(10, 165);
            this.labelKeyMetadataId.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyMetadataId.Name = "labelKeyMetadataId";
            this.labelKeyMetadataId.Size = new System.Drawing.Size(80, 23);
            this.labelKeyMetadataId.TabIndex = 12;
            this.labelKeyMetadataId.Text = "Metadata Id:";
            // 
            // labelKeyMetadataIdValue
            // 
            this.labelKeyMetadataIdValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeyMetadataIdValue.Location = new System.Drawing.Point(98, 165);
            this.labelKeyMetadataIdValue.Margin = new System.Windows.Forms.Padding(4);
            this.labelKeyMetadataIdValue.Name = "labelKeyMetadataIdValue";
            this.labelKeyMetadataIdValue.Size = new System.Drawing.Size(232, 23);
            this.labelKeyMetadataIdValue.TabIndex = 13;
            // 
            // toolStripKeysCommands
            // 
            this.tableLayoutMain.SetColumnSpan(this.toolStripKeysCommands, 2);
            this.toolStripKeysCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripKeysCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolButtonLoadSelected,
            this.toolStripSeparator2,
            this.toolButtonActivate,
            this.toolButtonDelete,
            this.toolStripSeparator3,
            this.toolButtonNew});
            this.toolStripKeysCommands.Location = new System.Drawing.Point(0, 0);
            this.toolStripKeysCommands.Name = "toolStripKeysCommands";
            this.toolStripKeysCommands.Size = new System.Drawing.Size(666, 36);
            this.toolStripKeysCommands.TabIndex = 16;
            // 
            // toolButtonLoadSelected
            // 
            this.toolButtonLoadSelected.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonLoadSelected.Image")));
            this.toolButtonLoadSelected.Name = "toolButtonLoadSelected";
            this.toolButtonLoadSelected.Size = new System.Drawing.Size(80, 33);
            this.toolButtonLoadSelected.Text = "Load Keys";
            this.toolButtonLoadSelected.ToolTipText = "Load Alternate Keys for Selected Entities";
            this.toolButtonLoadSelected.Click += new System.EventHandler(this.ToolButtonLoadSelected_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 36);
            // 
            // toolButtonActivate
            // 
            this.toolButtonActivate.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonActivate.Image")));
            this.toolButtonActivate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonActivate.Name = "toolButtonActivate";
            this.toolButtonActivate.Size = new System.Drawing.Size(70, 33);
            this.toolButtonActivate.Text = "Activate";
            this.toolButtonActivate.Click += new System.EventHandler(this.ToolButtonActivate_Click);
            // 
            // toolButtonDelete
            // 
            this.toolButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonDelete.Image")));
            this.toolButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonDelete.Name = "toolButtonDelete";
            this.toolButtonDelete.Size = new System.Drawing.Size(60, 33);
            this.toolButtonDelete.Text = "Delete";
            this.toolButtonDelete.ToolTipText = "Delete the selected Alternate Key(s)";
            this.toolButtonDelete.Click += new System.EventHandler(this.ToolButtonDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 36);
            // 
            // toolButtonNew
            // 
            this.toolButtonNew.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonNew.Image")));
            this.toolButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonNew.Name = "toolButtonNew";
            this.toolButtonNew.Size = new System.Drawing.Size(124, 33);
            this.toolButtonNew.Text = "New Alternate Key";
            this.toolButtonNew.ToolTipText = "Create a new Alternate Key";
            this.toolButtonNew.Click += new System.EventHandler(this.ToolButtonNew_Click);
            // 
            // labelUnderscore
            // 
            this.labelUnderscore.Location = new System.Drawing.Point(148, 129);
            this.labelUnderscore.Margin = new System.Windows.Forms.Padding(0);
            this.labelUnderscore.Name = "labelUnderscore";
            this.labelUnderscore.Size = new System.Drawing.Size(10, 23);
            this.labelUnderscore.TabIndex = 33;
            this.labelUnderscore.Text = "_";
            this.labelUnderscore.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // AlternateKeyManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitPanelsMain);
            this.Controls.Add(this.toolStripMain);
            this.Name = "AlternateKeyManagerControl";
            this.Size = new System.Drawing.Size(1003, 651);
            this.Load += new System.EventHandler(this.AlternateKeyManagerControl_Load);
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.splitPanelsMain.Panel1.ResumeLayout(false);
            this.splitPanelsMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanelsMain)).EndInit();
            this.splitPanelsMain.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.panelEditPane.ResumeLayout(false);
            this.panelNewKey.ResumeLayout(false);
            this.panelNewKey.PerformLayout();
            this.panelNewSaveCancel.ResumeLayout(false);
            this.panelKeyDetails.ResumeLayout(false);
            this.toolStripKeysCommands.ResumeLayout(false);
            this.toolStripKeysCommands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolButtonClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitPanelsMain;
        private Futurez.XrmToolbox.Controls.EntitiesListControl EntitiesListControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.ListView ListViewKeyList;
        private System.Windows.Forms.ColumnHeader colHeadKeyName;
        private System.Windows.Forms.ColumnHeader colHeadState;
        private System.Windows.Forms.ColumnHeader colHeadAttribs;
        private System.Windows.Forms.Panel panelEditPane;
        private System.Windows.Forms.FlowLayoutPanel panelKeyDetails;
        private System.Windows.Forms.Label labelKeyName;
        private System.Windows.Forms.Label labelKeyNameValue;
        private System.Windows.Forms.Label labelKeyStatus;
        private System.Windows.Forms.Label labelKeyStatusValue;
        private System.Windows.Forms.Label labelKeyIsManaged;
        private System.Windows.Forms.Label labelKeyIsManagedValue;
        private System.Windows.Forms.Label labelKeyLogicalName;
        private System.Windows.Forms.Label labelKeyLogicalNameValue;
        private System.Windows.Forms.Label labelKeySchemaName;
        private System.Windows.Forms.Label labelKeySchemaNameValue;
        private System.Windows.Forms.Label labelKeyMetadataId;
        private System.Windows.Forms.Label labelKeyMetadataIdValue;
        private System.Windows.Forms.FlowLayoutPanel panelNewKey;
        private Futurez.XrmToolbox.Controls.EntitiesDropdownControl EntityDropDown;
        private System.Windows.Forms.ToolStrip toolStripKeysCommands;
        private System.Windows.Forms.ToolStripButton toolButtonLoadSelected;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolButtonActivate;
        private System.Windows.Forms.ToolStripButton toolButtonDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolButtonNew;
        private System.Windows.Forms.Label labelNewLoadEntities;
        private System.Windows.Forms.Panel panelNewSaveCancel;
        private System.Windows.Forms.Button buttonCancelNew;
        private System.Windows.Forms.Button buttonSaveNew;
        private System.Windows.Forms.Label labelNames;
        private System.Windows.Forms.Label labelNewName;
        private System.Windows.Forms.TextBox textNewKeyName;
        private System.Windows.Forms.Label labelNewDisplayName;
        private System.Windows.Forms.TextBox textNewKeyDisplayName;
        private System.Windows.Forms.Label labelNewSelectAttribs;
        private System.Windows.Forms.ListBox listBoxAttrbutes;
        private System.Windows.Forms.ComboBox comboBoxPrefixes;
        private System.Windows.Forms.Label labelUnderscore;
    }
}
