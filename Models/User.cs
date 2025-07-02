using System.ComponentModel.DataAnnotations;

namespace score_management_be.Models
{
    public class User : BaseModel
    {
        // các thuộc tính này mà ko để ? thì nó sẽ bị lưu vào db là non null
        // tức muốn cập nhật hay tạo mới là phải nhập hết trường này mới được
        // không là lỗi db ngay
        // nếu chỉnh về ?
        // thì phai chạy lệnh migrations db rồi update theo model của mình
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string? Address { get; set; }
        public DateTime? Birth { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        //relationships
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public StudentUser? StudentUser { get; set; }
        public TeacherUser? TeacherUser { get; set; }
    }
}
