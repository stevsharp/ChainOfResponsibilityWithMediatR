namespace ChainOfResponsibility.Domain.Specifications;

public static class SpecificationExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right) => new AndSpecification<T>(left, right);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right) => new OrSpecification<T>(left, right);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="spec"></param>
    /// <returns></returns>
    public static ISpecification<T> Not<T>(this ISpecification<T> spec) => new NotSpecification<T>(spec);

}
