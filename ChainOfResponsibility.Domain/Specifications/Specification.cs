
namespace ChainOfResponsibility.Domain.Specifications;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="predicate"></param>
public sealed class Specification<T>(Func<T, bool> predicate) : ISpecification<T>
{
    private readonly Func<T, bool> _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
    public bool IsSatisfiedBy(T candidate) => _predicate(candidate);
}