using FluentValidation;

namespace Aurora.Framework.Application;

public static class RuleBuilderOptionsExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithBaseError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        BaseError baseError) =>
        rule.WithErrorCode(baseError.Code).WithMessage(baseError.Message);
}