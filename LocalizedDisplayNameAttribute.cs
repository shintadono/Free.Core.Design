using System;
using System.ComponentModel;
using System.Reflection;
using System.Resources;

namespace Free.Core.Design
{
	/// <summary>
	/// Specifies the localized display name that will be displayed in a property grid and its description area when the property or event will be displayed in that property grid.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Event|AttributeTargets.Class|AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
	public class LocalizedDisplayNameAttribute : DisplayNameAttribute
	{
		string table;
		Assembly assembly;

		/// <summary>
		/// Initializes a new instance of the <see cref='LocalizedDisplayNameAttribute'/> class with the specified display name source.
		/// </summary>
		/// <param name="displayName">Resource name, or display name, in case the resource is not found.</param>
		/// <param name="table">Resource table namespace.</param>
		/// <param name="type">A type, that is defined in the assembly that contains the resource table.</param>
		public LocalizedDisplayNameAttribute(string displayName, string table, Type type)
			: base(displayName)
		{
			this.table=table;
			assembly=type.Assembly;
		}

		/// <summary>
		/// Gets the localized display name for a property, event, or public void method that takes no arguments stored in this attribute.
		/// </summary>
		public override string DisplayName
		{
			get
			{
				try
				{
					return new ResourceManager(table, assembly).GetString(base.DisplayName)??base.DisplayName;
				}
				catch
				{
					return base.DisplayName;
				}
			}
		}
	}
}
