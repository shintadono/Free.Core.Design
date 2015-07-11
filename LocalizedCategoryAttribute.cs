using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Free.Core.Design
{
	/// <summary>
	/// Specifies the localized category in which the property or event will be displayed in a property grid.
	/// </summary>
	[AttributeUsage(AttributeTargets.All)]
	public class LocalizedCategoryAttribute : CategoryAttribute
	{
		string table;
		Assembly assembly;

		/// <summary>
		/// Initializes a new instance of the <see cref='LocalizedCategoryAttribute'/> class with the specified category text source.
		/// </summary>
		/// <param name="displayName">Resource name, or category text, in case the resource is not found.</param>
		/// <param name="table">Resource table namespace.</param>
		/// <param name="type">A type, that is defined in the assembly that contains the resource table.</param>
		public LocalizedCategoryAttribute(string category, string table, Type type) : base(category)
		{
			this.table=table;
			assembly=type.Assembly;
		}

		/// <summary>
		/// Gets the localized text of the category for the property or event that this attribute is bound to.
		/// </summary>
		protected override string GetLocalizedString(string value)
		{
			try
			{
				return new ResourceManager(table, assembly).GetString(value)??base.GetLocalizedString(value);
			}
			catch
			{
				return base.GetLocalizedString(value);
			}
		}
	}
}
