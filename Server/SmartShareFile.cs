using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server
{
    [Table("smart_share_file")]
    public class SmartShareFile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("file_name")]
        public string Filename { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("maximum_downloads")]
        public int MaximumDownloads { get; set; }

        [Column("download_count")]
        public int DownloadCount { get; set; }

        [Column("created")]
        public string Created { get; set; }

        [Column("expiration")]
        public string Expiration { get; set; }

        [Column("file_data")]
        public byte[] FileData { get; set; }
    }
}
