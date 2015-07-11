using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Free.Core.Collections;

namespace Free.Core.Design
{
	class TypeInParametersPropertyDescriptor<T> : PropertyDescriptor
	{
		string key;
		T old;

		public TypeInParametersPropertyDescriptor(string key, T old, params Attribute[] attributes)
			: base(key, attributes)
		{
			this.old=old;
			this.key=key;
		}

		public override bool CanResetValue(object component) { return true; }

		public override Type ComponentType { get { return typeof(Parameters); } } // the Parameters containing the value

		public override string Description
		{
			get
			{
				if(!string.IsNullOrEmpty(base.Description)) return base.Description;
				return string.Format(Properties.Resources.TypeInParametersDescription, typeof(T).Name);
			}
		}

		public override object GetValue(object component) { return ((Parameters)component).Get(key); }

		public override bool IsReadOnly { get { return false; } }

		public override Type PropertyType { get { return typeof(T); } } // the type of the value inside a Parameters

		public override void ResetValue(object component) { SetValue(component, old); }

		public override bool ShouldSerializeValue(object component) { return true; }

		public override void SetValue(object component, object value)
		{
			Parameters param=(Parameters)component;

			if(value is bool) param.Add(key, (bool)value);
			else if(value is long) param.Add(key, (long)value);
			else if(value is double) param.Add(key, (double)value);
			else if(value is string) param.Add(key, (string)value);
			else if(value is Parameters) param.Add(key, (Parameters)value);
			else if(value is List<bool>) param.Add(key, (List<bool>)value);
			else if(value is List<long>) param.Add(key, (List<long>)value);
			else if(value is List<double>) param.Add(key, (List<double>)value);
			else if(value is List<string>) param.Add(key, (List<string>)value);
			else if(value is List<Parameters>) param.Add(key, (List<Parameters>)value);
			else if(value is byte[]) param.Add(key, (byte[])value);

			// ignore everything else
		}
	}
}
