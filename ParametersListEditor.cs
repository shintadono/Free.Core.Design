using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public class ParametersListEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if(context==null||context.Instance==null) return base.GetEditStyle(context);
			return context.PropertyDescriptor.PropertyType==typeof(List<Parameters>)?UITypeEditorEditStyle.Modal:UITypeEditorEditStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if(value==null) return value;
			if(!(value is List<Parameters>)) return value;

			TypedListEditorForm<Parameters> dlg=new TypedListEditorForm<Parameters>(value as List<Parameters>, Properties.Resources.ListOfParametersEditorCaption,
				(v) =>
				{
					Parameters param=v as Parameters;
					if(param==null) return value.ToString();

					string ret=param.GetString("$name$", null);
					if(ret!=null) return ret;

					ret=param.GetString("Name", null);
					if(ret!=null) return ret;

					ret=param.GetString("name", null);
					if(ret!=null) return ret;

					return string.Format(Properties.Resources.FormatCountInParentheses, param.Count);
				},
				() => { return new Parameters(); },
				new CategoryAttribute(Properties.Resources.ListOfParametersEditorGridValueTypeText),
				new DescriptionAttribute(Properties.Resources.ParametersInListDescription),
				new TypeConverterAttribute(typeof(ParametersConverter)),
				new EditorAttribute(typeof(ParametersEditor), typeof(UITypeEditor))
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
