using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class UsersSeed : ISeedDataService<SecurityContext>
{
    const string defaultApplicationId = "25EE60E9-A6A9-45E8-A899-752C4B4576DC";
    const string defaultRoleName = "Administradores";

    public void Seed(SecurityContext context)
    {
        var applicationId = new ApplicationId(new Guid(defaultApplicationId));
        var adminRole = context
            .Roles
            .IgnoreQueryFilters()
            .FirstOrDefault(x => x.Name.Equals(defaultRoleName) && x.ApplicationId == applicationId);

        var path = UtilsSeed.GetSeedDataPath("users.json");
        var userList = context.GetFromFile<List<UserSeedData>, SecurityContext>(path);

        userList?
            .Where(userData => !context.Users.ToList().Any(x => x.Email.Equals(userData.Email)))
            .ToList()
            .ForEach(userData =>
            {
                var passwordResult = Password.Create(userData.Email);

                var user = User.Create(
                    userData.FirstName,
                    userData.LastName,
                    userData.Email,
                    context.PasswordProvider.HashPassword(passwordResult.Value),
                    null,
                    null,
                    userData.IsEditable);

                user.SetCreated(SecurityContext.BATCH_USER, DateTime.UtcNow);

                user.AddRole(adminRole!, false);
                foreach (var userRole in user.Roles)
                    userRole.SetCreated(SecurityContext.BATCH_USER, DateTime.UtcNow);

                context.Users.Add(user);
            });

        context.SaveChanges();
    }

    private class UserSeedData
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public bool IsEditable { get; set; }
    }
}