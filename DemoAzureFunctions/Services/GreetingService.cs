using DemoAzureFunctions.IServices;

namespace DemoAzureFunctions.Services
{
    public class GreetingService : IGreeterService
    {
        public string GetGreeting(string name)
        {
            return $"Hi {name}, nice to meet you!!";
        }
    }
}
