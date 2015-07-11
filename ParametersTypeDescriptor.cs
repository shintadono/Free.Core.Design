using System;
using System.ComponentModel;
using Free.Core.Collections;

namespace Free.Core.Design
{
	public class ParametersTypeDescriptor : ICustomTypeDescriptor
	{
		Parameters content=new Parameters();

		public ParametersTypeDescriptor()
		{
		}

		public ParametersTypeDescriptor(Parameters param)
		{
			content=param;
		}

		public override string ToString() { return string.Format(Properties.Resources.FormatCountInParentheses, content.Count); }

		#region ICustomTypeDescriptor
		AttributeCollection ICustomTypeDescriptor.GetAttributes() { return TypeDescriptor.GetAttributes(this, true); }

		string ICustomTypeDescriptor.GetClassName() { return TypeDescriptor.GetClassName(this, true); }

		string ICustomTypeDescriptor.GetComponentName() { return TypeDescriptor.GetComponentName(this, true); }

		TypeConverter ICustomTypeDescriptor.GetConverter() { return TypeDescriptor.GetConverter(this, true); }

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent() { return TypeDescriptor.GetDefaultEvent(this, true); }

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty() { return TypeDescriptor.GetDefaultProperty(this, true); }

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType) { return TypeDescriptor.GetEditor(this, editorBaseType, true); }

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes) { return TypeDescriptor.GetEvents(this, attributes, true); }

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents() { return TypeDescriptor.GetEvents(this, true); }

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) { return ((ICustomTypeDescriptor)this).GetProperties(); }

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			if(content==null||content.Count==0) return new PropertyDescriptorCollection(null);
			return ParametersConverter.GenPropertyDescriptorCollection(content);
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd) { return this; }
		#endregion
	}
}
