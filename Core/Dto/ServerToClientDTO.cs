using System;
using System.Xml.Serialization;

namespace Core.Dto
{
    [Serializable]
    [XmlRoot("ServerToClientDTO")]
    public class ServerToClientDTO
    {
        public string Filename   { get; set; }
        public string Expiration { get; set; }
        public int    MaxDown    { get; set; }
        public int    DownCount  { get; set; }
        public string Created    { get; set; }
        public byte[] FileData   { get; set; }
    }
}
