using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public class ParametersEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if(context==null||context.Instance==null) return base.GetEditStyle(context);
			return typeof(Parameters).IsAssignableFrom(context.PropertyDescriptor.PropertyType)?UITypeEditorEditStyle.Modal:UITypeEditorEditStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if(value==null) return value;
			if(!(value is Parameters)) return value;

			ParametersEditorForm dlg=new ParametersEditorForm(value as Parameters);

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
