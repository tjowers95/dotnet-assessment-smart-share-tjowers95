using System;
using System.IO;
using System.Net.Sockets;
using Client.Utils;
using CommandLine;
using System.Xml.Serialization;
using Core.Dto;

namespace Client.Options
{
    [Verb("view", HelpText = "View File's Meta-Data")]
    class ViewOptions
    {
        [Value(0, MetaName = "filename", HelpText = "Unique name of the file to view", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "password", HelpText = "Password", Required = true)]
        public string Password { get; set; }

        public static int ExecuteViewAndReturnExitCode(ViewOptions options)
        {
            Console.WriteLine($"Attempting to View {options.FileName} From Online Repository");

            XmlSerializer toServerSerialize = new XmlSerializer(typeof(ClientToServerDTO));
            XmlSerializer fromServerDeserialize = new XmlSerializer(typeof(ServerToClientDTO));

            ClientToServerDTO toServerDTO = new ClientToServerDTO();

            toServerDTO.Filename   = options.FileName;
            toServerDTO.Password   = options.Password;
            toServerDTO.UserAction = UserAction.view;
            toServerDTO.Expiration = "NULL";
            toServerDTO.Created    = "NULL";

            ServerToClientDTO fromServerDTO = new ServerToClientDTO();

            try
            {
                TcpClient clientConnection = new TcpClient("127.0.0.1", 1234);
                using (NetworkStream stream = clientConnection.GetStream())
                {
                    toServerSerialize.Serialize(stream, toServerDTO);
                    clientConnection.Client.Shutdown(SocketShutdown.Send);
                    fromServerDTO = (ServerToClientDTO)fromServerDeserialize.Deserialize(stream);
                };

                Console.WriteLine($"{fromServerDTO.Filename} Created {fromServerDTO.Created} Expires {fromServerDTO.Expiration}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 0;
        }
    }
}
