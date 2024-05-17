namespace Aurora.BizSuite.Items.Domain.Units;

public interface IUnitRepository : IRepository<UnitOfMeasurement>
{
    Task<UnitOfMeasurement?> GetByIdAsync(UnitOfMeasurementId id);
    Task<UnitOfMeasurement?> GetByNameAsync(string name);
    Task InsertAsync(UnitOfMeasurement unitOfMeasurement);
    void Update(UnitOfMeasurement unitOfMeasurement);
}