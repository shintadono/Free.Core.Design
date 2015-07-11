using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Globalization;

namespace Free.Core.Design
{
	public class MultilineStringListConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is List<string>) return string.Format(Properties.Resources.FormatCountInParentheses, ((List<string>)value).Count);
			return base.ConvertTo(context, culture, value, destType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			List<string> list=value as List<string>;
			if(list==null) return base.GetProperties(context, value, attributes);

			Attribute[] propAttributes=new Attribute[] {
				new DescriptionAttribute(Properties.Resources.StringInListDescription),
				new EditorAttribute(typeof(MultilineStringEditor), typeof(UITypeEditor))
			};

			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
			for(int i=0; i<list.Count; i++) props.Add(new TypeInTypedListPropertyDescriptor<string>(list, i, propAttributes));
			return props;
		}
	}
}
