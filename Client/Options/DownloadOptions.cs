using System;
using CommandLine;

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
            // TODO
            Console.WriteLine($"Attempting to download file: {options.FileName}");
            return 0;
        }
    }
}