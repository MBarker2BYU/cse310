using SwarNet.Attributes;

namespace SwarNet.Extensions;

public static class EnumExtensions
{
    public static int GetShipLength(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return 0;

        var attr = (ShipLengthAttribute?)Attribute.GetCustomAttribute(
            field, typeof(ShipLengthAttribute));

        return attr?.Value ?? 0;
    }
}