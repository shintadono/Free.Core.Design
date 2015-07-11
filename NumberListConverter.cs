using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace Free.Core.Design
{
	public class NumberListConverter<T> : ExpandableObjectConverter where T: struct
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is List<T>) return string.Format(Properties.Resources.FormatCountInParentheses, ((List<T>)value).Count);
			return base.ConvertTo(context, culture, value, destType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			List<T> list=value as List<T>;
			if(list==null) return base.GetProperties(context, value, attributes);

			Attribute[] propAttributes=new Attribute[] {
				new DescriptionAttribute(Properties.Resources.NumberInListDescription),
				new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
				new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor))
			};

			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
			for(int i=0; i<list.Count; i++) props.Add(new TypeInTypedListPropertyDescriptor<T>(list, i, propAttributes));
			return props;
		}
	}
}
