using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string NameSong { get; set; }
        public string PublicationDate { get; set; }
        [ForeignKey("TypeSong")]
        public int TypeSongId { get; set; }
    }
}