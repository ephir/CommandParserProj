using System;
using System.Collections.Generic;
using System.Threading;

namespace CommandParser
{
    class ConsoleProg
    {

        private static readonly Dictionary<string, string> availableCommands = new Dictionary<string, string>
        {
            { "/?", "Call help" },
            { "/help", "Call help" },
            { "-help", "Call help" },
            { "-k", "CommandParser.exe -k key1 value1 key2 value 2 - outputs Key - Value" },
            { "-ping", "Call ping" },
            { "-print", "Print data" }
        };

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                OutputHelp();
            }
            CommandValidator(args);
            CommandParser(args);
        }


        private static void CommandParser(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (availableCommands.ContainsKey(args[i]))
                    CommandExecutor(args, args[i], i);
            }
        }



        private static void CommandValidator(string[] args)
        {
            if (!availableCommands.ContainsKey(args[0]))
            {
                Console.WriteLine();
                Console.WriteLine("Command " + args[0] + " is not supported!!!");
                Console.WriteLine("Use CommandParser.exe /? to see set of allowed commands");
                Environment.Exit(0);
            }
        }

        private static void CommandExecutor(string[] args, string command, int pos)
        {
            if (availableCommands[command] == "Call help" && pos == 0) OutputHelp();
            if (availableCommands[command] == "Call ping") CallPing();
            if (availableCommands[command] == "Print data") OutputSingleString(args, pos);
            if (availableCommands[command] == "CommandParser.exe -k key1 value1 key2 value 2 - outputs Key - Value") OutputDictionary(args, pos);
        }

        private static void OutputSingleString(string[] args, int pos)
        {
            Console.WriteLine();
            if (pos == args.Length - 1) { return; }
            string argsString = "";
            for (int i = pos + 1; i < args.Length; i++)
            {
                if (!availableCommands.ContainsKey(args[i])) argsString += args[i] + " ";
                if (availableCommands.ContainsKey(args[i]) || i == args.Length - 1) { Console.WriteLine(argsString); return; }
            }
        }

        private static void OutputDictionary(string[] args, int pos)
        {
            Console.WriteLine();
            if (pos == args.Length - 1) { return; }
            Dictionary<int, string> argsStringDict = new Dictionary<int, string>();
            int p = 0;
            for (int i = pos + 1; i < args.Length; i++)
            {
                if (!availableCommands.ContainsKey(args[i])) { argsStringDict.Add(p, args[i]); p++; }
                if (availableCommands.ContainsKey(args[i]) || i == args.Length - 1) break;
            }
            for (int k = 0; k < p / 2; k++)
            {
                Console.WriteLine(argsStringDict[k * 2] + "    " + argsStringDict[k * 2 + 1]);
            }
            if (p % 2 != 0)
                Console.WriteLine(argsStringDict[p - 1] + "    " + "NULL");

        }

        private static void CallPing()
        {
            Console.WriteLine();
            Console.WriteLine("Pinging...");
            Console.WriteLine("Please press any key to continue work");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key != 0))
            {
                Console.Beep();
                Thread.Sleep(1000);
            }

        }

        private static void OutputHelp()
        {
            Console.WriteLine();
            foreach (string key in availableCommands.Keys)
            { Console.WriteLine("[" + key + "]" + "   " + availableCommands[key]); }
            Environment.Exit(0);
        }
    }
}
