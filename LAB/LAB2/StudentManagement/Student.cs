using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StudentManagement
{
    /// <summary>
    /// Thực thể sinh viên ánh xạ collection Students trên MongoDB.
    /// </summary>
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("grade")]
        public string Grade { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"ID: {Id}\nTên: {Name}\nEmail: {Email}\nĐịa chỉ: {Address}\nTuổi: {Age}\nLớp: {Grade}\n";
        }
    }
}
