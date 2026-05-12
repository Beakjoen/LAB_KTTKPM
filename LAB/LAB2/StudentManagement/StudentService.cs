namespace StudentManagement
{
    /// <summary>
    /// Hợp đồng tầng nghiệp vụ — UI chỉ phụ thuộc vào interface, dễ test và mở rộng.
    /// </summary>
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetAsync(string id);
        Task AddStudentAsync(string name, string email, string address, int age, string grade);
        Task<bool> UpdateStudentAsync(string id, string name, string email, string address, int age, string grade);
        Task<bool> DeleteStudentAsync(string id);
        Task<List<Student>> SearchAsync(string keyword);
    }

    /// <summary>
    /// Lớp nghiệp vụ: trung gian giữa UI và StudentRepository, toàn bộ API bất đồng bộ.
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly StudentRepository _repository;

        public StudentService(StudentRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public Task<List<Student>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        /// <inheritdoc />
        public Task<Student?> GetAsync(string id)
        {
            return _repository.GetAsync(id);
        }

        /// <inheritdoc />
        public async Task AddStudentAsync(string name, string email, string address, int age, string grade)
        {
            var student = new Student
            {
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };

            await _repository.AddAsync(student);
        }

        /// <inheritdoc />
        public async Task<bool> UpdateStudentAsync(string id, string name, string email, string address, int age, string grade)
        {
            var current = await _repository.GetAsync(id);
            if (current == null)
            {
                return false;
            }

            current.Name = name;
            current.Email = email;
            current.Address = address;
            current.Age = age;
            current.Grade = grade;

            return await _repository.UpdateAsync(current);
        }

        /// <inheritdoc />
        public Task<bool> DeleteStudentAsync(string id)
        {
            return _repository.DeleteAsync(id);
        }

        /// <inheritdoc />
        public Task<List<Student>> SearchAsync(string keyword)
        {
            return _repository.SearchAsync(keyword);
        }
    }
}
