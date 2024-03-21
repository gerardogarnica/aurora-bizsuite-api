using Aurora.Framework.Identity;

namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class UsersSeed : ISeedDataService<SecurityContext>
{
    public void Seed(SecurityContext context)
    {
        var adminRole = context.Roles.FirstOrDefault(x => x.Name.Equals("Administradores"));

        var path = UtilsSeed.GetSeedDataPath("users.json");
        var userList = context.GetFromFile<List<UserSeedData>, SecurityContext>(path);

        userList?
            .Where(userData => !context.Users.ToList().Any(x => x.Email.Equals(userData.Email)))
            .ToList()
            .ForEach(userData =>
            {
                var password = context.PasswordProvider.HashPassword(userData.Email);

                var user = User.Create(
                    userData.FirstName,
                    userData.LastName,
                    userData.Email,
                    Password.Create(password).Value,
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