using System.ComponentModel;
namespace Free.Core.Design
{
	partial class TypedListEditorFormBase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypedListEditorFormBase));
			this.elementsLabel = new System.Windows.Forms.Label();
			this.listbox = new System.Windows.Forms.ListBox();
			this.upButton = new System.Windows.Forms.Button();
			this.downButton = new System.Windows.Forms.Button();
			this.propertyBrowser = new System.Windows.Forms.PropertyGrid();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.propertiesLabel = new System.Windows.Forms.Label();
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
			// upButton
			// 
			resources.ApplyResources(this.upButton, "upButton");
			this.upButton.Image = global::Free.Core.Design.Properties.Resources.SortUp;
			this.upButton.Name = "upButton";
			this.upButton.UseVisualStyleBackColor = true;
			this.upButton.Click += new System.EventHandler(this.upButton_Click);
			// 
			// downButton
			// 
			resources.ApplyResources(this.downButton, "downButton");
			this.downButton.Image = global::Free.Core.Design.Properties.Resources.SortDown;
			this.downButton.Name = "downButton";
			this.downButton.Click += new System.EventHandler(this.downButton_Click);
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
			// TypedListEditorFormBase
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.removeButton);
			this.Controls.Add(this.addButton);
			this.Controls.Add(this.propertyBrowser);
			this.Controls.Add(this.downButton);
			this.Controls.Add(this.upButton);
			this.Controls.Add(this.listbox);
			this.Controls.Add(this.propertiesLabel);
			this.Controls.Add(this.elementsLabel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TypedListEditorFormBase";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		protected System.Windows.Forms.ListBox listbox;
		protected System.Windows.Forms.PropertyGrid propertyBrowser;
		protected System.Windows.Forms.Button upButton;
		protected System.Windows.Forms.Button downButton;
		protected System.Windows.Forms.Button addButton;
		protected System.Windows.Forms.Button removeButton;
		protected System.Windows.Forms.Label elementsLabel;
		protected System.Windows.Forms.Button okButton;
		protected System.Windows.Forms.Button cancelButton;
		protected System.Windows.Forms.Label propertiesLabel;
	}
}
