using System;

namespace ERService.Shared.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Get Non-Generic type name
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Name of type</returns>
        public static string GetFriendlyName(this Type type)
        {
            if (type.IsGenericType)
            {
                Type definition = type.GetGenericTypeDefinition();
                return definition.Name.Remove(definition.Name.IndexOf('`'));
            }
            return type.Name;
        }
    }
}
