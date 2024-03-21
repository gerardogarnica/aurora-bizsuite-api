namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class RolesSeed : ISeedDataService<SecurityContext>
{
    public void Seed(SecurityContext context)
    {
        var path = UtilsSeed.GetSeedDataPath("roles.json");
        var rolesList = context.GetFromFile<List<RoleSeedData>, SecurityContext>(path);

        rolesList?
            .Where(roleData => !context.Roles.ToList().Any(x => x.Name.Equals(roleData.Name)))
            .ToList()
            .ForEach(roleData =>
            {
                var role = Role.Create(
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
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Notes { get; set; }
    }
}