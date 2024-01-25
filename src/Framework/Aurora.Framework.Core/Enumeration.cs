using System.Reflection;

namespace Aurora.Framework;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }
    public int Value { get; private set; }

    protected Enumeration() => (Value, Name) = (default, string.Empty);

    protected Enumeration(int value, string name) => (Value, Name) = (value, name);

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object? other) => other is null ? 1 : Value.CompareTo(((Enumeration)other).Value);

    public override int GetHashCode() => Value.GetHashCode();
}