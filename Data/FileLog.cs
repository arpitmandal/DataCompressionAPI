using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataCompressionAPI.Data
{
    public class FileLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string InitialSize { get; set; }

        public string ReducedSize { get; set; }

        public string FinalSize { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("User")]
        public int CreatedBy { get; set; }
        public User User { get; set; }

        public string Error { get; set; } // Optional field
    }
}