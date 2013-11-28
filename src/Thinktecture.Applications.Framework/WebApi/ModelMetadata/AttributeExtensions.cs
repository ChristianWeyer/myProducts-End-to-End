using System;
using System.Linq;
using System.Reflection;

namespace Thinktecture.Applications.Framework.WebApi.ModelMetadata
{
    public static class AttributeExtensions
    {
        public static TAttribute GetAttributeOnTypeOrAssembly<TAttribute>(this Type type) where TAttribute : Attribute
        {
            return type.First<TAttribute>() ?? type.Assembly.First<TAttribute>();
        }

        public static TAttribute First<TAttribute>(this ICustomAttributeProvider attributeProvider)
            where TAttribute : Attribute
        {
            return attributeProvider.GetCustomAttributes(typeof (TAttribute), true).FirstOrDefault() as TAttribute;
        }
    }
}