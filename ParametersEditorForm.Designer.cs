using System.ComponentModel;
namespace Free.Core.Design
{
	partial class ParametersEditorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components=null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing&&(components!=null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParametersEditorForm));
			this.elementsLabel = new System.Windows.Forms.Label();
			this.listbox = new System.Windows.Forms.ListBox();
			this.propertyBrowser = new System.Windows.Forms.PropertyGrid();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.propertiesLabel = new System.Windows.Forms.Label();
			this.buttonChangeType = new System.Windows.Forms.Button();
			this.contextMenuStripStringTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.listOfStringsToolStripMenuItemStringTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripListOfStringsTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.stringToolStripMenuItemStringListTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripBooleanTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.listOfBooleansToolStripMenuItemBooleanTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripListOfBooleansTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.booleanToolStripMenuItemListOfBooleansTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripIntegerNumberTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.floatingPointNumberToolStripMenuItemIntegerNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripListOfIntegerNumbersTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.integerNumberToolStripMenuItemListOfIntegerNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripFloatingPointNumberTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.integerNumberToolStripMenuItemFloatingPointNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripListOfFloatingPointNumbersTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripParametersTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.listOfParametersToolStripMenuItemParametersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripListOfParametersTo = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.parametersToolStripMenuItemListOfParametersTo = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.parametersToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.booleanToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.integerNumberToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.floatingPointNumberToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.stringToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.byteArrayToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfBooleansToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfIntegerNumbersToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfFloatingPointNumbersToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfStringsToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.listOfParametersToolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStripStringTo.SuspendLayout();
			this.contextMenuStripListOfStringsTo.SuspendLayout();
			this.contextMenuStripBooleanTo.SuspendLayout();
			this.contextMenuStripListOfBooleansTo.SuspendLayout();
			this.contextMenuStripIntegerNumberTo.SuspendLayout();
			this.contextMenuStripListOfIntegerNumbersTo.SuspendLayout();
			this.contextMenuStripFloatingPointNumberTo.SuspendLayout();
			this.contextMenuStripListOfFloatingPointNumbersTo.SuspendLayout();
			this.contextMenuStripParametersTo.SuspendLayout();
			this.contextMenuStripListOfParametersTo.SuspendLayout();
			this.contextMenuStripAdd.SuspendLayout();
			this.SuspendLayout();
			// 
			// elementsLabel
			// 
			resources.ApplyResources(this.elementsLabel, "elementsLabel");
			this.elementsLabel.Name = "elementsLabel";
			// 
			// listbox
			// 
			resources.ApplyResources(this.listbox, "listbox");
			this.listbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listbox.Name = "listbox";
			this.listbox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listbox_DrawItem);
			this.listbox.SelectedIndexChanged += new System.EventHandler(this.listbox_SelectedIndexChanged);
			this.listbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listbox_KeyDown);
			// 
			// propertyBrowser
			// 
			resources.ApplyResources(this.propertyBrowser, "propertyBrowser");
			this.propertyBrowser.CommandsVisibleIfAvailable = false;
			this.propertyBrowser.Name = "propertyBrowser";
			this.propertyBrowser.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyBrowser_PropertyValueChanged);
			// 
			// addButton
			// 
			resources.ApplyResources(this.addButton, "addButton");
			this.addButton.Image = global::Free.Core.Design.Properties.Resources.Arrow;
			this.addButton.Name = "addButton";
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// removeButton
			// 
			resources.ApplyResources(this.removeButton, "removeButton");
			this.removeButton.Name = "removeButton";
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// propertiesLabel
			// 
			resources.ApplyResources(this.propertiesLabel, "propertiesLabel");
			this.propertiesLabel.Name = "propertiesLabel";
			// 
			// buttonChangeType
			// 
			resources.ApplyResources(this.buttonChangeType, "buttonChangeType");
			this.buttonChangeType.Image = global::Free.Core.Design.Properties.Resources.Arrow;
			this.buttonChangeType.Name = "buttonChangeType";
			this.buttonChangeType.UseVisualStyleBackColor = true;
			this.buttonChangeType.Click += new System.EventHandler(this.buttonChangeType_Click);
			// 
			// contextMenuStripStringTo
			// 
			this.contextMenuStripStringTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfStringsToolStripMenuItemStringTo});
			this.contextMenuStripStringTo.Name = "contextMenuStripStringTo";
			resources.ApplyResources(this.contextMenuStripStringTo, "contextMenuStripStringTo");
			// 
			// listOfStringsToolStripMenuItemStringTo
			// 
			this.listOfStringsToolStripMenuItemStringTo.Name = "listOfStringsToolStripMenuItemStringTo";
			resources.ApplyResources(this.listOfStringsToolStripMenuItemStringTo, "listOfStringsToolStripMenuItemStringTo");
			this.listOfStringsToolStripMenuItemStringTo.Click += new System.EventHandler(this.listOfStringsToolStripMenuItemStringTo_Click);
			// 
			// contextMenuStripListOfStringsTo
			// 
			this.contextMenuStripListOfStringsTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stringToolStripMenuItemStringListTo});
			this.contextMenuStripListOfStringsTo.Name = "contextMenuStripListOfStringsTo";
			resources.ApplyResources(this.contextMenuStripListOfStringsTo, "contextMenuStripListOfStringsTo");
			// 
			// stringToolStripMenuItemStringListTo
			// 
			this.stringToolStripMenuItemStringListTo.Name = "stringToolStripMenuItemStringListTo";
			resources.ApplyResources(this.stringToolStripMenuItemStringListTo, "stringToolStripMenuItemStringListTo");
			this.stringToolStripMenuItemStringListTo.Click += new System.EventHandler(this.stringToolStripMenuItemStringListTo_Click);
			// 
			// contextMenuStripBooleanTo
			// 
			this.contextMenuStripBooleanTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfBooleansToolStripMenuItemBooleanTo});
			this.contextMenuStripBooleanTo.Name = "contextMenuStripBooleanTo";
			resources.ApplyResources(this.contextMenuStripBooleanTo, "contextMenuStripBooleanTo");
			// 
			// listOfBooleansToolStripMenuItemBooleanTo
			// 
			this.listOfBooleansToolStripMenuItemBooleanTo.Name = "listOfBooleansToolStripMenuItemBooleanTo";
			resources.ApplyResources(this.listOfBooleansToolStripMenuItemBooleanTo, "listOfBooleansToolStripMenuItemBooleanTo");
			this.listOfBooleansToolStripMenuItemBooleanTo.Click += new System.EventHandler(this.listOfBooleansToolStripMenuItemBooleanTo_Click);
			// 
			// contextMenuStripListOfBooleansTo
			// 
			this.contextMenuStripListOfBooleansTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.booleanToolStripMenuItemListOfBooleansTo});
			this.contextMenuStripListOfBooleansTo.Name = "contextMenuStripListOfBooleansTo";
			resources.ApplyResources(this.contextMenuStripListOfBooleansTo, "contextMenuStripListOfBooleansTo");
			// 
			// booleanToolStripMenuItemListOfBooleansTo
			// 
			this.booleanToolStripMenuItemListOfBooleansTo.Name = "booleanToolStripMenuItemListOfBooleansTo";
			resources.ApplyResources(this.booleanToolStripMenuItemListOfBooleansTo, "booleanToolStripMenuItemListOfBooleansTo");
			this.booleanToolStripMenuItemListOfBooleansTo.Click += new System.EventHandler(this.booleanToolStripMenuItemListOfBooleansTo_Click);
			// 
			// contextMenuStripIntegerNumberTo
			// 
			this.contextMenuStripIntegerNumberTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.floatingPointNumberToolStripMenuItemIntegerNumberTo,
            this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo,
            this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo});
			this.contextMenuStripIntegerNumberTo.Name = "contextMenuStripIntegerNumberTo";
			resources.ApplyResources(this.contextMenuStripIntegerNumberTo, "contextMenuStripIntegerNumberTo");
			// 
			// floatingPointNumberToolStripMenuItemIntegerNumberTo
			// 
			this.floatingPointNumberToolStripMenuItemIntegerNumberTo.Name = "floatingPointNumberToolStripMenuItemIntegerNumberTo";
			resources.ApplyResources(this.floatingPointNumberToolStripMenuItemIntegerNumberTo, "floatingPointNumberToolStripMenuItemIntegerNumberTo");
			this.floatingPointNumberToolStripMenuItemIntegerNumberTo.Click += new System.EventHandler(this.floatingPointNumberToolStripMenuItemIntegerNumberTo_Click);
			// 
			// listOfIntegerNumbersToolStripMenuItemIntegerNumberTo
			// 
			this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo.Name = "listOfIntegerNumbersToolStripMenuItemIntegerNumberTo";
			resources.ApplyResources(this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo, "listOfIntegerNumbersToolStripMenuItemIntegerNumberTo");
			this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo.Click += new System.EventHandler(this.listOfIntegerNumbersToolStripMenuItemIntegerNumberTo_Click);
			// 
			// listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo
			// 
			this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo.Name = "listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo";
			resources.ApplyResources(this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo, "listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo");
			this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo.Click += new System.EventHandler(this.listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo_Click);
			// 
			// contextMenuStripListOfIntegerNumbersTo
			// 
			this.contextMenuStripListOfIntegerNumbersTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integerNumberToolStripMenuItemListOfIntegerNumbersTo,
            this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo,
            this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo});
			this.contextMenuStripListOfIntegerNumbersTo.Name = "contextMenuStripListOfIntegerNumbersTo";
			resources.ApplyResources(this.contextMenuStripListOfIntegerNumbersTo, "contextMenuStripListOfIntegerNumbersTo");
			// 
			// integerNumberToolStripMenuItemListOfIntegerNumbersTo
			// 
			this.integerNumberToolStripMenuItemListOfIntegerNumbersTo.Name = "integerNumberToolStripMenuItemListOfIntegerNumbersTo";
			resources.ApplyResources(this.integerNumberToolStripMenuItemListOfIntegerNumbersTo, "integerNumberToolStripMenuItemListOfIntegerNumbersTo");
			this.integerNumberToolStripMenuItemListOfIntegerNumbersTo.Click += new System.EventHandler(this.integerNumberToolStripMenuItemListOfIntegerNumbersTo_Click);
			// 
			// floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo
			// 
			this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo.Name = "floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo";
			resources.ApplyResources(this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo, "floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo");
			this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo.Click += new System.EventHandler(this.floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo_Click);
			// 
			// listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo
			// 
			this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo.Name = "listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo";
			resources.ApplyResources(this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo, "listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo");
			this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo.Click += new System.EventHandler(this.listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo_Click);
			// 
			// contextMenuStripFloatingPointNumberTo
			// 
			this.contextMenuStripFloatingPointNumberTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integerNumberToolStripMenuItemFloatingPointNumberTo,
            this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo,
            this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo});
			this.contextMenuStripFloatingPointNumberTo.Name = "contextMenuStripFloatingPointNumberTo";
			resources.ApplyResources(this.contextMenuStripFloatingPointNumberTo, "contextMenuStripFloatingPointNumberTo");
			// 
			// integerNumberToolStripMenuItemFloatingPointNumberTo
			// 
			this.integerNumberToolStripMenuItemFloatingPointNumberTo.Name = "integerNumberToolStripMenuItemFloatingPointNumberTo";
			resources.ApplyResources(this.integerNumberToolStripMenuItemFloatingPointNumberTo, "integerNumberToolStripMenuItemFloatingPointNumberTo");
			this.integerNumberToolStripMenuItemFloatingPointNumberTo.Click += new System.EventHandler(this.integerNumberToolStripMenuItemFloatingPointNumberTo_Click);
			// 
			// listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo
			// 
			this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo.Name = "listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo";
			resources.ApplyResources(this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo, "listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo");
			this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo.Click += new System.EventHandler(this.listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo_Click);
			// 
			// listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo
			// 
			this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo.Name = "listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo";
			resources.ApplyResources(this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo, "listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo");
			this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo.Click += new System.EventHandler(this.listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo_Click);
			// 
			// contextMenuStripListOfFloatingPointNumbersTo
			// 
			this.contextMenuStripListOfFloatingPointNumbersTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo,
            this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo,
            this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo});
			this.contextMenuStripListOfFloatingPointNumbersTo.Name = "contextMenuStripListOfFloatingPointNumbersTo";
			resources.ApplyResources(this.contextMenuStripListOfFloatingPointNumbersTo, "contextMenuStripListOfFloatingPointNumbersTo");
			// 
			// integerNumberToolStripMenuItemListOfFloatingPointNumbersTo
			// 
			this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo.Name = "integerNumberToolStripMenuItemListOfFloatingPointNumbersTo";
			resources.ApplyResources(this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo, "integerNumberToolStripMenuItemListOfFloatingPointNumbersTo");
			this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo.Click += new System.EventHandler(this.integerNumberToolStripMenuItemListOfFloatingPointNumbersTo_Click);
			// 
			// floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo
			// 
			this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo.Name = "floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo";
			resources.ApplyResources(this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo, "floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo");
			this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo.Click += new System.EventHandler(this.floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo_Click);
			// 
			// listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo
			// 
			this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo.Name = "listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo";
			resources.ApplyResources(this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo, "listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo");
			this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo.Click += new System.EventHandler(this.listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo_Click);
			// 
			// contextMenuStripParametersTo
			// 
			this.contextMenuStripParametersTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listOfParametersToolStripMenuItemParametersTo});
			this.contextMenuStripParametersTo.Name = "contextMenuStripParametersTo";
			resources.ApplyResources(this.contextMenuStripParametersTo, "contextMenuStripParametersTo");
			// 
			// listOfParametersToolStripMenuItemParametersTo
			// 
			this.listOfParametersToolStripMenuItemParametersTo.Name = "listOfParametersToolStripMenuItemParametersTo";
			resources.ApplyResources(this.listOfParametersToolStripMenuItemParametersTo, "listOfParametersToolStripMenuItemParametersTo");
			this.listOfParametersToolStripMenuItemParametersTo.Click += new System.EventHandler(this.listOfParametersToolStripMenuItemParametersTo_Click);
			// 
			// contextMenuStripListOfParametersTo
			// 
			this.contextMenuStripListOfParametersTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersToolStripMenuItemListOfParametersTo});
			this.contextMenuStripListOfParametersTo.Name = "contextMenuStripListOfParametersTo";
			resources.ApplyResources(this.contextMenuStripListOfParametersTo, "contextMenuStripListOfParametersTo");
			// 
			// parametersToolStripMenuItemListOfParametersTo
			// 
			this.parametersToolStripMenuItemListOfParametersTo.Name = "parametersToolStripMenuItemListOfParametersTo";
			resources.ApplyResources(this.parametersToolStripMenuItemListOfParametersTo, "parametersToolStripMenuItemListOfParametersTo");
			this.parametersToolStripMenuItemListOfParametersTo.Click += new System.EventHandler(this.parametersToolStripMenuItemListOfParametersTo_Click);
			// 
			// contextMenuStripAdd
			// 
			this.contextMenuStripAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersToolStripMenuItemAdd,
            this.booleanToolStripMenuItemAdd,
            this.integerNumberToolStripMenuItemAdd,
            this.floatingPointNumberToolStripMenuItemAdd,
            this.stringToolStripMenuItemAdd,
            this.byteArrayToolStripMenuItemAdd,
            this.listOfBooleansToolStripMenuItemAdd,
            this.listOfIntegerNumbersToolStripMenuItemAdd,
            this.listOfFloatingPointNumbersToolStripMenuItemAdd,
            this.listOfStringsToolStripMenuItemAdd,
            this.listOfParametersToolStripMenuItemAdd});
			this.contextMenuStripAdd.Name = "contextMenuStripAdd";
			resources.ApplyResources(this.contextMenuStripAdd, "contextMenuStripAdd");
			// 
			// parametersToolStripMenuItemAdd
			// 
			this.parametersToolStripMenuItemAdd.Name = "parametersToolStripMenuItemAdd";
			resources.ApplyResources(this.parametersToolStripMenuItemAdd, "parametersToolStripMenuItemAdd");
			this.parametersToolStripMenuItemAdd.Click += new System.EventHandler(this.parametersToolStripMenuItemAdd_Click);
			// 
			// booleanToolStripMenuItemAdd
			// 
			this.booleanToolStripMenuItemAdd.Name = "booleanToolStripMenuItemAdd";
			resources.ApplyResources(this.booleanToolStripMenuItemAdd, "booleanToolStripMenuItemAdd");
			this.booleanToolStripMenuItemAdd.Click += new System.EventHandler(this.booleanToolStripMenuItemAdd_Click);
			// 
			// integerNumberToolStripMenuItemAdd
			// 
			this.integerNumberToolStripMenuItemAdd.Name = "integerNumberToolStripMenuItemAdd";
			resources.ApplyResources(this.integerNumberToolStripMenuItemAdd, "integerNumberToolStripMenuItemAdd");
			this.integerNumberToolStripMenuItemAdd.Click += new System.EventHandler(this.integerNumberToolStripMenuItemAdd_Click);
			// 
			// floatingPointNumberToolStripMenuItemAdd
			// 
			this.floatingPointNumberToolStripMenuItemAdd.Name = "floatingPointNumberToolStripMenuItemAdd";
			resources.ApplyResources(this.floatingPointNumberToolStripMenuItemAdd, "floatingPointNumberToolStripMenuItemAdd");
			this.floatingPointNumberToolStripMenuItemAdd.Click += new System.EventHandler(this.floatingPointNumberToolStripMenuItemAdd_Click);
			// 
			// stringToolStripMenuItemAdd
			// 
			this.stringToolStripMenuItemAdd.Name = "stringToolStripMenuItemAdd";
			resources.ApplyResources(this.stringToolStripMenuItemAdd, "stringToolStripMenuItemAdd");
			this.stringToolStripMenuItemAdd.Click += new System.EventHandler(this.stringToolStripMenuItemAdd_Click);
			// 
			// byteArrayToolStripMenuItemAdd
			// 
			this.byteArrayToolStripMenuItemAdd.Name = "byteArrayToolStripMenuItemAdd";
			resources.ApplyResources(this.byteArrayToolStripMenuItemAdd, "byteArrayToolStripMenuItemAdd");
			this.byteArrayToolStripMenuItemAdd.Click += new System.EventHandler(this.byteArrayToolStripMenuItemAdd_Click);
			// 
			// listOfBooleansToolStripMenuItemAdd
			// 
			this.listOfBooleansToolStripMenuItemAdd.Name = "listOfBooleansToolStripMenuItemAdd";
			resources.ApplyResources(this.listOfBooleansToolStripMenuItemAdd, "listOfBooleansToolStripMenuItemAdd");
			this.listOfBooleansToolStripMenuItemAdd.Click += new System.EventHandler(this.listOfBooleansToolStripMenuItemAdd_Click);
			// 
			// listOfIntegerNumbersToolStripMenuItemAdd
			// 
			this.listOfIntegerNumbersToolStripMenuItemAdd.Name = "listOfIntegerNumbersToolStripMenuItemAdd";
			resources.ApplyResources(this.listOfIntegerNumbersToolStripMenuItemAdd, "listOfIntegerNumbersToolStripMenuItemAdd");
			this.listOfIntegerNumbersToolStripMenuItemAdd.Click += new System.EventHandler(this.listOfIntegerNumbersToolStripMenuItemAdd_Click);
			// 
			// listOfFloatingPointNumbersToolStripMenuItemAdd
			// 
			this.listOfFloatingPointNumbersToolStripMenuItemAdd.Name = "listOfFloatingPointNumbersToolStripMenuItemAdd";
			resources.ApplyResources(this.listOfFloatingPointNumbersToolStripMenuItemAdd, "listOfFloatingPointNumbersToolStripMenuItemAdd");
			this.listOfFloatingPointNumbersToolStripMenuItemAdd.Click += new System.EventHandler(this.listOfFloatingPointNumbersToolStripMenuItemAdd_Click);
			// 
			// listOfStringsToolStripMenuItemAdd
			// 
			this.listOfStringsToolStripMenuItemAdd.Name = "listOfStringsToolStripMenuItemAdd";
			resources.ApplyResources(this.listOfStringsToolStripMenuItemAdd, "listOfStringsToolStripMenuItemAdd");
			this.listOfStringsToolStripMenuItemAdd.Click += new System.EventHandler(this.listOfStringsToolStripMenuItemAdd_Click);
			// 
			// listOfParametersToolStripMenuItemAdd
			// 
			this.listOfParametersToolStripMenuItemAdd.Name = "listOfParametersToolStripMenuItemAdd";
			resources.ApplyResources(this.listOfParametersToolStripMenuItemAdd, "listOfParametersToolStripMenuItemAdd");
			this.listOfParametersToolStripMenuItemAdd.Click += new System.EventHandler(this.listOfParametersToolStripMenuItemAdd_Click);
			// 
			// ParametersEditorForm
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.buttonChangeType);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.propertyBrowser);
			this.Controls.Add(this.listbox);
			this.Controls.Add(this.propertiesLabel);
			this.Controls.Add(this.elementsLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ParametersEditorForm";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.contextMenuStripStringTo.ResumeLayout(false);
			this.contextMenuStripListOfStringsTo.ResumeLayout(false);
			this.contextMenuStripBooleanTo.ResumeLayout(false);
			this.contextMenuStripListOfBooleansTo.ResumeLayout(false);
			this.contextMenuStripIntegerNumberTo.ResumeLayout(false);
			this.contextMenuStripListOfIntegerNumbersTo.ResumeLayout(false);
			this.contextMenuStripFloatingPointNumberTo.ResumeLayout(false);
			this.contextMenuStripListOfFloatingPointNumbersTo.ResumeLayout(false);
			this.contextMenuStripParametersTo.ResumeLayout(false);
			this.contextMenuStripListOfParametersTo.ResumeLayout(false);
			this.contextMenuStripAdd.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label elementsLabel;
		private System.Windows.Forms.ListBox listbox;
		private System.Windows.Forms.PropertyGrid propertyBrowser;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label propertiesLabel;
		private System.Windows.Forms.Button buttonChangeType;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripStringTo;
		private System.Windows.Forms.ToolStripMenuItem listOfStringsToolStripMenuItemStringTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListOfStringsTo;
		private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItemStringListTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripBooleanTo;
		private System.Windows.Forms.ToolStripMenuItem listOfBooleansToolStripMenuItemBooleanTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListOfBooleansTo;
		private System.Windows.Forms.ToolStripMenuItem booleanToolStripMenuItemListOfBooleansTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripIntegerNumberTo;
		private System.Windows.Forms.ToolStripMenuItem floatingPointNumberToolStripMenuItemIntegerNumberTo;
		private System.Windows.Forms.ToolStripMenuItem listOfIntegerNumbersToolStripMenuItemIntegerNumberTo;
		private System.Windows.Forms.ToolStripMenuItem listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListOfIntegerNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem integerNumberToolStripMenuItemListOfIntegerNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripFloatingPointNumberTo;
		private System.Windows.Forms.ToolStripMenuItem integerNumberToolStripMenuItemFloatingPointNumberTo;
		private System.Windows.Forms.ToolStripMenuItem listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo;
		private System.Windows.Forms.ToolStripMenuItem listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListOfFloatingPointNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem integerNumberToolStripMenuItemListOfFloatingPointNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo;
		private System.Windows.Forms.ToolStripMenuItem listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripParametersTo;
		private System.Windows.Forms.ToolStripMenuItem listOfParametersToolStripMenuItemParametersTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripListOfParametersTo;
		private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItemListOfParametersTo;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripAdd;
		private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem booleanToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem integerNumberToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem floatingPointNumberToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem byteArrayToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem listOfBooleansToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem listOfIntegerNumbersToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem listOfFloatingPointNumbersToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem listOfStringsToolStripMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem listOfParametersToolStripMenuItemAdd;
	}
}
