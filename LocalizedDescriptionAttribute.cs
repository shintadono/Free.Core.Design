using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Free.Core.Design
{
	/// <summary>
	/// Specifies the localized description that will be displayed in the description area of a property grid when the property or event will be displayed in that property grid.
	/// </summary>
	[AttributeUsage(AttributeTargets.All)]
	public class LocalizedDescriptionAttribute : DescriptionAttribute
	{
		string table;
		Assembly assembly;

		/// <summary>
		/// Initializes a new instance of the <see cref='LocalizedDescriptionAttribute'/> class with the specified description text source.
		/// </summary>
		/// <param name="displayName">Resource name, or description text, in case the resource is not found.</param>
		/// <param name="table">Resource table namespace.</param>
		/// <param name="type">A type, that is defined in the assembly that contains the resource table.</param>
		public LocalizedDescriptionAttribute(string description, string table, Type type)
			: base(description)
		{
			this.table=table;
			assembly=type.Assembly;
		}

		/// <summary>
		/// Gets the localized description stored in this attribute.
		/// </summary>
		public override string Description
		{
			get
			{
				try
				{
					return new ResourceManager(table, assembly).GetString(base.Description)??base.Description;
				}
				catch
				{
					return base.Description;
				}
			}
		}
	}
}
