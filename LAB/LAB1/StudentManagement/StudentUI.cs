using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class StudentUI
    {
        private readonly StudentService studentService = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowStudents();
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        UpdateStudent();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        SearchById();
                        break;
                    case "5":
                        SearchByName();
                        break;
                    case "6":
                        SearchByAddress();
                        break;
                    case "7":
                        SearchByGrade();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }

                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ShowStudents()
        {
            var students = studentService.GetAllStudents();
            Console.WriteLine("=== DANH SÁCH SINH VIÊN ===");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            if (students.Count == 0)
                Console.WriteLine("Chưa có sinh viên nào.");
        }

        private void ShowMenu()
        {
            Console.WriteLine("\nChức năng:");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Sửa sinh viên");
            Console.WriteLine("3. Xoá sinh viên");
            Console.WriteLine("4. Tìm theo ID");
            Console.WriteLine("5. Tìm theo Tên");
            Console.WriteLine("6. Tìm theo Địa chỉ");
            Console.WriteLine("7. Tìm theo Điểm");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn: ");
        }

        private void AddStudent()
        {
            Console.Write("Nhập tên: ");
            string name = Console.ReadLine();

            Console.Write("Nhập email: ");
            string email = Console.ReadLine();

            Console.Write("Nhập địa chỉ: ");
            string address = Console.ReadLine();

            Console.Write("Nhập tuổi: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
                return;

            Console.Write("Nhập điểm: ");
            if (!double.TryParse(Console.ReadLine(), out double grade))
                return;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
                studentService.AddStudent(name, email, address, age, grade);
        }

        private void DeleteStudent()
        {
            Console.Write("Nhập ID cần xoá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                studentService.DeleteStudent(id);
        }

        private void UpdateStudent()
        {
            Console.Write("Nhập ID cần sửa: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                return;

            Console.Write("Nhập tên mới: ");
            string name = Console.ReadLine();

            Console.Write("Nhập email mới: ");
            string email = Console.ReadLine();

            Console.Write("Nhập địa chỉ mới: ");
            string address = Console.ReadLine();

            Console.Write("Nhập tuổi mới: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
                return;

            Console.Write("Nhập điểm mới: ");
            if (!double.TryParse(Console.ReadLine(), out double grade))
                return;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
                studentService.UpdateStudent(id, name, email, address, age, grade);
        }

        private void SearchById()
        {
            Console.Write("Nhập ID cần tìm: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var student = studentService.SearchById(id);
                if (student != null)
                    Console.WriteLine(student);
                else
                    Console.WriteLine("Không tìm thấy.");
            }
        }

        private void SearchByName()
        {
            Console.Write("Nhập tên cần tìm: ");
            string name = Console.ReadLine();
            var students = studentService.SearchByName(name);

            Console.WriteLine("\n=== KẾT QUẢ TÌM KIẾM ===");
            foreach (var student in students)
                Console.WriteLine(student);
            if (students.Count == 0)
                Console.WriteLine("Không tìm thấy.");
        }

        private void SearchByAddress()
        {
            Console.Write("Nhập địa chỉ cần tìm: ");
            string address = Console.ReadLine();
            var students = studentService.SearchByAddress(address);

            Console.WriteLine("\n=== KẾT QUẢ TÌM KIẾM ===");
            foreach (var student in students)
                Console.WriteLine(student);
            if (students.Count == 0)
                Console.WriteLine("Không tìm thấy.");
        }

        private void SearchByGrade()
        {
            Console.Write("Nhập điểm tối thiểu: ");
            if (!double.TryParse(Console.ReadLine(), out double minGrade))
                return;

            Console.Write("Nhập điểm tối đa: ");
            if (!double.TryParse(Console.ReadLine(), out double maxGrade))
                return;

            var students = studentService.SearchByGrade(minGrade, maxGrade);

            Console.WriteLine("\n=== KẾT QUẢ TÌM KIẾM ===");
            foreach (var student in students)
                Console.WriteLine(student);
            if (students.Count == 0)
                Console.WriteLine("Không tìm thấy.");
        }
    }
}
