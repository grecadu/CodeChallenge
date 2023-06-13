using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BLueCodeChanllenge.Models
{
    public class ShortUrls
    {
        public ShortUrls()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? ShortUrl { get; set; }

        public string? LongUrl { get; set; }

        public int Counter { get; set; }
      
    }

    
}
