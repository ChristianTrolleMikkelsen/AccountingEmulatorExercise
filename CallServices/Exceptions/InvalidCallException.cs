using System;
using Core.ServiceCalls;

namespace CallServices.Exceptions
{
    public class InvalidCallException : Exception
    {
        private readonly IServiceCall _call;

        public InvalidCallException(IServiceCall call)
        {
            _call = call;
        }

        public override string ToString()
        {
            return string.Format("Your call was dismissed as it contained invalid information: {0},", _call);
        }
    }
}
