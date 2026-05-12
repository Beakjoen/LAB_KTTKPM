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

            
            const string connectionString =
                "Server=.;Database=StudentDB;Integrated Security=true;TrustServerCertificate=true;";

            // Two-Tier: UI → Service → Repository → SQL Server
            var repository = new StudentRepository(connectionString);
            IStudentService service = new StudentService(repository);
            var ui = new StudentUI(service);

            await ui.RunAsync();
        }
    }
}
