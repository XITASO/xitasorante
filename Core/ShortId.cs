using System.Text.RegularExpressions;

namespace Core;

public class ShortId: IEquatable<ShortId>
{
    public static ShortId From(string id) => new(id);
    
    private string id { get; }

    private ShortId(string from)
    {
        id = from;
    }
    
    public ShortId()
    {
        id = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
    }
    
    public static bool operator ==(ShortId left, ShortId right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ShortId left, ShortId right)
    {
        return !left.Equals(right);
    }

    public bool Equals(ShortId? other)
    {
        return other is ShortId && id == other.ToString();
    }

    public override bool Equals(object? obj)
    {
        return obj is ShortId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id);
    }

    public override string ToString()
    {
        return id;
    }
}