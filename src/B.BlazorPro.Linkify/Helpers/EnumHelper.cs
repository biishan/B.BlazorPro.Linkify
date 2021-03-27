using System;
using System.ComponentModel;

namespace B.BlazorPro.Linkify.Helpers
{
    internal static class EnumHelper
    {
        /// <summary>
        /// Get description of enum value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name != null)
            {
                var field = type.GetField(name);
                if (field != null)
                {
                    var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}