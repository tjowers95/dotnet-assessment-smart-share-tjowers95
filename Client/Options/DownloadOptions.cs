using System;
using System.Net.Sockets;
using CommandLine;
using System.Linq;
using System.Xml.Serialization;
using Core.Dto;
using System.IO;

namespace Client.Options
{
    [Verb("download", HelpText = "Downloads a file provided the correct password is given")]
    public class DownloadOptions
    {
        [Value(0, MetaName = "filename", HelpText = "Unique name of file to be downloaded", Required = true)]
        public string FileName { get; set; }
        
        [Value(1, MetaName = "password", HelpText = "Password used to access file", Required = true)]
        public string Password { get; set; }


        public static int ExecuteDownloadAndReturnExitCode(DownloadOptions options)
        {
            Console.WriteLine($"Attempting to Fetch {options.FileName} From the Online Repository");

            XmlSerializer Serializer = new XmlSerializer(typeof(ClientToServerDTO));
            XmlSerializer Deserializer = new XmlSerializer(typeof(ServerToClientDTO));

            ClientToServerDTO toServerDTO = new ClientToServerDTO();

            toServerDTO.Filename = options.FileName;
            toServerDTO.Password = options.Password;
            toServerDTO.UserAction = UserAction.download;
            toServerDTO.Created = "NULL"; 
            toServerDTO.Expiration = "NULL";

            ServerToClientDTO fromServerDTO = new ServerToClientDTO();

            try
            {
                TcpClient clientConnection = new TcpClient("127.0.0.1", 1234);
                using (NetworkStream stream = clientConnection.GetStream())
                {
                    Serializer.Serialize(stream, toServerDTO);
                    clientConnection.Client.Shutdown(SocketShutdown.Send);
                    fromServerDTO = (ServerToClientDTO)Deserializer.Deserialize(stream); 
                };

                FileInfo file = new FileInfo(options.FileName);
                using (FileStream stream = new FileStream(file.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    stream.Write(fromServerDTO.FileData, 0, fromServerDTO.FileData.Length);
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

          /*  try
            {
                XmlSerializer Deserializer = new XmlSerializer(typeof(Client));
                XmlSerializer Serializer = new XmlSerializer(typeof(Files));



                TcpClient clientConnection = new TcpClient("127.0.0.1", 1234);
                using (NetworkStream stream = clientConnection.GetStream())
                {
                    Files DTO = new Files();

                    DTO.FileName = options.FileName;
                    DTO.Password = options.Password;

                    Serializer.Serialize(stream, DTO);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
            return 0;
        }
    }
}