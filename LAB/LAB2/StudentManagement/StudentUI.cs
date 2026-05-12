using System.Linq;

namespace StudentManagement
{
    /// <summary>
    /// Giao diện Console: menu tương tác, mọi thao tác dữ liệu đều await qua <see cref="IStudentService"/>.
    /// </summary>
    public class StudentUI
    {
        private readonly IStudentService _service;

        public StudentUI(IStudentService service)
        {
            _service = service;
        }

        /// <summary>Vòng lặp menu; mỗi chức năng chờ hoàn thành tầng Service.</summary>
        public async Task RunAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== QUẢN LÝ SINH VIÊN (MongoDB) ===");
                Console.WriteLine("1. Hiển thị danh sách sinh viên");
                Console.WriteLine("2. Thêm sinh viên");
                Console.WriteLine("3. Sửa sinh viên");
                Console.WriteLine("4. Xóa sinh viên");
                Console.WriteLine("5. Tìm kiếm sinh viên");
                Console.WriteLine("0. Thoát");
                Console.Write("Chọn chức năng: ");

                var choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        await ShowAllAsync();
                        break;
                    case "2":
                        await AddStudentAsync();
                        break;
                    case "3":
                        await UpdateStudentAsync();
                        break;
                    case "4":
                        await DeleteStudentAsync();
                        break;
                    case "5":
                        await SearchStudentsAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Nhấn Enter để thử lại.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        /// <summary>Đọc danh sách từ SQL Server.</summary>
        private async Task ShowAllAsync()
        {
            var students = await _service.GetAllAsync();
            Console.WriteLine("\n--- DANH SÁCH SINH VIÊN ---");
            if (!students.Any())
            {
                Console.WriteLine("Chưa có sinh viên nào.");
            }
            else
            {
                foreach (var student in students)
                {
                    Console.WriteLine(student);
                }
            }

            Pause();
        }

        /// <summary>Thêm bản ghi mới; Id do IDENTITY cấp.</summary>
        private async Task AddStudentAsync()
        {
            Console.WriteLine("\n--- THÊM SINH VIÊN ---");
            var name = ReadRequired("Tên");
            var email = ReadRequired("Email");
            var address = ReadRequired("Địa chỉ");
            var age = ReadInt("Tuổi");
            var grade = ReadRequired("Lớp");

            await _service.AddStudentAsync(name, email, address, age, grade);
            Console.WriteLine("Thêm sinh viên thành công.");
            Pause();
        }

        /// <summary>Sửa theo Id (ObjectId string).</summary>
        private async Task UpdateStudentAsync()
        {
            Console.WriteLine("\n--- SỬA SINH VIÊN ---");
            var id = ReadRequired("Nhập ID sinh viên cần sửa");
            var student = await _service.GetAsync(id);
            if (student == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID này.");
                Pause();
                return;
            }

            Console.WriteLine("Nhập thông tin mới. Nhấn Enter để giữ nguyên giá trị cũ.");
            var name = ReadOptional("Tên", student.Name);
            var email = ReadOptional("Email", student.Email);
            var address = ReadOptional("Địa chỉ", student.Address);
            var age = ReadIntOptional("Tuổi", student.Age);
            var grade = ReadOptional("Lớp", student.Grade);

            var ok = await _service.UpdateStudentAsync(id, name, email, address, age, grade);
            Console.WriteLine(ok ? "Cập nhật sinh viên thành công." : "Cập nhật thất bại.");
            Pause();
        }

        /// <summary>Xóa theo Id (ObjectId string).</summary>
        private async Task DeleteStudentAsync()
        {
            Console.WriteLine("\n--- XÓA SINH VIÊN ---");
            var id = ReadRequired("Nhập ID sinh viên cần xóa");
            if (await _service.DeleteStudentAsync(id))
            {
                Console.WriteLine("Xóa sinh viên thành công.");
            }
            else
            {
                Console.WriteLine("Không tìm thấy sinh viên với ID này.");
            }

            Pause();
        }

        /// <summary>Tìm theo Id, Name, Address hoặc Grade (câu lệnh SQL trong Repository).</summary>
        private async Task SearchStudentsAsync()
        {
            Console.WriteLine("\n--- TÌM KIẾM SINH VIÊN ---");
            Console.Write("Nhập ID, tên, địa chỉ hoặc lớp tìm kiếm: ");
            var query = Console.ReadLine()?.Trim() ?? string.Empty;
            var results = await _service.SearchAsync(query);

            Console.WriteLine($"\nKết quả tìm kiếm ({results.Count}):");
            if (!results.Any())
            {
                Console.WriteLine("Không tìm thấy sinh viên phù hợp.");
            }
            else
            {
                foreach (var student in results)
                {
                    Console.WriteLine(student);
                }
            }

            Pause();
        }

        private static string ReadRequired(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                var value = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                Console.WriteLine($"{fieldName} không được để trống.");
            }
        }

        private static string ReadOptional(string fieldName, string currentValue)
        {
            Console.Write($"{fieldName} [{currentValue}]: ");
            var value = Console.ReadLine()?.Trim();
            return string.IsNullOrWhiteSpace(value) ? currentValue : value;
        }

        private static int ReadInt(string fieldName)
        {
            while (true)
            {
                Console.Write($"{fieldName}: ");
                var value = Console.ReadLine()?.Trim();
                if (int.TryParse(value, out var number) && number > 0)
                {
                    return number;
                }

                Console.WriteLine($"{fieldName} phải là số nguyên dương.");
            }
        }

        private static int ReadIntOptional(string fieldName, int currentValue)
        {
            while (true)
            {
                Console.Write($"{fieldName} [{currentValue}]: ");
                var value = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(value))
                {
                    return currentValue;
                }

                if (int.TryParse(value, out var number) && number > 0)
                {
                    return number;
                }

                Console.WriteLine($"{fieldName} phải là số nguyên dương.");
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nNhấn Enter để tiếp tục...");
            Console.ReadLine();
        }
    }
}
