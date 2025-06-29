using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace UzClevMate._Common.Extensions
{
    public static class AttributeExtensions
    {
        public static string GetDisplayAttribute(this Enum enumValue)
        {
            var res = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>();

            if (res == null)
            {
                return enumValue.ToString();
            }
            else
            {

                var localizedValue = Resources.Translations.ResourceManager.GetString(res.Name, CultureInfo.CurrentUICulture);
                return localizedValue ?? res.Name;
            }
        }

        public static List<(string Text, int Value)> GetEnumDisplayTextValuePairs<T>() where T : Enum
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Select(field =>
                            {
                                var displayAttr = field.GetCustomAttribute<DisplayAttribute>();
                                var text = displayAttr != null ? displayAttr.Name : field.Name;
                                var value = (int)Enum.Parse(typeof(T), field.Name);
                                return (Text: text, Value: value);
                            })
                            .ToList();
        }
    }
}