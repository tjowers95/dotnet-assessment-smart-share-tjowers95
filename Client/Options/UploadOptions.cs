using System;
using System.IO;
using System.Net.Sockets;
using Client.Utils;
using CommandLine;
using System.Xml.Serialization;
using Core.Dto;

namespace Client.Options
{
    [Verb("upload", HelpText = "Upload A File To The Online Repository")]
    public class UploadOptions
    {
        [Value(0, MetaName = "Filename", HelpText = "The File to Upload, Relative Path or Absolute Path", Required = true)]
        public string FileName { get; set; }

        [Value(1, MetaName = "Password", HelpText = "Password for the File", Required = false)]
        public string Password { get; set; } = PasswordGenerator.Generate();

        [Value(2, MetaName = "Download Constraint", HelpText = "Restrict the Number of Downloads", Required = false)]
        public int MaximumDownloads { get; set; } = 5;

        [Value(3, MetaName = "Expiration", HelpText = "Expiration of The File in Minutes From the Time Uploaded", Required = false)]
        public double Expiration { get; set; } = 30;
        
        public static int ExecuteUploadAndReturnExitCode(UploadOptions options)
        { 
            // TODO
            try
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(ClientToServerDTO));

                FileInfo file = new FileInfo(options.FileName);

                ClientToServerDTO toServerDTO = new ClientToServerDTO
                {
                    Filename = options.FileName,
                    Password = options.Password,
                    MaxDown = options.MaximumDownloads,
                    Created = DateTime.Now.ToString(),
                    UserAction = UserAction.upload,

                    Expiration = DateTime.Now.AddMinutes(options.Expiration).ToString()
                };

                FileStream Upload      = new FileStream(file.FullName, FileMode.Open, FileAccess.ReadWrite);
                toServerDTO.FileData   = new byte[Upload.Length];


                Upload.Read(toServerDTO.FileData, 0, (int)Upload.Length);

                // TCP - Internet Connection
                TcpClient clientConnection = new TcpClient("127.0.0.1", 1234);

                using (NetworkStream stream = clientConnection.GetStream())
                {
                    Serializer.Serialize(stream, toServerDTO);
                }

                Console.WriteLine($"Uploading {options.FileName}");
                Console.WriteLine($"Repository Access Password: {options.Password}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }
    }
}