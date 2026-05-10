using System;
using System.Collections.Generic;

namespace StudentManagement
{
    public class StudentService
    {
        private readonly StudentRepository _repo = new();

        public List<Student> GetAllStudents() => _repo.GetAll();

        public Student AddStudent(string name, string email, string address, int age, double grade)
            => _repo.Add(name, email, address, age, grade);

        public bool UpdateStudent(int id, string name, string email, string address, int age, double grade)
            => _repo.Update(id, name, email, address, age, grade);

        public bool DeleteStudent(int id) => _repo.Delete(id);

        public Student SearchById(int id) => _repo.FindById(id);

        public List<Student> SearchByName(string name) => _repo.FindByName(name);

        public List<Student> SearchByAddress(string address) => _repo.FindByAddress(address);

        public List<Student> SearchByGrade(double minGrade, double maxGrade)
            => _repo.FindByGrade(minGrade, maxGrade);
    }
}
