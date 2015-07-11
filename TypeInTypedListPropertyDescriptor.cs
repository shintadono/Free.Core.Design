using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Free.Core.Design
{
	class TypeInTypedListPropertyDescriptor<T> : PropertyDescriptor
	{
		int index=-1;
		T old;

		public TypeInTypedListPropertyDescriptor(List<T> parent, int index, params Attribute[] attribute)
			: base(string.Format(Properties.Resources.FormatListConverterIndexInParenthese, index), attribute)
		{
			old=parent[index];
			this.index=index;
		}

		public override bool CanResetValue(object component) { return true; }

		public override Type ComponentType { get { return typeof(List<T>); } }

		public override string Description
		{
			get
			{
				if(!string.IsNullOrEmpty(base.Description)) return base.Description;
				return string.Format(Properties.Resources.TypeInTypedListDescription, typeof(T).Name);
			}
		}

		public override object GetValue(object component) { return ((List<T>)component)[index]; }

		public override bool IsReadOnly { get { return false; } }

		public override Type PropertyType { get { return typeof(T); } }

		public override void ResetValue(object component) { ((List<T>)component)[index]=old; }

		public override bool ShouldSerializeValue(object component) { return true; }

		public override void SetValue(object component, object value) { ((List<T>)component)[index]=(T)value; }
	}
}
