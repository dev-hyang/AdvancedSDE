namespace DevIQ.Principles.ExplicitDependency
{
    public class ExplicitDependencyDemoControllerV2
    {
        private readonly ILogger<ExplicitDependencyDemoControllerV2> _logger;

        public ExplicitDependencyDemoControllerV2(ILogger<ExplicitDependencyDemoControllerV2> logger)
        {
            _logger = logger;
        }

        public void Demo()
        {
            var customer = new Customer()
            {
                FirstName = "Hello",
                LastName = "World",
                Description = "My hobby is programming"
            };
            var response = new PersonalizedResponseV2(new SimpleFileLogger(), new SystemDateTime());

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

    public class PersonalizedResponseV2
    {
        private readonly ILogger _logger;
        private readonly IDateTime _dateTime;

        public PersonalizedResponseV2(ILogger logger,
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
