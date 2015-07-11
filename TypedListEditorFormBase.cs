using System;
using System.Drawing;
using System.Windows.Forms;

namespace Free.Core.Design
{
	/// <summary>
	/// Not fully implemented base class for generic class <see cref="TypedListEditorForm{T}"/>,
	/// necessary because resources don't work with generic types.
	/// </summary>
	public partial class TypedListEditorFormBase : Form
	{
		protected TypedListEditorFormBase()
		{
			InitializeComponent();

			listbox.ItemHeight=Font.Height+(SystemInformation.BorderSize.Width*2);
		}

		protected virtual void listbox_DrawItem(object sender, DrawItemEventArgs e) { throw new NotImplementedException(); }
		protected virtual void listbox_SelectedIndexChanged(object sender, EventArgs e) { throw new NotImplementedException(); }

		protected virtual void listbox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Insert) addButton_Click(sender, EventArgs.Empty);
			else if(e.KeyCode==Keys.Delete) removeButton_Click(sender, EventArgs.Empty);
		}

		protected virtual void propertyBrowser_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e) { throw new NotImplementedException(); }
		protected virtual void upButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
		protected virtual void downButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
		protected virtual void addButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
		protected virtual void removeButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
		protected virtual void okButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
		protected virtual void cancelButton_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
	}
}
