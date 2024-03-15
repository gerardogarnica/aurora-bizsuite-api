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
}