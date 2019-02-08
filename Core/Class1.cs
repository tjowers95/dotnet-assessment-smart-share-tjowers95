using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Dto
{
    [Table("data")]
    public class Files
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("path_name")]
        public string PathName { get; set; }
        [Column("down_path")]
        public string DownloadPath { get; set; }
        [Column("down_max")]
        public int MaxDownloads { get; set; }
        [Column("down_count")]
        public int DownloadCount { get; set; }
        [Column("created")]
        public string TimeStamp { get; set; }
        [Column("expiration")]
        public string Expiration { get; set; }
        [Column("file_data")]
        public byte[] FileData { get; set; }
        [Column("password")]
        public string Password { get; set; }
    }
}
