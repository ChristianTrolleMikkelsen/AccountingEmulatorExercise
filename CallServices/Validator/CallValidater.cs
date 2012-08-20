using System.Collections.Generic;
using System.Linq;
using CallServices.Validator.Rules;
using Core.ServiceCalls;
using MoreLinq;

namespace CallServices.Validator
{
    public interface ICallValidator
    {
        void ValidateCall(IServiceCall call);
    }

    public class CallValidator : ICallValidator
    {
        private readonly IEnumerable<IRule> _rules;

        public CallValidator(IEnumerable<IRule> rules)
        {
            _rules = rules.OrderBy(rule => rule.GetType().Name);
        }

        public void ValidateCall(IServiceCall call)
        {
            _rules.ForEach(rule => rule.Validate(call));
        }
    }
}
