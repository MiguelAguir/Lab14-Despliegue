using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportesAPI.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("ClientId")]
        public int ClientId { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }

        public Client Client { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}