namespace Aurora.BizSuite.Items.Domain.Units;

public interface IUnitRepository : IRepository<UnitOfMeasurement>
{
    Task<UnitOfMeasurement?> GetByIdAsync(UnitOfMeasurementId id);
}