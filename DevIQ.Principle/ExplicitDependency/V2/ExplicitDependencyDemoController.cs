namespace DevIQ.Principle.ExplicitDependency.V2
{
    public class ExplicitDependencyDemoController
    {
        public ExplicitDependencyDemoController()
        {
        }

        public void Demo()
        {
            var customer = new Customer()
            {
                FirstName = "Hello",
                LastName = "World",
                Description = "My hobby is programming"
            };
            var response = new PersonalizedResponse(new SimpleFileLogger(), new SystemDateTime());

            Console.WriteLine(response.GetResponse(customer));
            Console.ReadLine();
        }
    }

    public interface ILogger
    {
        void Log(string message);
    }

    public class SimpleFileLogger : ILogger
    {
        public void Log(string message)
        {
            using (StreamWriter logFile = new StreamWriter(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "logfile.txt")
                ))
            {
                logFile.WriteLine(message);
            }
        }
    }

    public interface IDateTime
    {
        DateTime Now { get; }
    }

    public class SystemDateTime : IDateTime
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }

    public class PersonalizedResponse
    {
        private readonly ILogger _logger;
        private readonly IDateTime _dateTime;

        public PersonalizedResponse(ILogger logger,
            IDateTime dateTime)
        {
            _dateTime = dateTime;
            _logger = logger;
        }

        public string GetResponse(Customer customer)
        {
            _logger.Log("Generating personalized response.");
            string formatString = "Good {3}! {0}, {1} and {2}, Which one you prefer?";
            string timeOfDay = "afternoon";
            if (_dateTime.Now.Hour < 12)
            {
                timeOfDay = "morning";
            }
            if (_dateTime.Now.Hour > 17)
            {
                timeOfDay = "evening";
            }
            return string.Format(formatString, 
                customer.FirstName + "," + customer.LastName,
                "Cupccino",
                "Latte",
                timeOfDay);

        }
    }
}
