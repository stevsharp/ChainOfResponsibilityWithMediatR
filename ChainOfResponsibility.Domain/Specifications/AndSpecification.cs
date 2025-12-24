namespace ChainOfResponsibility.Domain.Specifications;

public class AndSpecification<T>(ISpecification<T> left, ISpecification<T> right) : ISpecification<T>
{
    /// <summary>
    /// 
    /// </summary>
    private ISpecification<T> left = left;
    /// <summary>
    /// 
    /// </summary>
    private ISpecification<T> right = right;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public bool IsSatisfiedBy(T candidate) => left.IsSatisfiedBy(candidate) && right.IsSatisfiedBy(candidate);
}