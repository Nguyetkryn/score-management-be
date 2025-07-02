using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace score_management_be.Models
{
    public class BaseModel
    {
        [Column("id"), Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Column("modified_by")]
        public string? ModifiedBy { get; set; }
        [Column("modified_date")]
        public DateTime? ModifiedDate { get; set; }
        [Column("status")]
        public bool IsStatus { get; set; } = true;
    }
}
