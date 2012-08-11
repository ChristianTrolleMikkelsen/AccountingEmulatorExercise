using System.Collections.Generic;
using CallServices.Validator.Rules;
using Core.ServiceCalls;
using MoreLinq;
using StructureMap;

namespace CallServices.Validator
{
    public interface ICallValidator
    {
        void ValidateCall(IServiceCall call);
    }

    public class CallValidator : ICallValidator
    {
        private readonly IEnumerable<IRule> _rules;

        public CallValidator()
        {
            _rules = ObjectFactory.GetAllInstances<IRule>();
        }

        public void ValidateCall(IServiceCall call)
        {
            _rules.ForEach(rule => rule.Validate(call));
        }
    }
}
