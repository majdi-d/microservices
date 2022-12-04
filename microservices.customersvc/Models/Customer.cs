using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace microservices.customersvc.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("customer_id")]
        public int Id { get; set; }
        [Column("customer_name")]
        public string Name { get; set; }
        [Column("customer_mobileNumber")]
        public string MobileNumber { get; set; }
        [Column("customer_address")]
        public string Address { get; set; }
    }
}
