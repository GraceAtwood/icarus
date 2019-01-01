using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Icarus.Utilities
{
    public static class TypeUtilities
    {
        public static Type GetUnderLyingType(this MemberInfo member)
        {
            switch (member)
            {
                    case EventInfo info:
                        return info.EventHandlerType;
                    case FieldInfo info:
                        return info.FieldType;
                    case MethodInfo info:
                        return info.ReturnType;
                    case PropertyInfo info:
                        return info.PropertyType;
                    default:
                        throw new ArgumentException("Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo", nameof(member));
            }
        }

        public static IEnumerable<Type> GetAllParentTypes(this Type type)
        {
            if (type == null)
            {
                yield break;
            }

            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType= currentBaseType.BaseType;
            }
        }
    }
}