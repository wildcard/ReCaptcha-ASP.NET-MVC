using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace reCaptcha
{
    // TODO: move to external pack
    public static class EnumHelper<T>
    {
        private static void ValidateEnum()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }

        public static T Random()
        {
            ValidateEnum();

            var values = Enum.GetValues(typeof(T));
            var random = new Random();
            return (T)values.GetValue(random.Next(values.Length));
        }

        public static IList<T> GetValues(Enum value)
        {
            return  value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Select(fi => (T) Enum.Parse(value.GetType(), fi.Name, false)).ToList();
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static TS GetAttribute<TS>(T value)
        {
            ValidateEnum();

            var fieldInfo = value.GetType().GetField(value.ToString());

            var attrs = fieldInfo.GetCustomAttributes(typeof(TS), false) as TS[];

            if (attrs == null) 
                return default(TS);

            return attrs.Length == 0 ? default(TS) : attrs[0];
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        public static T GetValueFromDescription(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }


    }
}
