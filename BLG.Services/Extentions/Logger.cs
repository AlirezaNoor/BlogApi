using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace BLG.Services.Extentions
{
    public static class Logger
    {
        public static void WriteToConsole(Exception err,
           string context,  string programer , string des, [CallerLineNumber] int line = 0, [CallerFilePath] string file = null )
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}] [{1}] ({2}): {3}", PersianDateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Path.GetFileName(file), line, err.Message);
            Console.WriteLine($"Source = {err.Source}");
            Console.WriteLine($"InnerException = {err.InnerException}");
            Console.WriteLine($"StackTrace = {err.StackTrace}");
            Console.WriteLine($"Method = {err.TargetSite?.Name}");
            Console.WriteLine($"request = {context}");
            Console.WriteLine($"programer description = {des}");
            Console.ResetColor();
        }

        public static void WriteToConsole(string err, string context,[CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}] [{1}] ({2}): {3}", PersianDateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Path.GetFileName(file), line, err);
            Console.WriteLine($"request = {context}");
             Console.ResetColor();
        }

        public static void WriteToFile(Exception e)
        {
            AppendToFile(GenerateLog(e));
        }

        public static void WriteToFile(string e, [CallerFilePath] string file = null,
            [CallerLineNumber] int line = 0)
        {
            AppendToFile(GenerateLog(e, Path.GetFileName(file), line));
        }

        private static string GenerateLog(Exception err)
        {
            var log = @$"
--------------------------------------------------------------------------------------------------------------------------------------------------
                Datetime = {PersianDateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}

                Message = {err.Message}

                Source = {err.Source}

                InnerException = {err.InnerException}

                StackTrace = {err.StackTrace}

                Method = {err.TargetSite?.Name}
--------------------------------------------------------------------------------------------------------------------------------------------------
            ";
            return log;
        }

        private static string GenerateLog(string err, string source, int line)
        {
            var log = @$"
--------------------------------------------------------------------------------------------------------------------------------------------------
                Datetime = {PersianDateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}

                Message = {err}

                Source = {source} at line {line.ToString()}
--------------------------------------------------------------------------------------------------------------------------------------------------
            ";
            return log;
        }

        private static void AppendToFile(string log)
        {
            try
            {
                if (!Directory.Exists(@"Sandbox\LOG"))
                {
                    Directory.CreateDirectory(@"Sandbox\LOG");
                }

                File.AppendAllText(@"Sandbox\LOG\SITA_LOG.txt", log);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                File.AppendAllText("SITA_LOG.txt", GenerateLog("ACCESS DENIED TO PATH : Sandbox\\LOG", "Logger", 90));
                File.AppendAllText("SITA_LOG.txt", log);
            }
        }
    }
}