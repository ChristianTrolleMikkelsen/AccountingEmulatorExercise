using System;
using Core.ServiceCalls;

namespace CallCentral.Validator.Rules
{
    public class CallIsValid : IRule
    {
        public void Validate(IServiceCall call)
        {
            if (call.IsValid() == false)
            {
                throw new Exception(string.Format("Your call was dimissed as it contained invalid information: {0},", call.ToString()));
            }
        }
    }
}
