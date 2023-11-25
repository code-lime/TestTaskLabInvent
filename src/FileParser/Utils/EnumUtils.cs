using System.Reflection;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace FileParser.Utils;

public static class EnumUtils
{
    private static readonly ConcurrentDictionary<Type, (ImmutableDictionary<string, Enum> nameToValue, ImmutableDictionary<Enum, string> valueToName)> cacheEnums = new();

    private static (ImmutableDictionary<string, Enum> nameToValue, ImmutableDictionary<Enum, string> valueToName) BuildByType(Type type)
    {
        ImmutableDictionary<string, Enum>.Builder nameToValue = ImmutableDictionary.CreateBuilder<string, Enum>();
        ImmutableDictionary<Enum, string>.Builder valueToName = ImmutableDictionary.CreateBuilder<Enum, string>();
        string[] names = Enum.GetNames(type);
        Array values = Enum.GetValues(type);
        int length = names.Length;
        for (int i = 0; i < length; i++)
        {
            string name = names[i];
            Enum value = (Enum)values.GetValue(i)!;
            string displayName = type.GetField(name)?.GetCustomAttribute<EnumMemberAttribute>(false)?.Value ?? name;

            nameToValue[displayName] = value;
            valueToName[value] = displayName;
        }
        return (nameToValue.ToImmutable(), valueToName.ToImmutable());
    }
    private static (ImmutableDictionary<string, Enum> nameToValue, ImmutableDictionary<Enum, string> valueToName) GetByType(Type type)
        => cacheEnums.GetOrAdd(type, BuildByType);

    public static string GetEnumMember<T>(this T value) where T : Enum
        => GetByType(value.GetType()).valueToName[value];
    public static bool TryParseEnumMember<T>(this string memberName, [NotNullWhen(true)] out T? result) where T : Enum
    {
        if (GetByType(typeof(T)).nameToValue.TryGetValue(memberName, out Enum? _result) && _result is T __result)
        {
            result = __result;
            return true;
        }
        result = default;
        return false;
    }
    public static bool TryParseEnumMember(this string memberName, Type enumType, [NotNullWhen(true)] out Enum? result)
    {
        if (GetByType(enumType).nameToValue.TryGetValue(memberName, out Enum? _result) && enumType.IsInstanceOfType(_result))
        {
            result = _result;
            return true;
        }
        result = default;
        return false;
    }
}
