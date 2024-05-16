namespace Aurora.BizSuite.Items.Domain.Units;

public static class UnitErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Units.NotFound",
        $"The unit with identifier {id} was not found.");
}