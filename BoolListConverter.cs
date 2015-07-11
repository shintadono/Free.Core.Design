using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Free.Core.Design
{
	public class BoolListConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is List<bool>) return string.Format(Properties.Resources.FormatCountInParentheses, ((List<bool>)value).Count);
			return base.ConvertTo(context, culture, value, destType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			List<bool> list=value as List<bool>;
			if(list==null) return base.GetProperties(context, value, attributes);

			Attribute[] propAttributes=new Attribute[] { new DescriptionAttribute(Properties.Resources.BoolInListDescription) };

			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
			for(int i=0; i<list.Count; i++) props.Add(new TypeInTypedListPropertyDescriptor<bool>(list, i, propAttributes));
			return props;
		}
	}
}
