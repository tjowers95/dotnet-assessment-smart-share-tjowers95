using System;
using System.Xml.Serialization;

namespace Core.Dto
{
    public enum UserAction
    {
        [XmlEnum("upload")]
        upload,
        [XmlEnum("download")]
        download,
        [XmlEnum("view")]
        view
    }

    [Serializable]
    [XmlRoot("ClientToServerDTO")]
    public class ClientToServerDTO
    {
        
        public UserAction UserAction { get; set; }
        public string Filename { get; set; }
        public string Password { get; set; }
        public int MaxDown { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public byte[] FileData { get; set; }
    }
}