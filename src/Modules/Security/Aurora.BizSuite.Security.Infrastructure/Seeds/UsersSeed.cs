using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class UsersSeed : ISeedDataService<SecurityContext>
{
    public void Seed(SecurityContext context)
    {
        var applicationId = new ApplicationId(new Guid(UtilsSeed.AdminApplicationCode));
        var adminRole = context
            .Roles
            .IgnoreQueryFilters()
            .FirstOrDefault(x => x.ApplicationId == applicationId && x.Name.Equals(UtilsSeed.AdminRoleName));

        var path = UtilsSeed.GetSeedDataPath("users.json");
        var userList = context.GetFromFile<List<UserSeedData>, SecurityContext>(path);
        var users = context.Users.ToList();

        userList?
            .Where(userData => !users.Any(x => x.Email.Equals(userData.Email)))
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