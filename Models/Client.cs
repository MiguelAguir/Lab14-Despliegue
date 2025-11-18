using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportesAPI.Models
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        [Column("ClientId")]
        public int ClientId { get; set; }

        [Column("Name")]
        public string Name { get; set; } = null!;

        [Column("Email")]
        public string Email { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}