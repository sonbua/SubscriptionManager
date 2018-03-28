using System;
using System.Collections.Generic;

namespace R2
{
    public abstract class ResponsibilityChain<TRequest, TResponse>
    {
        protected ResponsibilityChain<TRequest, TResponse> NextInChain { get; set; }

        public virtual void Add(ResponsibilityChain<TRequest, TResponse> handler)
        {
            if (NextInChain == null)
            {
                NextInChain = handler;
            }
            else
            {
                NextInChain.Add(handler);
            }
        }

        public abstract bool CanHandle(TRequest request);

        public abstract TResponse Handle(TRequest request);
    }

    public class CompositeResponsibilityChain<TRequest, TResponse> : ResponsibilityChain<TRequest, TResponse>
    {
        private const string _CANNOT_HANDLE = "{0} cannot handle this request. Request information: {1}";

        private readonly IList<ResponsibilityChain<TRequest, TResponse>> _handlers;

        public CompositeResponsibilityChain(IList<ResponsibilityChain<TRequest, TResponse>> handlers)
        {
            _handlers = handlers;
        }

        public override bool CanHandle(TRequest request) => true;

        public override TResponse Handle(TRequest request)
        {
            foreach (var handler in _handlers)
            {
                if (handler.CanHandle(request))
                {
                    return handler.Handle(request);
                }
            }

            throw new NotSupportedException(
                string.Format(_CANNOT_HANDLE, nameof(CompositeResponsibilityChain<TRequest, TResponse>), request)
            );
        }
    }

    public class NullResponsibilityChain<TRequest, TResponse> : ResponsibilityChain<TRequest, TResponse>
    {
        private const string _END_OF_CHAIN = "{0} should be the end of the chain.";

        public override void Add(ResponsibilityChain<TRequest, TResponse> handler)
        {
            throw new NotSupportedException(
                string.Format(_END_OF_CHAIN, nameof(NullResponsibilityChain<TRequest, TResponse>))
            );
        }

        public override bool CanHandle(TRequest request) => true;

        public override TResponse Handle(TRequest request) => default(TResponse);
    }
}