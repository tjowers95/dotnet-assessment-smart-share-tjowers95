using System;
using System.Collections.Generic;

namespace Client
{
    public sealed class Tester
    {
        private static readonly List<string[]> TestArgs = new List<string[]>
        {
            // Experiment with different command line arguments here
            new[] {""},
            new[] {"--help"},
            new[] {"--version"},
            new[] {"upload"},
            new[] {"upload", "Program.cs"},
            new[] {"upload", "Program.cs", "password123"},
            new[] {"download"},
            new[] {"download", "Program.cs"},
            new[] {"download", "Program.cs", "p@ssw0rd"},
            new[] {"download", "Program.cs", "password123"}
        };

        public static void RunTestArgs()
        {
            int runCount = 1;
            foreach (var args in TestArgs)
            {
                Console.WriteLine($"\n*** RUN #{runCount++}, args: { String.Join(" ", args)}");
                Program.RunCommandArgs(args);
            }
        }
    }
}