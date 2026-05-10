using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement
{
    public class StudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;
        private readonly string filePath = "students.txt";

        public StudentRepository()
        {
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(filePath)) return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                var student = Student.FromFileString(line);
                _students.Add(student);
                if (student.Id >= _nextId)
                    _nextId = student.Id + 1;
            }
        }

        public List<Student> GetAll() => _students;

        public Student Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
            SaveToFile();
            return student;
        }

        private void SaveToFile()
        {
            File.WriteAllLines(filePath, _students.Select(s => s.ToFileString()));
        }

        public bool Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _students.Remove(student);
                SaveToFile();
                return true;
            }
            return false;
        }

        public bool Update(int id, Student updatedStudent)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Email = updatedStudent.Email;
                student.Address = updatedStudent.Address;
                student.Age = updatedStudent.Age;
                student.Grade = updatedStudent.Grade;
                SaveToFile();
                return true;
            }
            return false;
        }

        public Student FindById(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> FindByName(string name)
        {
            return _students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Student> FindByAddress(string address)
        {
            return _students.Where(s => s.Address.Contains(address, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Student> FindByGrade(double minGrade, double maxGrade)
        {
            return _students.Where(s => s.Grade >= minGrade && s.Grade <= maxGrade).ToList();
        }
    }
}
