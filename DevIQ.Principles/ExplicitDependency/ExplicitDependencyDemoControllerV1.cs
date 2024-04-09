namespace DevIQ.Principles.ExplicitDependency
{
    public class ExplicitDependencyDemoControllerV1
    {
        private readonly ILogger<ExplicitDependencyDemoControllerV1> _logger;

        public ExplicitDependencyDemoControllerV1(ILogger<ExplicitDependencyDemoControllerV1> logger)
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
            Context.CurrentCustomer = customer;
            var response = new PersonalizedResponse();
        }
    }

    public static class Context
    {
        public static Customer CurrentCustomer { get; set; }
        public static void Log(string message)
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

    public class Customer
    {
        public string FirstName { get; set;}
        public string LastName { get; set;} 
        public string Description { get; set;}
    }

    public class PersonalizedResponse
    {
        public string GetResponse()
        {
            Context.Log("Get personalized response.");
            string formatString = "Good Friday! {0}, {1} and {2}, Which one you prefer?";
            return string.Format(formatString, 
                Context.CurrentCustomer.FirstName + "," + Context.CurrentCustomer.LastName,
                "Cupccino",
                "Latte");

        }
    }
}
