namespace Aurora.BizSuite.Items.Domain.Units;

public interface IUnitRepository : IRepository<UnitOfMeasurement>
{
    Task<UnitOfMeasurement?> GetByIdAsync(UnitOfMeasurementId id);
    Task<UnitOfMeasurement?> GetByNameAsync(string name);
    Task<PagedResult<UnitOfMeasurement>> GetPagedAsync(PagedViewRequest paged, string? searchTerms);
    Task InsertAsync(UnitOfMeasurement unitOfMeasurement);
    void Update(UnitOfMeasurement unitOfMeasurement);
}