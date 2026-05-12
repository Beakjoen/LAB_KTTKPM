using System.Text;

namespace StudentManagement
{
    internal class Program
    {
        private static async Task Main()
        {
            // Hiển thị tiếng Việt ổn định trên Console
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            // MongoDB connection string (localhost mặc định)
            const string connectionString = "mongodb://localhost:27017";
            const string databaseName = "StudentDB";

            // Two-Tier: UI → Service → Repository → MongoDB
            var repository = new StudentRepository(connectionString, databaseName);
            IStudentService service = new StudentService(repository);
            var ui = new StudentUI(service);

            await ui.RunAsync();
        }
    }
}
