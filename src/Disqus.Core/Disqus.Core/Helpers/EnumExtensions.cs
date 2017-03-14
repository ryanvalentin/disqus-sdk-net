using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Disqus.Core.Helpers
{
	internal static class EnumExtensions
	{
		/// <summary>
		/// Extension that converts enum to its Parameter value if set
		/// </summary>
		internal static string ToArgument(this Enum value)
		{
			string output = null;
			Type type = value.GetType();

			FieldInfo field = type.GetRuntimeField(value.ToString());
			ArgumentValue[] attributes = field.GetCustomAttributes(typeof(ArgumentValue), false) as ArgumentValue[];

			if (attributes.Length > 0)
				output = attributes[0].Value;

			return output ?? value.ToString();
		}

		/// <summary>
		/// Gets a list of flags from an enum
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		internal static IEnumerable<Enum> GetFlags(this Enum input)
		{
			foreach (Enum value in Enum.GetValues(input.GetType()))
				if (input.HasFlag(value))
					yield return value;
		}

		/// <summary>
		/// Converts an enum to an enumerable list
		/// </summary>
		/// <typeparam name="T">The enum type to return in an enumarable</typeparam>
		internal static IEnumerable<T> ToEnumerable<T>(this Enum input)
		{
			return Enum.GetValues(typeof(T)).OfType<T>();
		}
	}
}
