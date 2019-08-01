using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TeleprompterConsole;

namespace test
{
    class Program
    {
        static private string FilePath { get; set; } = @"C:\Users\yamas\Desktop\test\sampleQuotes.txt";
        static void Main(string[] args)
        {
            Console.Write("test");
            RunTeleprompter().Wait();
        }

        static IEnumerable<string> GetFileContent(string file)
        {
            using (var rs = File.OpenText(file))
            {
                string line = String.Empty;
                while ((line = rs.ReadLine()) != null)
                {
                    var lineLength = 0;
                    var words = line.Split(" ");
                    foreach (var word in words)
                    {
                        lineLength += word.Length + 1;
                        if (70 < lineLength)
                        {
                            yield return Environment.NewLine;
                            lineLength = 0;
                        }
                        yield return word + " ";
                    }
                    yield return Environment.NewLine;
                }
            }
        }

        private static async Task ShowTeleprompter(TelePrompterConfig config)
        {
            var words = GetFileContent(FilePath);
            foreach (var word in words)
            {
                Console.Write(word);
                if (!string.IsNullOrWhiteSpace(word))
                {
                    await Task.Delay(config.DelayInMilliseconds);
                }
            }
            config.SetDone();
        }

        private static async Task GetInput(TelePrompterConfig config)
        {
            var delay = 200;
            Action work = () =>
            {
                do
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == '>')
                        config.UpdateDelay(-10);
                    else if (key.KeyChar == '<')
                        config.UpdateDelay(10);
                    else if (Char.ToUpper(key.KeyChar) == 'X')
                        config.SetDone();
                } while (!config.Done);
            };
            await Task.Run(work);
        }

        private static async Task RunTeleprompter()
        {
            var config = new TelePrompterConfig();
            var displayTask = ShowTeleprompter(config);
            var speedTask = GetInput(config);
            await Task.WhenAny(displayTask, speedTask);
        }
    }
}
