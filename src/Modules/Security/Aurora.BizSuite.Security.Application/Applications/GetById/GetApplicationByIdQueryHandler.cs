using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Application.Applications.GetById;

public class GetApplicationByIdQueryHandler(
    IApplicationRepository applicationRepository)
    : IQueryHandler<GetApplicationByIdQuery, ApplicationResponse>
{
    public async Task<Result<ApplicationResponse>> Handle(
        GetApplicationByIdQuery request,
        CancellationToken cancellationToken)
    {
        // Get application
        var application = await applicationRepository.GetByIdAsync(new ApplicationId(request.Id));

        if (application is null)
            return Result.Fail<ApplicationResponse>(DomainErrors.ApplicationErrors.ApplicationNotFound(request.Id));

        // Return application response
        return application.ToApplicationResponse();
    }
}