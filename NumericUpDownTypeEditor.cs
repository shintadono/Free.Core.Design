using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Free.Core.Design
{
	public class NumericUpDownTypeEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if(context==null||context.Instance==null) return base.GetEditStyle(context);
			return context.PropertyDescriptor.IsReadOnly?UITypeEditorEditStyle.None:UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			try
			{
				if(context==null||context.Instance==null||provider==null) return value;

				//use IWindowsFormsEditorService object to display a control in the dropdown area
				IWindowsFormsEditorService frmsrv=(IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if(frmsrv==null) return value;

				NumericUpDown nmr;

				NumericUpDownTypeSettingsAttribute attr=(NumericUpDownTypeSettingsAttribute)context.PropertyDescriptor.Attributes[typeof(NumericUpDownTypeSettingsAttribute)];
				if(attr==null)
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

					decimal v=(decimal)Convert.ChangeType(value, typeof(decimal));

					int decimalPlaces=Math.Min((int)BitConverter.GetBytes(decimal.GetBits(v)[3])[2], 10);

					nmr=new NumericUpDown
						{
							Size=new Size(60, 120),
							Minimum=Min,
							Maximum=Max,
							Increment=decimal.One,
							DecimalPlaces=decimalPlaces,
							Value=v
						};
				}
				else
				{
					decimal v=attr.PutInRange(value);

					int decimalPlaces=attr.DecimalPlaces;
					if(decimalPlaces<0) decimalPlaces=Math.Min((int)BitConverter.GetBytes(decimal.GetBits(v)[3])[2], 10);

					nmr=new NumericUpDown
						{
							Size=new Size(60, 120),
							Minimum=attr.Min,
							Maximum=attr.Max,
							Increment=attr.Increment,
							DecimalPlaces=decimalPlaces,
							Value=v
						};
				}

				frmsrv.DropDownControl(nmr);
				context.OnComponentChanged();

				return Convert.ChangeType(nmr.Value, context.PropertyDescriptor.PropertyType);
			}
			catch { }

			return value;
		}
	}
}
