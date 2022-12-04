using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace microservices.productsvc.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("product_id")]
        public int Id { get; set; }
        [Column("product_name")]
        public string? Name { get; set; }
        [Column("product_description")]
        public string? Description { get; set; }
        [Column("product_sku")]
        public string? Sku { get; set; }

    }
}
