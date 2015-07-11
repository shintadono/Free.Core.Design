using System;
using System.ComponentModel;
using System.Globalization;

namespace Free.Core.Design
{
	/// <summary>
	/// Range modification for direct edit override
	/// </summary>
	public class NumericUpDownTypeConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			// Attempt to do them all
			return true;
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			try
			{
				string Value=value as string;
				if(Value==null) Value=Convert.ChangeType(value, context.PropertyDescriptor.PropertyType).ToString();

				decimal decimalValue;
				if(!decimal.TryParse(Value, out decimalValue)) decimalValue=decimal.Zero;

				NumericUpDownTypeSettingsAttribute settingsAttribute=(NumericUpDownTypeSettingsAttribute)context.PropertyDescriptor.Attributes[typeof(NumericUpDownTypeSettingsAttribute)];
				if(settingsAttribute!=null) decimalValue=settingsAttribute.PutInRange(decimalValue);
				else
				{
					// check context.PropertyDescriptor.PropertyType and make Range depending on the type
					Type type=context.PropertyDescriptor.PropertyType;

					decimal Min=0;
					decimal Max=100;

					if(type==typeof(byte)) { Min=byte.MinValue; Max=byte.MaxValue; }
					else if(type==typeof(sbyte)) { Min=sbyte.MinValue; Max=sbyte.MaxValue; }
					else if(type==typeof(short)) { Min=short.MinValue; Max=short.MaxValue; }
					else if(type==typeof(ushort)) { Min=ushort.MinValue; Max=ushort.MaxValue; }
					else if(type==typeof(int)) { Min=int.MinValue; Max=int.MaxValue; }
					else if(type==typeof(uint)) { Min=uint.MinValue; Max=uint.MaxValue; }
					else if(type==typeof(long)) { Min=long.MinValue; Max=long.MaxValue; }
					else if(type==typeof(ulong)) { Min=ulong.MinValue; Max=ulong.MaxValue; }
					else if(type==typeof(float)||type==typeof(double)||type==typeof(decimal)) { Min=decimal.MinValue; Max=decimal.MaxValue; }
					else { Min=0; Max=100; }

					if(decimalValue>Max) decimalValue=Max;
					else if(decimalValue<Min) decimalValue=Min;
				}

				return Convert.ChangeType(decimalValue, context.PropertyDescriptor.PropertyType);
			}
			catch
			{
				return base.ConvertFrom(context, culture, value);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			try
			{
				return destinationType==typeof(string)?Convert.ChangeType(value, context.PropertyDescriptor.PropertyType).ToString():Convert.ChangeType(value, destinationType);
			}
			catch { }

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
