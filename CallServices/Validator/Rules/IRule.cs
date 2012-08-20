using Core.ServiceCalls;

namespace CallServices.Validator.Rules
{
    public interface IRule
    {
        void Validate(IServiceCall call);
    }
}
