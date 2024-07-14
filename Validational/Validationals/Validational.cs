using FluentValidation.Results;

namespace Validational;

public class Validational<T>
{
    public readonly IReadOnlyList<ValidationFailure> Failures;
    public readonly T? Value;

    public static implicit operator Validational<T>(T t) => new(t);
    public static implicit operator Validational<T>(ValidationFailure failure) => new([failure]);
    public static implicit operator Validational<T>(List<ValidationFailure> failures) => new(failures);

    private Validational(T t)
    {
        Value = t;
        Failures = [];
    }

    private Validational(IReadOnlyList<ValidationFailure> failures)
    {
        Value = default;
        Failures = failures;
    }
}