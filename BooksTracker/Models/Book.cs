using Postgrest.Attributes;
using Postgrest.Models;

namespace BooksTracker.Models
{
    // BaseModel is from Postgrest library (that is included in the supbase client)

    [Table("books")]
    public class Book : BaseModel 
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("made_at", ignoreOnInsert: true)]
        public DateTime MadeAt { get; set; }

        [Column("is_finished")]
        public bool IsFinished { get; set; }



    }
}
