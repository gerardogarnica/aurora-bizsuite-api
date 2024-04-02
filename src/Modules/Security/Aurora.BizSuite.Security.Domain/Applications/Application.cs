namespace Aurora.BizSuite.Security.Domain.Applications;

public class Application : AggregateRoot<ApplicationId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool HasCustomConfiguration { get; private set; }

    protected Application()
        : base(new ApplicationId(Guid.NewGuid()))
    {
        Name = string.Empty;
        Description = string.Empty;
        HasCustomConfiguration = false;
    }

    private Application(
        ApplicationId applicationId,
        string name,
        string description,
        bool hasCustomConfiguration)
        : base(applicationId)
    {
        Name = name.Trim();
        Description = description.Trim();
        HasCustomConfiguration = hasCustomConfiguration;
    }

    public static Application Create(
        ApplicationId applicationId,
        string name,
        string description,
        bool hasCustomConfiguration)
    {
        return new Application(
            applicationId,
            name,
            description,
            hasCustomConfiguration);
    }
}