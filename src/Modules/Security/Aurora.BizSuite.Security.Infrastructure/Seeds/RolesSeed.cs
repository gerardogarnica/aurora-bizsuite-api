using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class RolesSeed : ISeedDataService<SecurityContext>
{
    public void Seed(SecurityContext context)
    {
        var applicationId = new ApplicationId(new Guid(UtilsSeed.AdminApplicationCode));

        var path = UtilsSeed.GetSeedDataPath("roles.json");
        var rolesList = context.GetFromFile<List<RoleSeedData>, SecurityContext>(path);
        var roles = context.Roles.IgnoreQueryFilters().ToList();

        rolesList?
            .Where(roleData => !roles.Any(x => x.ApplicationId == applicationId && x.Name.Equals(roleData.Name)))
            .ToList()
            .ForEach(roleData =>
            {
                var role = Role.Create(
                    new ApplicationId(roleData.ApplicationId),
                    roleData.Name,
                    roleData.Description,
                    roleData.Notes);

                role.SetCreated(SecurityContext.BATCH_USER, DateTime.UtcNow);
                context.Roles.Add(role);
            });

        context.SaveChanges();
    }

    private class RoleSeedData
    {
        public required Guid ApplicationId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Notes { get; set; }
    }
}