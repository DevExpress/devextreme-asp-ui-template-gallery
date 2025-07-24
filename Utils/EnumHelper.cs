namespace DevExtremeVSTemplateMVC.Utils
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    public static class EnumHelper
    {
        public static string GetDisplayName(Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var displayAttr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
                if (displayAttr != null)
                    return displayAttr.Name;
            }
            return enumValue.ToString();
        }

        public static List<string> GetDisplayNames<TEnum>() where TEnum : Enum
        {
            var names = new List<string>();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                names.Add(GetDisplayName((Enum)value));
            }
            return names;
        }
    }

}
