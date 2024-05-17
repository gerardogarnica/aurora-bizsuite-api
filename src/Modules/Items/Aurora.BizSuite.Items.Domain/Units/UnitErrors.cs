namespace Aurora.BizSuite.Items.Domain.Units;

public static class UnitErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Units.NotFound",
        $"The unit with identifier {id} was not found.");

    public static BaseError NameIsNotUnique(string name) => new(
        "Units.NameIsNotUnique",
        $"The unit name '{name}' already exists for another unit of measurement.");
}