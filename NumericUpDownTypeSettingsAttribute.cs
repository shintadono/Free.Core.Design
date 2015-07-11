using System;

namespace Free.Core.Design
{
	/// <summary>
	/// Attribute to allow ranges to be added to the numeric updowner
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class NumericUpDownTypeSettingsAttribute : Attribute
	{
		public decimal Min { get; private set; }
		public decimal Max { get; private set; }
		public decimal Increment { get; private set; }
		public int DecimalPlaces { get; private set; }

		/// <summary>
		/// Use to make a simple ulong maximum. Starts at 0, increment by 1.
		/// </summary>
		/// <param name="max"></param>
		public NumericUpDownTypeSettingsAttribute(ulong max)
			: this(decimal.Zero, (decimal)max) { }

		/// <summary>
		/// Use to make a simple long (or default conversion) based range.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="increment">Default is 1.</param>
		public NumericUpDownTypeSettingsAttribute(long min, long max, int increment=1)
			: this((decimal)min, (decimal)max, increment) { }

		/// <summary>
		/// Set the Min, Max, increment, and decimal places to be used.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="increment"></param>
		/// <param name="decimalPlaces"></param>
		public NumericUpDownTypeSettingsAttribute(decimal min, decimal max, decimal increment=decimal.One, int decimalPlaces=0)
		{
			Min=min;
			Max=max;
			Increment=increment;
			DecimalPlaces=decimalPlaces;
		}

		public NumericUpDownTypeSettingsAttribute(Type type, double increment=1, int decimalPlaces=-1)
		{
			Increment=(decimal)increment;
			if(decimalPlaces>=0) DecimalPlaces=decimalPlaces;
			else DecimalPlaces=0;

			if(type==typeof(byte)) { Min=byte.MinValue; Max=byte.MaxValue; }
			else if(type==typeof(sbyte)) { Min=sbyte.MinValue; Max=sbyte.MaxValue; }
			else if(type==typeof(short)) { Min=short.MinValue; Max=short.MaxValue; }
			else if(type==typeof(ushort)) { Min=ushort.MinValue; Max=ushort.MaxValue; }
			else if(type==typeof(int)) { Min=int.MinValue; Max=int.MaxValue; }
			else if(type==typeof(uint)) { Min=uint.MinValue; Max=uint.MaxValue; }
			else if(type==typeof(long)) { Min=long.MinValue; Max=long.MaxValue; }
			else if(type==typeof(ulong)) { Min=ulong.MinValue; Max=ulong.MaxValue; }
			else if(type==typeof(float)||type==typeof(double)||type==typeof(decimal)) { Min=decimal.MinValue; Max=decimal.MaxValue; if(decimalPlaces<0) DecimalPlaces=-1; }
			else { Min=0; Max=100; }
		}

		/// <summary>
		/// Validation function to check if the value is withtin the range (inclusive)
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool IsInRange(object value)
		{
			decimal checkedValue=(decimal)Convert.ChangeType(value, typeof(decimal));
			return checkedValue<=Max&&checkedValue>=Min;
		}

		/// <summary>
		/// Takes the value and adjusts if it is out of bounds.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public decimal PutInRange(object value)
		{
			decimal checkedValue=(decimal)Convert.ChangeType(value, typeof(decimal));
			if(checkedValue>Max) checkedValue=Max;
			else if(checkedValue<Min) checkedValue=Min;
			return checkedValue;
		}
	}
}
