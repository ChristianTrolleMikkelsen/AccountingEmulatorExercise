using Core.ServiceCalls;

namespace CallServices.Validator.Rules
{
    internal interface IRule
    {
        void Validate(IServiceCall call);
    }
}
