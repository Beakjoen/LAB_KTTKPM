using MongoDB.Driver;

namespace StudentManagement
{
    /// <summary>
    /// Tầng truy cập dữ liệu: MongoDB Driver cho CRUD và tìm kiếm trên collection Students.
    /// </summary>
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _collection;

        /// <summary>
        /// Khởi tạo kết nối MongoDB và lấy collection Students.
        /// </summary>
        public StudentRepository(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<Student>("Students");
        }

        /// <summary>Lấy toàn bộ sinh viên từ collection Students.</summary>
        public async Task<List<Student>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        /// <summary>Lấy một sinh viên theo Id (ObjectId).</summary>
        public async Task<Student?> GetAsync(string id)
        {
            return await _collection.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>Thêm mới sinh viên; MongoDB tự động sinh ObjectId.</summary>
        public async Task AddAsync(Student student)
        {
            await _collection.InsertOneAsync(student);
        }

        /// <summary>Cập nhật sinh viên theo Id; trả về true nếu thành công.</summary>
        public async Task<bool> UpdateAsync(Student student)
        {
            var result = await _collection.ReplaceOneAsync(s => s.Id == student.Id, student);
            return result.ModifiedCount > 0;
        }

        /// <summary>Xóa sinh viên theo Id; trả về true nếu xóa được.</summary>
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(s => s.Id == id);
            return result.DeletedCount > 0;
        }

        /// <summary>
        /// Tìm kiếm sinh viên theo Id, Name, Address hoặc Grade.
        /// Từ khóa rỗng: trả về toàn bộ.
        /// </summary>
        public async Task<List<Student>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetAllAsync();
            }

            var kw = keyword.Trim();
            
            // Tìm kiếm theo Id (ObjectId) hoặc các trường text
            var filter = Builders<Student>.Filter.Or(
                Builders<Student>.Filter.Eq(s => s.Id, kw),
                Builders<Student>.Filter.Regex(s => s.Name, new MongoDB.Bson.BsonRegularExpression(kw, "i")),
                Builders<Student>.Filter.Regex(s => s.Address, new MongoDB.Bson.BsonRegularExpression(kw, "i")),
                Builders<Student>.Filter.Regex(s => s.Grade, new MongoDB.Bson.BsonRegularExpression(kw, "i"))
            );

            return await _collection.Find(filter).ToListAsync();
        }
    }
}
