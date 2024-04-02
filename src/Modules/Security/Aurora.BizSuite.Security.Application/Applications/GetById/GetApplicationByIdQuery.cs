namespace Aurora.BizSuite.Security.Application.Applications.GetById;

public sealed record GetApplicationByIdQuery(Guid Id) : IQuery<ApplicationResponse>;