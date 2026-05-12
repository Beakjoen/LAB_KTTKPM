using Dapper;
using Microsoft.Data.SqlClient;

namespace StudentManagement
{
    /// <summary>
    /// Tầng truy cập dữ liệu: SqlConnection + Dapper cho CRUD và tìm kiếm trên bảng Students.
    /// Trước khi chạy, cần tạo DB StudentDB và bảng Students (xem script gợi ý trong file Program.cs hoặc XML của Repository).
    /// </summary>
    public class StudentRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Giữ chuỗi kết nối để mở kết nối ngắn gọn cho từng thao tác (chuẩn ADO.NET).
        /// </summary>
        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>Tạo kết nối SQL Server mới — dùng <c>using</c> để đảm bảo dispose.</summary>
        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>Lấy toàn bộ bản ghi từ bảng Students.</summary>
        public async Task<List<Student>> GetAllAsync()
        {
            const string sql = "SELECT * FROM Students ORDER BY Id;";
            await using var connection = CreateConnection();
            var rows = await connection.QueryAsync<Student>(sql);
            return rows.AsList();
        }

        /// <summary>Lấy một sinh viên theo Id khóa chính.</summary>
        public async Task<Student?> GetAsync(int id)
        {
            const string sql = "SELECT * FROM Students WHERE Id = @Id;";
            await using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Student>(sql, new { Id = id });
        }

        /// <summary>
        /// Thêm mới; Id do cột IDENTITY sinh. Sau khi gọi, <paramref name="student"/>.Id được gán giá trị mới.
        /// </summary>
        public async Task AddAsync(Student student)
        {
            const string sql = @"
INSERT INTO Students (Name, Email, Address, Age, Grade)
VALUES (@Name, @Email, @Address, @Age, @Grade);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

            await using var connection = CreateConnection();
            // Mở kết nối rõ ràng cho ExecuteScalarAsync
            await connection.OpenAsync();
            var newId = await connection.ExecuteScalarAsync<int>(sql, student);
            student.Id = newId;
        }

        /// <summary>Cập nhật theo Id; trả về true nếu có ít nhất một dòng bị ảnh hưởng.</summary>
        public async Task<bool> UpdateAsync(Student student)
        {
            const string sql = @"
UPDATE Students
SET Name = @Name, Email = @Email, Address = @Address, Age = @Age, Grade = @Grade
WHERE Id = @Id;";

            await using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync(sql, student);
            return affected > 0;
        }

        /// <summary>Xóa theo Id; trả về true nếu xóa được một dòng.</summary>
        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Students WHERE Id = @Id;";
            await using var connection = CreateConnection();
            var affected = await connection.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        /// <summary>
        /// Tìm theo Id (khớp chính xác nếu từ khóa là số) hoặc LIKE trên Name, Address, Grade.
        /// Từ khóa rỗng: trả về toàn bộ (thuận tiện cho UI).
        /// </summary>
        public async Task<List<Student>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetAllAsync();
            }

            var kw = keyword.Trim();
            int? idMatch = int.TryParse(kw, out var parsedId) ? parsedId : null;
            var likePattern = "%" + kw + "%";

            // Tham số hóa toàn bộ — tránh nối chuỗi SQL (SQL injection)
            const string sql = @"
SELECT * FROM Students
WHERE (@IdMatch IS NOT NULL AND Id = @IdMatch)
   OR Name LIKE @LikePattern
   OR Address LIKE @LikePattern
   OR Grade LIKE @LikePattern
ORDER BY Id;";

            await using var connection = CreateConnection();
            var rows = await connection.QueryAsync<Student>(sql, new
            {
                IdMatch = idMatch,
                LikePattern = likePattern
            });

            return rows.AsList();
        }
    }
}
