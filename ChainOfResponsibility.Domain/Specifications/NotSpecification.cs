namespace ChainOfResponsibility.Domain.Specifications;
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="spec"></param>
internal class NotSpecification<T>(ISpecification<T> spec) : ISpecification<T>
{
    /// <summary>
    /// 
    /// </summary>
    private ISpecification<T> spec = spec;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="candidate"></param>
    /// <returns></returns>
    public bool IsSatisfiedBy(T candidate) => !spec.IsSatisfiedBy(candidate);
}