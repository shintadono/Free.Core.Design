using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Free.Core.Design
{
	public partial class TypedListEditorForm<T> : TypedListEditorFormBase
	{
		public List<T> Result=new List<T>();
		
		Func<T, string> GetDisplayText;
		Func<T> CreateInstance;

		Attribute[] attributes;

		class Entry
		{
			public T Item;
			public Entry(T item) { Item=item; }
		}

		TypedListEditorForm()
		{
			InitializeComponent();
		}
		
		public TypedListEditorForm(List<T> list, string caption, Func<T, string> getDisplayText, Func<T> createInstance, params Attribute[] attributes)
			: this()
		{
			if(list==null) throw new ArgumentNullException("list");
			if(caption==null) throw new ArgumentNullException("caption");
			if(getDisplayText==null) throw new ArgumentNullException("getDisplayText");
			if(createInstance==null) throw new ArgumentNullException("createInstance");
			if(attributes==null) throw new ArgumentNullException("attributes");

			Text=caption;

			GetDisplayText=getDisplayText;
			CreateInstance=createInstance;

			this.attributes=attributes;

			listbox.SuspendLayout();
			for(int i=0; i<list.Count; i++) listbox.Items.Add(new Entry(list[i]));
			listbox.ResumeLayout();

			if(list.Count!=0) listbox.SelectedIndex=0;
		}

		void UpdateButtons()
		{
			if(listbox.SelectedIndex==-1)
			{
				downButton.Enabled=upButton.Enabled=removeButton.Enabled=false;
				return;
			}

			removeButton.Enabled=true;
			downButton.Enabled=listbox.SelectedIndex<listbox.Items.Count-1;
			upButton.Enabled=listbox.SelectedIndex>0;
		}

		void UpdateListAndLabels()
		{
			listbox.Refresh();

			if(listbox.SelectedIndex==-1)
			{
				propertiesLabel.Text=Properties.Resources.ListEditorFormsPropertiesLabelWithoutText;
				return;
			}

			propertiesLabel.Text=string.Format(Properties.Resources.FormatListEditorFormsPropertiesLabelWithText, GetDisplayText(((Entry)listbox.SelectedItem).Item));
		}

		class TypeAsProperty : PropertyDescriptor, ICustomTypeDescriptor
		{
			Entry entry;
			T old;

			public TypeAsProperty(Entry entry, params Attribute[] attributes) :
				base(Properties.Resources.ListEditorFormPropertyGridValueNameText, attributes)
			{ this.entry=entry; old=entry.Item; }

			AttributeCollection ICustomTypeDescriptor.GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }

			string ICustomTypeDescriptor.GetClassName() { return TypeDescriptor.GetClassName(this, true); }

			string ICustomTypeDescriptor.GetComponentName() { return TypeDescriptor.GetComponentName(this, true); }

			TypeConverter ICustomTypeDescriptor.GetConverter() { return TypeDescriptor.GetConverter(this, true); }

			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }

			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(this, true); }

			object ICustomTypeDescriptor.GetEditor(Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }

			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }

			EventDescriptorCollection ICustomTypeDescriptor.GetEvents() { return TypeDescriptor.GetEvents(this, true); }

			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) { return ((ICustomTypeDescriptor)this).GetProperties(); }

			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(this);
				return props;
			}

			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) { return this; }

			public override bool CanResetValue(object component) { return true; }

			public override Type ComponentType { get { return typeof(TypeAsProperty); } }

			public override object GetValue(object component) { return entry.Item; }

			public override bool IsReadOnly { get { return false; } }

			public override Type PropertyType { get { return typeof(T); } }

			public override void ResetValue(object component) { entry.Item=old; }

			public override bool ShouldSerializeValue(object component) { return false; }

			public override void SetValue(object component, object value) { entry.Item=(T)value; }
		}

		protected override void listbox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if(e.Index<0) return;

			Graphics graphics=e.Graphics;

			Color window=SystemColors.Window;
			Color windowText=SystemColors.WindowText;
			if((e.State&DrawItemState.Selected)==DrawItemState.Selected)
			{
				window=SystemColors.Highlight;
				windowText=SystemColors.HighlightText;
			}

			int count=listbox.Items.Count;

			int maxIndex=count==0?0:count-1;
			string maxIndexStr=maxIndex.ToString();

			SizeF maxIndexStrSize=graphics.MeasureString(maxIndexStr, listbox.Font);
			int boxWidth=Math.Max(4+maxIndexStr.Length*Font.Height/2, (int)Math.Ceiling((double)maxIndexStrSize.Width))+SystemInformation.BorderSize.Width*4;

			Rectangle rectangle=new Rectangle(e.Bounds.X, e.Bounds.Y, boxWidth, e.Bounds.Height);
			ControlPaint.DrawButton(graphics, rectangle, ButtonState.Normal);

			Rectangle rect=new Rectangle(e.Bounds.X+boxWidth, e.Bounds.Y, e.Bounds.Width-boxWidth, e.Bounds.Height);
			graphics.FillRectangle(new SolidBrush(window), rect);

			if((e.State&DrawItemState.Focus)==DrawItemState.Focus)
				ControlPaint.DrawFocusRectangle(graphics, rect);

			using(StringFormat format=new StringFormat())
			{
				format.Alignment=StringAlignment.Center;
				graphics.DrawString(e.Index.ToString(), Font, SystemBrushes.ControlText, rectangle, format);
			}

			Brush brush=new SolidBrush(windowText);
			string displayText=GetDisplayText(((Entry)listbox.Items[e.Index]).Item);

			boxWidth+=2;
			try
			{
				graphics.DrawString(displayText, Font, brush, new Rectangle(e.Bounds.X+boxWidth, e.Bounds.Y, e.Bounds.Width-boxWidth, e.Bounds.Height));
			}
			finally
			{
				if(brush!=null) brush.Dispose();
			}

			int horizontalExtent=boxWidth+(int)graphics.MeasureString(displayText, Font).Width;

			if(horizontalExtent>e.Bounds.Width&&listbox.HorizontalExtent<horizontalExtent)
				listbox.HorizontalExtent=horizontalExtent;
		}

		protected override void listbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtons();
			UpdateListAndLabels();

			if(listbox.SelectedIndex==-1)
			{
				propertyBrowser.SelectedObject=null;
				propertyBrowser.Enabled=false;
				return;
			}

			propertyBrowser.Enabled=true;
			propertyBrowser.SelectedObject=new TypeAsProperty((Entry)listbox.SelectedItem, attributes);
		}

		protected override void propertyBrowser_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			UpdateListAndLabels();
		}

		protected override void upButton_Click(object sender, EventArgs e)
		{
			if(listbox.SelectedIndex<=0) return;

			listbox.SuspendLayout();

			int index=listbox.SelectedIndex;

			T temp=((Entry)listbox.Items[index]).Item;
			((Entry)listbox.Items[index]).Item=((Entry)listbox.Items[index-1]).Item;
			((Entry)listbox.Items[index-1]).Item=temp;

			listbox.SelectedIndex=index-1;

			listbox.ResumeLayout();
		}

		protected override void downButton_Click(object sender, EventArgs e)
		{
			if(listbox.SelectedIndex==-1||listbox.SelectedIndex>=listbox.Items.Count-1) return;

			listbox.SuspendLayout();

			int index=listbox.SelectedIndex;

			T temp=((Entry)listbox.Items[index]).Item;
			((Entry)listbox.Items[index]).Item=((Entry)listbox.Items[index+1]).Item;
			((Entry)listbox.Items[index+1]).Item=temp;

			listbox.SelectedIndex=index+1;

			listbox.ResumeLayout();
		}

		protected override void addButton_Click(object sender, EventArgs e)
		{
			listbox.SelectedIndex=listbox.Items.Add(new Entry(CreateInstance()));
		}

		protected override void removeButton_Click(object sender, EventArgs e)
		{
			if(listbox.SelectedIndex==-1) return;

			listbox.SuspendLayout();

			int index=listbox.SelectedIndex;

			if(index==0&&listbox.Items.Count>1) listbox.SelectedIndex=1;
			else listbox.SelectedIndex=index-1;

			listbox.Items.RemoveAt(index);
			
			listbox_SelectedIndexChanged(sender, e);

			listbox.ResumeLayout();
		}

		protected override void okButton_Click(object sender, EventArgs e)
		{
			Result.Clear();

			for(int i=0; i<listbox.Items.Count; i++)
				Result.Add((T)((Entry)listbox.Items[i]).Item);

			DialogResult=DialogResult.OK;
			Close();
		}

		protected override void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
			Close();
		}
	}
}
