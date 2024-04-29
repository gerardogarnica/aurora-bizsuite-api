namespace Aurora.BizSuite.Security.Domain.Applications;

public interface IApplicationProvider
{
    Guid GetApplicationId();
    bool IsAdminApp();
}