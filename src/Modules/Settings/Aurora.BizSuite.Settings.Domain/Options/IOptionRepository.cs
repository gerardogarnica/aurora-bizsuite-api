namespace Aurora.BizSuite.Settings.Domain.Options;

public interface IOptionRepository : IRepository<Option>
{
    void Insert(Option option);
    void Update(Option option);
    Task<Option?> GetOptionAsync(string code);
}