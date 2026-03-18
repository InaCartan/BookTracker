using Postgrest.Attributes;
using Postgrest.Models;

namespace BooksTracker.Models
{
    // BaseModel is from Postgrest library 

    [Table("books")]
    public class Book : BaseModel 
    {
        // Id is automatically added to the table in supabase
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        // made_at is automatically added to the table in supabase 
        // made_at's name has been changed from it's originally name
        // made_at generates a timestamp for each row upon insertion
        // timestamp = date/time when an event occured
        [Column("made_at", ignoreOnInsert: true)]
        public DateTime MadeAt { get; set; }

        [Column("is_finished")]
        public bool IsFinished { get; set; }



    }
}
