using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Free.Core.Design
{
	public class MultilineStringListEditor : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if(context==null||context.Instance==null) return base.GetEditStyle(context);
			return context.PropertyDescriptor.PropertyType==typeof(List<string>)?UITypeEditorEditStyle.Modal:UITypeEditorEditStyle.None;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if(value==null) return value;
			if(!(value is List<string>)) return value;

			TypedListEditorForm<string> dlg=new TypedListEditorForm<string>(value as List<string>, Properties.Resources.ListOfStringEditorCaption,
				(v) =>
				{
					string str=v as string;
					if(str==null) return string.Empty;
					return string.Format(Properties.Resources.FormatStringInDoublequotes, str);
				},
				() => { return string.Empty; },
				new CategoryAttribute(Properties.Resources.ListOfStringEditorGridValueTypeText),
				new DescriptionAttribute(Properties.Resources.StringInListDescription),
				new EditorAttribute(typeof(MultilineStringEditor), typeof(UITypeEditor))
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
