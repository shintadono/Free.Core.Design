using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Globalization;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public class ParametersConverter : ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
		{
			if(destType==typeof(string)&&value is Parameters) return string.Format(Properties.Resources.FormatCountInParentheses, ((Parameters)value).Count);
			return base.ConvertTo(context, culture, value, destType);
		}

		public static PropertyDescriptorCollection GenPropertyDescriptorCollection(Parameters param)
		{
			PropertyDescriptorCollection props=new PropertyDescriptorCollection(null);

			foreach(string key in param.Keys)
			{
				object obj=param.Get(key);
				if(obj is Parameters)
				{
					TypeInParametersPropertyDescriptor<Parameters> prop=new TypeInParametersPropertyDescriptor<Parameters>(key, (Parameters)obj,
						new TypeConverterAttribute(typeof(ParametersConverter)),
						new EditorAttribute(typeof(ParametersEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.ParametersInParametersDescription));
					props.Add(prop);
				}
				else if(obj is long)
				{
					TypeInParametersPropertyDescriptor<long> prop=new TypeInParametersPropertyDescriptor<long>(key, (long)obj,
						new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor)),
						new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
						new DescriptionAttribute(Properties.Resources.LongInParametersDescription));
					props.Add(prop);
				}
				else if(obj is double)
				{
					TypeInParametersPropertyDescriptor<double> prop=new TypeInParametersPropertyDescriptor<double>(key, (double)obj,
						new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor)),
						new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
						new DescriptionAttribute(Properties.Resources.DoubleInParametersDescription));
					props.Add(prop);
				}
				else if(obj is string)
				{
					TypeInParametersPropertyDescriptor<string> prop=new TypeInParametersPropertyDescriptor<string>(key, (string)obj,
						new EditorAttribute(typeof(MultilineStringEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.StringInParametersDescription));
					props.Add(prop);
				}
				else if(obj is bool)
				{
					TypeInParametersPropertyDescriptor<bool> prop=new TypeInParametersPropertyDescriptor<bool>(key, (bool)obj,
						new DescriptionAttribute(Properties.Resources.BoolInParametersDescription));
					props.Add(prop);
				}
				else if(obj is byte[])
				{
					TypeInParametersPropertyDescriptor<byte[]> prop=new TypeInParametersPropertyDescriptor<byte[]>(key, (byte[])obj,
						new TypeConverterAttribute(typeof(ByteArrayConverter)),
						new EditorAttribute(typeof(ArrayEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.ByteArrayInParametersDescription));
					props.Add(prop);
				}
				else if(obj is List<long>)
				{
					TypeInParametersPropertyDescriptor<List<long>> prop=new TypeInParametersPropertyDescriptor<List<long>>(key, (List<long>)obj,
						new TypeConverterAttribute(typeof(NumberListConverter<long>)),
						new EditorAttribute(typeof(LongListEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.LongListInParametersDescription));
					props.Add(prop);
				}
				else if(obj is List<double>)
				{
					TypeInParametersPropertyDescriptor<List<double>> prop=new TypeInParametersPropertyDescriptor<List<double>>(key, (List<double>)obj,
						new TypeConverterAttribute(typeof(NumberListConverter<double>)),
						new EditorAttribute(typeof(DoubleListEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.DoubleListInParametersDescription));
					props.Add(prop);
				}
				else if(obj is List<string>)
				{
					TypeInParametersPropertyDescriptor<List<string>> prop=new TypeInParametersPropertyDescriptor<List<string>>(key, (List<string>)obj,
						new TypeConverterAttribute(typeof(MultilineStringListConverter)),
						new EditorAttribute(typeof(MultilineStringListEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.StringListInParametersDescription));
					props.Add(prop);
				}
				else if(obj is List<bool>)
				{
					TypeInParametersPropertyDescriptor<List<bool>> prop=new TypeInParametersPropertyDescriptor<List<bool>>(key, (List<bool>)obj,
						new TypeConverterAttribute(typeof(BoolListConverter)),
						new DescriptionAttribute(Properties.Resources.BoolListInParametersDescription));
					props.Add(prop);
				}
				else if(obj is List<Parameters>)
				{
					TypeInParametersPropertyDescriptor<List<Parameters>> prop=new TypeInParametersPropertyDescriptor<List<Parameters>>(key, (List<Parameters>)obj,
						new TypeConverterAttribute(typeof(ParametersListConverter)),
						new EditorAttribute(typeof(ParametersListEditor), typeof(UITypeEditor)),
						new DescriptionAttribute(Properties.Resources.ParametersListInParametersDescription));
					props.Add(prop);
				}
			}

			return props;
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			Parameters param=value as Parameters;
			if(param==null) return base.GetProperties(context, value, attributes);

			return GenPropertyDescriptorCollection(param);
		}
	}
}
