using MediatR;

namespace ChainOfResponsibility.Application.Abstractions.Messaging;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse> { }
