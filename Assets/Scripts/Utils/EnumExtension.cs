using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Utils
{
    public static class EnumExtension
    {
        public static List<T> GenerateEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
        
        public static List<T> GenerateEnumList<T>(Func<T, bool> func) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Where(func).ToList();
        }
    }
}