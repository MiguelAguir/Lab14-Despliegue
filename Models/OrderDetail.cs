using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportesAPI.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column("OrderDetailId")]
        public int OrderDetailId { get; set; }

        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        public Order Order { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}