namespace Aurora.BizSuite.Items.Application.Units.GetById;

public sealed record GetUnitByIdQuery(Guid Id) : IQuery<UnitOfMeasurementModel>;