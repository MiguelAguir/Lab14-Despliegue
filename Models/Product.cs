using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportesAPI.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column("ProductId")]
        public int ProductId { get; set; }

        [Column("Name")]
        public string Name { get; set; } = null!;

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new();
    }
}