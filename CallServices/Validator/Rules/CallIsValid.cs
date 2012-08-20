using CallServices.Exceptions;
using Core.ServiceCalls;

namespace CallServices.Validator.Rules
{
    public class CallIsValid : IRule
    {
        public void Validate(IServiceCall call)
        {
            if (call.IsValid() == false)
            {
                throw new InvalidCallException(call);
            }
        }
    }
}
