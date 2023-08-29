using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VektorelProje.Models
{
    public class Hire
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Müşteri Id")]

        public int CustomerId { get; set; }
        [ValidateNever]
        [DisplayName("Kitap Id")]

        public int BookId { get; set; }

        [ForeignKey("BookId")]

        [ValidateNever]
        public Book Book { get; set; }
    }
}
