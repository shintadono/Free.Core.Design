using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Free.Core.Design
{
	public class DoubleListEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if(context==null||context.Instance==null) return base.GetEditStyle(context);
			return context.PropertyDescriptor.PropertyType==typeof(List<double>)?UITypeEditorEditStyle.Modal:UITypeEditorEditStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if(value==null) return value;
			if(!(value is List<double>)) return value;

			TypedListEditorForm<double> dlg=new TypedListEditorForm<double>(value as List<double>, Properties.Resources.ListOfDoubleEditorCaption, (v) => { return v.ToString(); }, () => { return 0.0; },
				new CategoryAttribute(Properties.Resources.ListOfDoubleEditorGridValueTypeText),
				new DescriptionAttribute(Properties.Resources.NumberInListDescription),
				new TypeConverterAttribute(typeof(NumericUpDownTypeConverter)),
				new EditorAttribute(typeof(NumericUpDownTypeEditor), typeof(UITypeEditor))
			);

			IWindowsFormsEditorService editorService=(IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			DialogResult res;
			if(editorService!=null) res=editorService.ShowDialog(dlg);
			else res=dlg.ShowDialog();

			if(res!=DialogResult.OK) return value;

			context.OnComponentChanged();
			return dlg.Result;
		}
	}
}
