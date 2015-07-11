using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public class ParametersListConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is List<Parameters>) return string.Format(Properties.Resources.FormatCountInParentheses, ((List<Parameters>)value).Count);
			return base.ConvertTo(context, culture, value, destType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			List<Parameters> list=value as List<Parameters>;
			if(list==null) return base.GetProperties(context, value, attributes);

			Attribute[] propAttributes=new Attribute[] {
				new DescriptionAttribute(Properties.Resources.ParametersInListDescription),
				new TypeConverterAttribute(typeof(ParametersConverter)),
				new EditorAttribute(typeof(ParametersEditor), typeof(UITypeEditor))
			};

			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);
			for(int i=0; i<list.Count; i++) props.Add(new TypeInTypedListPropertyDescriptor<Parameters>(list, i, propAttributes));
			return props;
		}
	}
}
