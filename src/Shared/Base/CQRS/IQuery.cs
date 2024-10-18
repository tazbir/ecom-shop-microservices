using MediatR;

namespace Base.CQRS;

public interface IQuery: IQuery<Unit>
{
    
}
public interface IQuery<out TResponse>: IRequest<TResponse>
where TResponse: notnull
{
    
}