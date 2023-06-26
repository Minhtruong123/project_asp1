using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("TypeSong")]
    public class TypeSong
    {
        [Key]
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Song> Song { get; set; }
    }
}