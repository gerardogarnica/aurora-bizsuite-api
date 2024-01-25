namespace Aurora.BizSuite.Settings.Infrastructure.Repositories;

internal class OptionRepository : BaseRepository<Option, OptionId>, IOptionRepository
{
    public IUnitOfWork UnitOfWork => DbContext;

    public OptionRepository(SettingsContext context)
        : base(context) { }

    public async Task<Option?> GetOptionAsync(string code)
    {
        return await DbContext
            .Options
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Code == code);
    }
}