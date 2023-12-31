﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Mensajeria_Linux.Infrastructure.ErrorMapping;

namespace Mensajeria_Linux.Infrastructure.ExecutionContext
{
    public class ApiExecutionContext<T> : ApiExecutionContext, IExecutionContext<T>
    {
        public ApiExecutionContext(
            IHttpContextAccessor httpContextAccessor,
            ILogger<T> logger,
            IErrorMapper errorMapper)
            : base(httpContextAccessor)
        {
            Logger = logger;
            ErrorMapper = errorMapper;
        }

        public ILogger<T> Logger { get; private set; }

        public IErrorMapper ErrorMapper { get; private set; }
    }
}
