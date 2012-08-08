namespace PhoneSubscriptionCalculator.Service_Calls
{
    public interface IServiceCall
    {
        string PhoneNumber { get; }
        string FromCountry { get; }
        string ToCountry { get; }
    }
}
