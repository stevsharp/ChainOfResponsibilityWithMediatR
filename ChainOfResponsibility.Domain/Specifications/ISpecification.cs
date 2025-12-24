
namespace ChainOfResponsibility.Domain.Specifications;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISpecification<T>
{
    bool IsSatisfiedBy(T candidate);
}
