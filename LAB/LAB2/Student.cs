namespace StudentManagement
{
    /// <summary>
    /// Thực thể sinh viên ánh xạ bảng Students trên SQL Server (khóa Id kiểu int, thường IDENTITY).
    /// </summary>
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Grade { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {Id}\nTên: {Name}\nEmail: {Email}\nĐịa chỉ: {Address}\nTuổi: {Age}\nLớp: {Grade}\n";
        }
    }
}
