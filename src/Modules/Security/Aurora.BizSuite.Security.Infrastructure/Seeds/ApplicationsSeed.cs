namespace Aurora.BizSuite.Security.Infrastructure.Seeds;

internal class ApplicationsSeed : ISeedDataService<SecurityContext>
{
    public void Seed(SecurityContext context)
    {
        var path = UtilsSeed.GetSeedDataPath("applications.json");
        var applicationsList = context.GetFromFile<List<ApplicationSeedData>, SecurityContext>(path);

        applicationsList?
            .Where(applicationData => !context.Applications.ToList().Any(
                x => x.Id.Equals(new Domain.Applications.ApplicationId(applicationData.ApplicationId))))
            .ToList()
            .ForEach(applicationData =>
            {
                var application = Application.Create(
                    new Domain.Applications.ApplicationId(applicationData.ApplicationId),
                    applicationData.Name,
                    applicationData.Description,
                    applicationData.HasCustomConfiguration);

                application.SetCreated(SecurityContext.BATCH_USER, DateTime.UtcNow);
                context.Applications.Add(application);
            });

        context.SaveChanges();
    }

    private class ApplicationSeedData
    {
        public Guid ApplicationId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool HasCustomConfiguration { get; set; }
    }
}