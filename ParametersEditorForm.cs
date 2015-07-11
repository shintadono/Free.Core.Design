using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public partial class ParametersEditorForm : Form
	{
		public Parameters Result=new Parameters();

		public ParametersEditorForm()
		{
			InitializeComponent();

			listbox.ItemHeight=Font.Height+(SystemInformation.BorderSize.Width*2);
		}

		public ParametersEditorForm(Parameters param)
			: this()
		{
			if(param==null) throw new ArgumentNullException("param");

			Result=(Parameters)param.Clone();

			UpdateListBox();

			if(listbox.Items.Count!=0) listbox.SelectedIndex=0;
		}

		void UpdateListBox()
		{
			try
			{
				listbox.SuspendLayout();

				listbox.Items.Clear();

				foreach(string key in Result.Keys)
				{
					object obj=Result.Get(key);

					ListViewItem item=new ListViewItem();
					item.Text=key;

					if(obj is Parameters||obj is long||obj is double||obj is string||obj is bool||obj is byte[]||
						obj is List<long>||obj is List<double>||obj is List<string>||obj is List<bool>||obj is List<Parameters>) listbox.Items.Add(key);
				}

				removeButton.Enabled=buttonChangeType.Enabled=false;
			}
			finally
			{
				listbox.ResumeLayout();
			}
		}

		void UpdateButtons()
		{
			removeButton.Enabled=true;
			buttonChangeType.Enabled=listbox.SelectedIndex<listbox.Items.Count-1;

			int index=listbox.SelectedIndex;
			if(index>=0&&index<listbox.Items.Count)
			{
				string key=(string)listbox.Items[index];
				object obj=Result.Get(key);
				buttonChangeType.Enabled=obj is Parameters||obj is long||obj is double||obj is string||obj is bool||
					obj is List<long>||obj is List<double>||obj is List<string>||obj is List<bool>||obj is List<Parameters>;
				removeButton.Enabled=true;
			}
			else removeButton.Enabled=buttonChangeType.Enabled=false;
		}

		void UpdateListAndLabels()
		{
			listbox.Refresh();

			if(listbox.SelectedIndex==-1)
			{
				propertiesLabel.Text=Properties.Resources.ListEditorFormsPropertiesLabelWithoutText;
				return;
			}

			propertiesLabel.Text=string.Format(Properties.Resources.FormatListEditorFormsPropertiesLabelWithText, GetDisplayText(listbox.SelectedItem));
		}

		string GetDisplayText(object value)
		{
			string key=value as string;
			if(key==null) return string.Empty;
			return key;
		}

		class NamePropertyDescriptor : PropertyDescriptor
		{
			ListBox.ObjectCollection list;
			int index;
			Parameters parameters;
			string name, category;
			Type componentType;

			public NamePropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category, Type componentType)
				: base(name, null)
			{
				this.list=list;
				this.index=index;
				this.parameters=parameters;
				this.name=name;
				this.category=category;
				this.componentType=componentType;
			}

			public override AttributeCollection Attributes { get { return new AttributeCollection(null); } }

			public override bool CanResetValue(object component) { return false; }

			public override string Category { get { return category; } }

			public override Type ComponentType { get { return componentType; } }

			public override string DisplayName { get { return name; } }

			public override string Description { get { return "The name of the parameter."; } }

			public override object GetValue(object component) { return list[index]; }

			public override bool IsReadOnly { get { return false; } }

			public override string Name { get { return name; } }

			public override Type PropertyType { get { return typeof(string); } }

			public override void ResetValue(object component) { }

			public override bool ShouldSerializeValue(object component) { return false; }

			public override void SetValue(object component, object value)
			{
				string oldKey=list[index] as string;
				string newKey=value as string;
				if(newKey==null||newKey==""||oldKey==null||oldKey=="") return;

				newKey=FilterName(newKey);
				if(newKey=="") return;

				if(parameters.Contains(newKey))
				{
					MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				object obj=parameters.Get(oldKey);

				if(obj is Parameters) parameters.Add(newKey, (Parameters)obj);
				else if(obj is long) parameters.Add(newKey, (long)obj);
				else if(obj is double) parameters.Add(newKey, (double)obj);
				else if(obj is string) parameters.Add(newKey, (string)obj);
				else if(obj is bool) parameters.Add(newKey, (bool)obj);
				else if(obj is byte[]) parameters.Add(newKey, (byte[])obj);
				else if(obj is List<long>) parameters.Add(newKey, (List<long>)obj);
				else if(obj is List<double>) parameters.Add(newKey, (List<double>)obj);
				else if(obj is List<string>) parameters.Add(newKey, (List<string>)obj);
				else if(obj is List<bool>) parameters.Add(newKey, (List<bool>)obj);
				else if(obj is List<Parameters>) parameters.Add(newKey, (List<Parameters>)obj);
				else return;

				parameters.Remove(oldKey);

				list[index]=newKey;
			}

			static string FilterName(string name)
			{
				bool ok=true;

				int namelen=name.Length;
				if(namelen<1) return "";

				StringBuilder newName=new StringBuilder();

				// build clean name
				bool first=true;
				foreach(char c in name)
				{
					if(first)
					{
						if(!(c>='a'&&c<='z')&&!(c>='A'&&c<='Z')&&(c!='$')&&(c!='_'))
						{
							ok=false;
							continue;
						}

						newName.Append(c);
						first=false;
					}
					else
					{
						if(!(c>='a'&&c<='z')&&!(c>='A'&&c<='Z')&&!(c>='0'&&c<='9')&&(c!='$')&&(c!='_'))
						{
							ok=false;
							continue;
						}

						newName.Append(c);
					}
				}

				if(!ok) return newName.ToString();
				return name;
			}
		}

		#region ValueAsProperty&Co.
		abstract class ValueAsProperty : ICustomTypeDescriptor
		{
			protected ListBox.ObjectCollection list;
			protected int index;
			protected Parameters parameters;

			public ValueAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters)
			{
				this.list=list;
				this.index=index;
				this.parameters=parameters;
			}

			AttributeCollection ICustomTypeDescriptor.GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }

			string ICustomTypeDescriptor.GetClassName() { return TypeDescriptor.GetClassName(this, true); }

			string ICustomTypeDescriptor.GetComponentName() { return TypeDescriptor.GetComponentName(this, true); }

			TypeConverter ICustomTypeDescriptor.GetConverter() { return TypeDescriptor.GetConverter(this, true); }

			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }

			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(this, true); }

			object ICustomTypeDescriptor.GetEditor(Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }

			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }

			EventDescriptorCollection ICustomTypeDescriptor.GetEvents() { return TypeDescriptor.GetEvents(this, true); }

			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) { return GetProperties(); }

			public abstract PropertyDescriptorCollection GetProperties();

			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) { return this; }
		}

		// TODO Generic
		class ParametersAsProperty : ValueAsProperty
		{
			public ParametersAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(ParametersAsProperty)));
				props.Add(new ParametersPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "Parameters"));
				return props;
			}

			class ParametersPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public ParametersPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(ParametersConverter)),
							new EditorAttribute(typeof(ParametersEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(ParametersAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.ParametersInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(Parameters); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (Parameters)value); }
			}
		}

		class LongAsProperty : ValueAsProperty
		{
			public LongAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(LongAsProperty)));
				props.Add(new LongPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "Long"));
				return props;
			}

			class LongPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public LongPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
							new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(LongAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.LongInParametersDescription; } }

				public override object GetValue(object component)
				{
					return parameters.Get((string)list[index]);
				}

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(long); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (long)value); }
			}
		}

		class DoubleAsProperty : ValueAsProperty
		{
			public DoubleAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(DoubleAsProperty)));
				props.Add(new DoublePropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "Double"));
				return props;
			}

			class DoublePropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public DoublePropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
							new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(DoubleAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.DoubleInParametersDescription; } }

				public override object GetValue(object component)
				{
					return parameters.Get((string)list[index]);
				}

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(double); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (double)value); }
			}
		}

		class StringAsProperty : ValueAsProperty
		{
			public StringAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(StringAsProperty)));
				props.Add(new StringPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "String"));
				return props;
			}

			class StringPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public StringPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new EditorAttribute(typeof(MultilineStringEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(StringAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.StringInParametersDescription; } }

				public override object GetValue(object component)
				{
					return parameters.Get((string)list[index]);
				}

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(string); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (string)value); }
			}
		}

		class BoolAsProperty : ValueAsProperty
		{
			public BoolAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(BoolAsProperty)));
				props.Add(new BoolPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "Boolean"));
				return props;
			}

			class BoolPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public BoolPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes { get { return new AttributeCollection(new CategoryAttribute(category)); } }

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(BoolAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.BoolInParametersDescription; } }

				public override object GetValue(object component)
				{
					return parameters.Get((string)list[index]);
				}

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(bool); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (bool)value); }
			}
		}

		class ByteArrayAsProperty : ValueAsProperty
		{
			public ByteArrayAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(ByteArrayAsProperty)));
				props.Add(new ByteArrayPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "Byte[]"));
				return props;
			}

			class ByteArrayPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public ByteArrayPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(ByteArrayConverter)),
							new EditorAttribute(typeof(ArrayEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(ByteArrayAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.ByteArrayInParametersDescription; } }

				public override object GetValue(object component)
				{
					return parameters.Get((string)list[index]);
				}

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(byte[]); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (byte[])value); }
			}
		}

		class LongListAsProperty : ValueAsProperty
		{
			public LongListAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(LongListAsProperty)));
				props.Add(new LongListPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "LongList"));
				return props;
			}

			class LongListPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public LongListPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(NumberListConverter<long>)),
							new EditorAttribute(typeof(LongListEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(LongListAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.LongListInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(List<long>); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (List<long>)value); }
			}
		}

		class DoubleListAsProperty : ValueAsProperty
		{
			public DoubleListAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(DoubleListAsProperty)));
				props.Add(new DoubleListPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "DoubleList"));
				return props;
			}

			class DoubleListPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public DoubleListPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(NumberListConverter<double>)),
							new EditorAttribute(typeof(DoubleListEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(DoubleListAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.DoubleListInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(List<double>); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (List<double>)value); }
			}
		}

		class StringListAsProperty : ValueAsProperty
		{
			public StringListAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(StringListAsProperty)));
				props.Add(new StringListPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "StringList"));
				return props;
			}

			class StringListPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public StringListPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(MultilineStringListConverter)),
							new EditorAttribute(typeof(MultilineStringListEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(StringListAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.StringListInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(List<string>); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (List<string>)value); }
			}
		}

		class BoolListAsProperty : ValueAsProperty
		{
			public BoolListAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(BoolListAsProperty)));
				props.Add(new BoolListPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "BoolList"));
				return props;
			}

			class BoolListPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public BoolListPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(BoolListConverter)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(BoolListAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.BoolListInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(List<bool>); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (List<bool>)value); }
			}
		}

		class ParametersListAsProperty : ValueAsProperty
		{
			public ParametersListAsProperty(ListBox.ObjectCollection list, int index, Parameters parameters) : base(list, index, parameters) { }

			public override PropertyDescriptorCollection GetProperties()
			{
				PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
				props.Add(new NamePropertyDescriptor(list, index, parameters, "Name", "Name", typeof(ParametersListAsProperty)));
				props.Add(new ParametersListPropertyDescriptor(list, index, parameters, Properties.Resources.ListEditorFormPropertyGridValueNameText, "ParametersList"));
				return props;
			}

			class ParametersListPropertyDescriptor : PropertyDescriptor
			{
				ListBox.ObjectCollection list;
				int index;
				Parameters parameters;
				string name, category;

				public ParametersListPropertyDescriptor(ListBox.ObjectCollection list, int index, Parameters parameters, string name, string category)
					: base(name, null)
				{
					this.list=list;
					this.index=index;
					this.parameters=parameters;
					this.name=name;
					this.category=category;
				}

				public override AttributeCollection Attributes
				{
					get
					{
						return new AttributeCollection(
							new TypeConverterAttribute(typeof(ParametersListConverter)),
							new EditorAttribute(typeof(ParametersListEditor), typeof(UITypeEditor)),
							new CategoryAttribute(category));
					}
				}

				public override bool CanResetValue(object component) { return false; }

				public override Type ComponentType { get { return typeof(ParametersListAsProperty); } }

				public override string DisplayName { get { return name; } }

				public override string Description { get { return Properties.Resources.ParametersListInParametersDescription; } }

				public override object GetValue(object component) { return parameters.Get((string)list[index]); }

				public override bool IsReadOnly { get { return false; } }

				public override string Name { get { return name; } }

				public override Type PropertyType { get { return typeof(List<Parameters>); } }

				public override void ResetValue(object component) { }

				public override bool ShouldSerializeValue(object component) { return false; }

				public override void SetValue(object component, object value) { parameters.Add((string)list[index], (List<Parameters>)value); }
			}
		}
		#endregion

		void listbox_DrawItem(object sender, DrawItemEventArgs e)
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
			string displayText=GetDisplayText(listbox.Items[e.Index]);

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

		void listbox_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtons();
			UpdateListAndLabels();

			if(listbox.SelectedIndex==-1)
			{
				propertyBrowser.SelectedObject=null;
				propertyBrowser.Enabled=false;
				return;
			}

			string key=(string)listbox.SelectedItem;
			object obj=Result.Get(key);

			if(obj is Parameters)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new ParametersAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is long)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new LongAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is double)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new DoubleAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is string)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new StringAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is bool)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new BoolAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is byte[])
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new ByteArrayAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is List<long>)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new LongListAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is List<double>)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new DoubleListAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is List<string>)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new StringListAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is List<bool>)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new BoolListAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else if(obj is List<Parameters>)
			{
				propertyBrowser.Enabled=true;
				propertyBrowser.SelectedObject=new ParametersListAsProperty(listbox.Items, listbox.SelectedIndex, Result);
			}
			else
			{
				propertyBrowser.SelectedObject=null;
				propertyBrowser.Enabled=false;
			}
		}

		void listbox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Delete) removeButton_Click(sender, EventArgs.Empty);
		}

		void propertyBrowser_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
		{
			UpdateListAndLabels();
		}

		void addButton_Click(object sender, EventArgs e)
		{
			contextMenuStripAdd.Show(addButton, 0, 0);
		}

		private void buttonChangeType_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;

			object obj=Result.Get(key);

			if(obj is Parameters) contextMenuStripParametersTo.Show(buttonChangeType, 0, 0);
			else if(obj is long) contextMenuStripIntegerNumberTo.Show(buttonChangeType, 0, 0);
			else if(obj is double) contextMenuStripFloatingPointNumberTo.Show(buttonChangeType, 0, 0);
			else if(obj is string) contextMenuStripStringTo.Show(buttonChangeType, 0, 0);
			else if(obj is bool) contextMenuStripBooleanTo.Show(buttonChangeType, 0, 0);
			else if(obj is List<long>) contextMenuStripListOfIntegerNumbersTo.Show(buttonChangeType, 0, 0);
			else if(obj is List<double>) contextMenuStripListOfFloatingPointNumbersTo.Show(buttonChangeType, 0, 0);
			else if(obj is List<string>) contextMenuStripListOfStringsTo.Show(buttonChangeType, 0, 0);
			else if(obj is List<bool>) contextMenuStripListOfBooleansTo.Show(buttonChangeType, 0, 0);
			else if(obj is List<Parameters>) contextMenuStripListOfParametersTo.Show(buttonChangeType, 0, 0);
		}

		void removeButton_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;

			listbox.SuspendLayout();

			if(index==0&&listbox.Items.Count>1) listbox.SelectedIndex=1;
			else listbox.SelectedIndex=index-1;

			listbox.Items.RemoveAt(index);
			Result.Remove(key);

			listbox_SelectedIndexChanged(sender, e);

			listbox.ResumeLayout();
		}

		void okButton_Click(object sender, EventArgs e)
		{
			DialogResult=DialogResult.OK;
			Close();
		}

		void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult=DialogResult.Cancel;
			Close();
		}

		#region Add
		private void parametersToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormParametersDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new Parameters());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void booleanToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormBoolDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, false);

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void integerNumberToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormLongDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, (long)0);

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void floatingPointNumberToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormDoubleDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, (double)0);

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void stringToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormStringDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, string.Empty);

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void byteArrayToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormByteArrayDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new byte[0]);

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void listOfBooleansToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormBoolListDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new List<bool>());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void listOfIntegerNumbersToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormLongListDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new List<long>());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void listOfFloatingPointNumbersToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormDoubleListDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new List<double>());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void listOfStringsToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormStringListDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new List<string>());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}

		private void listOfParametersToolStripMenuItemAdd_Click(object sender, EventArgs e)
		{
			ParametersNameInputForm dlg=new ParametersNameInputForm(Properties.Resources.ParametersEditorFormParametersListDefaultName);
			if(dlg.ShowDialog()!=DialogResult.OK) return;

			if(Result.Contains(dlg.Result))
			{
				MessageBox.Show(Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxText, Properties.Resources.ParametersEditorFormNameInUseErrorMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Result.Add(dlg.Result, new List<Parameters>());

			listbox.SuspendLayout();
			listbox.Items.Add(dlg.Result);
			listbox.SelectedIndex=listbox.Items.Count-1;
			listbox.ResumeLayout();
		}
		#endregion

		#region string <==> List<string>
		private void listOfStringsToolStripMenuItemStringTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			string value=Result.GetString(key);
			List<string> newValue=new List<string>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void stringToolStripMenuItemStringListTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<string> value=Result.GetStringList(key);
			if(value==null||value.Count==0) Result.Add(key, string.Empty);
			else if(value.Count==1) Result.Add(key, value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion

		#region bool <==> List<bool>
		private void listOfBooleansToolStripMenuItemBooleanTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			bool value=Result.GetBool(key);
			List<bool> newValue=new List<bool>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void booleanToolStripMenuItemListOfBooleansTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<bool> value=Result.GetBoolList(key);
			if(value==null||value.Count==0) Result.Add(key, false);
			else if(value.Count==1) Result.Add(key, value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion

		#region long/double/List<long>/List<double>
		#region IntegerNumberTo
		private void floatingPointNumberToolStripMenuItemIntegerNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			long value=Result.GetInt(key);
			Result.Add(key, (double)value);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfIntegerNumbersToolStripMenuItemIntegerNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			long value=Result.GetInt(key);
			List<long> newValue=new List<long>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfFloatingPointNumbersToolStripMenuItemIntegerNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			long value=Result.GetInt(key);
			List<double> newValue=new List<double>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion

		#region ListOfIntegerNumbersTo
		private void integerNumberToolStripMenuItemListOfIntegerNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<long> value=Result.GetIntList(key);
			if(value==null||value.Count==0) Result.Add(key, 0);
			else if(value.Count==1) Result.Add(key, value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}

		private void floatingPointNumberToolStripMenuItemListOfIntegerNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<long> value=Result.GetIntList(key);
			if(value==null||value.Count==0) Result.Add(key, 0.0);
			else if(value.Count==1) Result.Add(key, (double)value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, (double)value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfFloatingPointNumbersToolStripMenuItemListOfIntegerNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<long> value=Result.GetIntList(key);
			List<double> newValue=new List<double>();
			for(int i=0; i<value.Count; i++) newValue.Add(value[i]);

			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion

		#region FloatingPointNumberTo
		private void integerNumberToolStripMenuItemFloatingPointNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			double value=Result.GetDouble(key);
			Result.Add(key, (long)Math.Round(value));

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfIntegerNumbersToolStripMenuItemFloatingPointNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			double value=Result.GetDouble(key);
			List<long> newValue=new List<long>();
			newValue.Add((long)Math.Round(value));
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfFloatingPointNumbersToolStripMenuItemFloatingPointNumberTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			double value=Result.GetDouble(key);
			List<double> newValue=new List<double>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion

		#region ListOfFloatingPointNumbersTo
		private void integerNumberToolStripMenuItemListOfFloatingPointNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<double> value=Result.GetDoubleList(key);
			if(value==null||value.Count==0) Result.Add(key, 0);
			else if(value.Count==1) Result.Add(key, (long)Math.Round(value[0]));
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, (long)Math.Round(value[0]));
			}

			listbox_SelectedIndexChanged(sender, e);
		}

		private void floatingPointNumberToolStripMenuItemListOfFloatingPointNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<double> value=Result.GetDoubleList(key);
			if(value==null||value.Count==0) Result.Add(key, 0.0);
			else if(value.Count==1) Result.Add(key, value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}

		private void listOfIntegerNumbersToolStripMenuItemListOfFloatingPointNumbersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<double> value=Result.GetDoubleList(key);
			List<long> newValue=new List<long>();
			for(int i=0; i<value.Count; i++) newValue.Add((long)Math.Round(value[i]));

			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion
		#endregion

		#region Parameters <==> List<Parameters>
		private void listOfParametersToolStripMenuItemParametersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			Parameters value=Result.GetParameters(key);
			List<Parameters> newValue=new List<Parameters>();
			newValue.Add(value);
			Result.Add(key, newValue);

			listbox_SelectedIndexChanged(sender, e);
		}

		private void parametersToolStripMenuItemListOfParametersTo_Click(object sender, EventArgs e)
		{
			int index=listbox.SelectedIndex;
			if(index<0&&index>=listbox.Items.Count) return;

			string key=(string)listbox.SelectedItem;
			List<Parameters> value=Result.GetParametersList(key);
			if(value==null||value.Count==0) Result.Add(key, new Parameters());
			else if(value.Count==1) Result.Add(key, value[0]);
			else
			{
				if(MessageBox.Show(Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxText,
					Properties.Resources.ParameterEditorFormListConversionLossOfDataWarningMessageBoxCaption,
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning)!=DialogResult.Yes) return;

				Result.Add(key, value[0]);
			}

			listbox_SelectedIndexChanged(sender, e);
		}
		#endregion
	}
}
