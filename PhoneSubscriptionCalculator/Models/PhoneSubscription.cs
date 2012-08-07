using System.Collections.Generic;
using System.Linq;

namespace PhoneSubscriptionCalculator.Models
{
    public interface IPhoneSubscription
    {
        IEnumerable<IService> GetInLandServices();
        IEnumerable<IService> GetAbroadServices();
        void AddInLandService(IService service);
        void AddAbroadService(IService service);

        IPhoneSubscription RegisterACall(ICall call);

        string PhoneNumber { get; }
        IEnumerable<ICall> GetCalls();
    }

    public class PhoneSubscription : IPhoneSubscription
    {
        private List<IService> _inlandServices;
        private List<IService> _abroadServices;
        private List<ICall> _calls;

        public string PhoneNumber { get; private set; }

        public PhoneSubscription(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            _inlandServices = new List<IService>();
            _abroadServices = new List<IService>();
            _calls = new List<ICall>();
        }

        public IEnumerable<IService> GetInLandServices()
        {
            return _inlandServices;
        }

        public IEnumerable<IService> GetAbroadServices()
        {
            return _abroadServices;
        }

        public void AddInLandService(IService service)
        {
            if (ListDoNotContainAServiceOfSameType(_inlandServices, service))
            {
                _inlandServices.Add(service);
            }
        }

        public void AddAbroadService(IService service)
        {
            if (ListDoNotContainAServiceOfSameType(_abroadServices, service))
            {
                _abroadServices.Add(service);
            }
        }

        private bool ListDoNotContainAServiceOfSameType(IEnumerable<IService> list, IService service)
        {
            return !list.Any(listedService => listedService.GetType() == service.GetType());
        }

        public IPhoneSubscription RegisterACall(ICall call)
        {
            _calls.Add(call);
            return this;
        }

        public IEnumerable<ICall> GetCalls()
        {
            return _calls;
        }
    }
}
