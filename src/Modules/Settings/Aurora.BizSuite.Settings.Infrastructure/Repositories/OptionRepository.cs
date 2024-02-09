namespace Aurora.BizSuite.Settings.Infrastructure.Repositories;

internal class OptionRepository : BaseRepository<Option, OptionId>, IOptionRepository
{
    private readonly SettingsContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public OptionRepository(SettingsContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Option?> GetOptionAsync(string code)
    {
        return await _context
            .Options
            .Where(x => x.Code == code)
            .Include(x => x.Items)
            .FirstOrDefaultAsync();
    }

    public async Task<PagedResult<Option>> GetPagedOptionAsync(PagedViewRequest paged, string? searchCriteria)
    {
        var query = _context
            .Options
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchCriteria) && searchCriteria.Length >= 3)
        {
            query = query.Where(
                x => x.Code.Contains(searchCriteria) ||
                x.Name.Contains(searchCriteria));
        }

        return await ToPagedResultAsync(query.OrderBy(x => x.Name), paged);
    }
}