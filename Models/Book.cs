using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VektorelProje.Models;

namespace VektorelProje.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Kitap Adı")]
        public string BookName { get; set; }
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Yazar Adı")]
        public string Author { get; set; }
        [Required]
        [Range(25, 5000)]
        [DisplayName("Fiyat")]
        public double price { get; set; }
        [ValidateNever]
        [DisplayName("Kitap Türü")]
        public int BookTypeId { get; set; }
        [ForeignKey("BookTypeId")]
        [ValidateNever]
        public BookType bookType { get; set; }
        [ValidateNever]
        [DisplayName("Kitap Resmi")]
        public string PictureUrl { get; set; }


    }
}
