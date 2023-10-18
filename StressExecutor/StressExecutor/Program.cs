using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;

namespace StressExecutor
{
    /// <summary>
    /// read https://stackoverflow.com/questions/9679375/how-can-i-run-an-exe-file-from-my-c-sharp-code
    /// https://stackoverflow.com/questions/177856/how-do-i-trap-ctrlc-sigint-in-a-c-sharp-console-app
    /// https://stackoverflow.com/questions/19758741/catching-ctrlc-event-in-console-application-multi-threaded
    /// 
    /// https://stackoverflow.com/questions/2863683/how-to-find-if-a-file-is-an-exe
    /// https://learn.microsoft.com/en-us/previous-versions/ms809762(v=msdn.10)?redirectedfrom=MSDN
    /// https://learn.microsoft.com/en-us/archive/msdn-magazine/2002/february/inside-windows-win32-portable-executable-file-format-in-detail
    /// </summary>
    internal class Program
    {
        private static List<int> processes = new List<int>();
        private static string processName = "";
        private static string workingDir = "";

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            //Process proc = Process.GetProcessById(67992);
            //try
            //{
            //    proc.Kill(true);
            //}
            //catch(Exception err)
            //{
            //    Console.WriteLine(err.ToString());
            //}

            //return;

            if (args.Length != 2)
            {
                Console.WriteLine("Wrong usage. 2 Params are needed");
                Help();
                return;
            }

            if (!int.TryParse(args[0], out int times) || times <= 0)
            {
                Console.WriteLine("first argument must be an integer number > 0");
                Help();
                return;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine($"File not exists {args[1]}");
                Help();
                return;
            }



            Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e) {


                CloseAll();
                
                Environment.Exit(0);
            };

            DoTest(times, args[1]);

            string lastCommand = "";

            while (lastCommand != "exit")
            {
                lastCommand = Console.ReadLine()?.ToLower()?.Trim() ?? "";

                if (lastCommand == "exit")
                {
                    CloseAll();
                    return;
                }
            }

            CloseAll();


        }


        static void Help()
        {
            Console.WriteLine("Usage: StressExecutor <Number Of Times : int non zero non negative> <Path To exe : string>");
        }

        static void DoTest(int times, string exePath)
        {
            processName = Path.GetFileNameWithoutExtension(exePath);
            workingDir = Path.GetDirectoryName(exePath);

            Console.WriteLine($"Running {processName} {times} times in {workingDir} directory");

            for (int i=0; i<times; i++)
            {
                //https://stackoverflow.com/questions/9679375/how-can-i-run-an-exe-file-from-my-c-sharp-code

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.FileName = exePath;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;
                startInfo.WorkingDirectory = workingDir;
                startInfo.Verb = "runas";
                startInfo.RedirectStandardOutput = false;
                startInfo.RedirectStandardError = false;
                //startInfo.Arguments = xxx

                Process exeProcess = Process.Start(startInfo);
                processes.Add(exeProcess?.Id??0);
                Console.WriteLine($"ProcessId: {exeProcess?.Id}");
                
            }
        }

        static void CloseAll()
        {
            foreach(var p in processes)
            {
                if (p != 0)
                {
                    Process proc = Process.GetProcessById(p);
                    try
                    {
                        
                        
                        proc?.Kill(true);
                        if (!proc?.WaitForExit(1000)??false)
                        {
                            proc?.Kill(true);
                        }
                        
                    }
                    catch (Exception err)
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                        //Console.WriteLine($"Error: {err}");
                    }
                    finally
                    {
                        if(proc != null)
                        {
                            if(proc.ExitCode == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Proccess: {proc.Id} sucessfully terminated");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Proccess: {proc.Id} terminated with exit code {proc.ExitCode}");
                            }

                            proc?.Close();
                            proc?.Dispose();
                        }
                    }

                }
            }
        }


    }
}