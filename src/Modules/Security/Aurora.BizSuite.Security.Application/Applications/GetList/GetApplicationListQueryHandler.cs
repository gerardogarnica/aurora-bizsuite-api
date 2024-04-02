namespace Aurora.BizSuite.Security.Application.Applications.GetList;

public class GetApplicationListQueryHandler(
    IApplicationRepository applicationRepository)
    : IQueryHandler<GetApplicationListQuery, IList<ApplicationResponse>>
{
    public async Task<Result<IList<ApplicationResponse>>> Handle(
        GetApplicationListQuery request,
        CancellationToken cancellationToken)
    {
        // Get applications
        var applications = await applicationRepository.GetListAsync();

        // Return application responses
        return applications.Select(x => x.ToApplicationResponse()).ToList();
    }
}