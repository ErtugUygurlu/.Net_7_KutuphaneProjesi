using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VektorelProje.Models
{
    public class BookType
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage ="Bu Alan Boş Bırakılamaz!")]
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        public string Name { get; set; }


    }
}
 