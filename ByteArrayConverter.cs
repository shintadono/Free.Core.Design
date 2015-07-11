using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace Free.Core.Design
{
	public class ByteArrayConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is byte[]) return string.Format(Properties.Resources.FormatCountInParentheses, ((byte[])value).Length);
			return base.ConvertTo(context, culture, value, destType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			byte[] array=value as byte[];
			if(array==null||array.Length>1024) return base.GetProperties(context, value, attributes);

			Attribute[] propAttributes=new Attribute[] {
				new DescriptionAttribute(Properties.Resources.ByteInArrayDescription),
				new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
				new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor))
			};

			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
			for(int i=0; i<array.Length; i++) props.Add(new ByteInArrayPropertyDescriptor(array, i, propAttributes));
			return props;
		}

		class ByteInArrayPropertyDescriptor : PropertyDescriptor
		{
			int index=-1;
			byte old;

			public ByteInArrayPropertyDescriptor(byte[] parent, int index, params Attribute[] attribute)
				: base(string.Format(Properties.Resources.FormatListConverterIndexInParenthese, index), attribute)
			{
				old=parent[index];
				this.index=index;
			}

			public override bool CanResetValue(object component) { return true; }

			public override Type ComponentType { get { return typeof(byte[]); } }

			public override string DisplayName { get { return string.Format(Properties.Resources.FormatListConverterIndexInParenthese, index); } }

			public override object GetValue(object component) { return ((byte[])component)[index]; }

			public override bool IsReadOnly { get { return false; } }

			public override string Name { get { return string.Format(Properties.Resources.FormatListConverterIndexInParenthese, index); } }

			public override Type PropertyType { get { return typeof(byte); } }

			public override void ResetValue(object component) { ((byte[])component)[index]=old; }

			public override bool ShouldSerializeValue(object component) { return true; }

			public override void SetValue(object component, object value) { ((byte[])component)[index]=(byte)value; }
		}
	}
}
