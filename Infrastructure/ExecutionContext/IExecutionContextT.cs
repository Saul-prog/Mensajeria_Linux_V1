using Mensajeria_Linux.ExecutionContext.Infrastructure.ExecutionContext;
using Mensajeria_Linux.Infrastructure.ErrorMapping;

namespace Mensajeria_Linux.Infrastructure.ExecutionContext
{
    public interface IExecutionContext<out T> : IExecutionContext
    {
        ILogger<T> Logger { get; }

        IErrorMapper ErrorMapper { get; }
    }
}
