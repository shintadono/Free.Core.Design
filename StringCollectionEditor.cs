using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Free.Core.Design
{
	/// <summary>
	/// This Editor can be used on <b>string</b>[] and list of <b>string</b>, but is doesn't support multiline strings. Use <see cref="MultilineStringListEditor"/> instead.
	/// </summary>
	public class StringCollectionEditor : CollectionEditor
	{
		public StringCollectionEditor() : base(typeof(List<string>)) { }

		protected override object CreateInstance(Type itemType)
		{
			if(itemType==typeof(string)) return string.Empty;
			return base.CreateInstance(itemType);
		}

		protected override string GetDisplayText(object value)
		{
			if(value is string) return string.Format(Properties.Resources.FormatStringInDoublequotes, value);
			return base.GetDisplayText(value);
		}
	}
}
