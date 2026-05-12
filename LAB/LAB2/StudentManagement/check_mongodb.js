// Script để kiểm tra dữ liệu trong MongoDB
// Chạy: mongosh < check_mongodb.js

use StudentDB

print("\n=== THÔNG TIN DATABASE ===")
print("Database: " + db.getName())
print("Collections: " + db.getCollectionNames())

print("\n=== DANH SÁCH SINH VIÊN ===")
print("Tổng số sinh viên: " + db.Students.countDocuments())

print("\n=== CHI TIẾT SINH VIÊN ===")
db.Students.find().forEach(function(student) {
    print("\n---")
    print("ID: " + student._id)
    print("Tên: " + student.name)
    print("Email: " + student.email)
    print("Địa chỉ: " + student.address)
    print("Tuổi: " + student.age)
    print("Lớp: " + student.grade)
})
