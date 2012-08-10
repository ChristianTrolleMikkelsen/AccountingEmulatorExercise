using Core.ServiceCalls;

namespace CallCentral.Validator.Rules
{
    internal interface IRule
    {
        void Validate(IServiceCall call);
    }
}
