using Newtonsoft.Json;
using System.Diagnostics;

namespace Project;

static partial class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\Users\yaros\Desktop\save.txt";
        List<Task> tasksForTrack = new List<Task>();

        try
        {
            using (var sr = new StreamReader(path))
            {
                tasksForTrack = JsonConvert.DeserializeObject<List<Task>>(sr.ReadToEnd())!;

                if (tasksForTrack.Count == 0)
                {
                    throw new Exception();
                }
            }
        }

        catch
        {
            tasksForTrack.Add(new Task("Opera"));
            tasksForTrack.Add(new Task("Discord"));
            tasksForTrack.Add(new Task("Telegram"));
        }

        while (true)
        {
            var wait = System.Threading.Tasks.Task.Delay(1000);

            for (int i = 0; i < tasksForTrack.Count; i++)
            {
                var task = tasksForTrack[i];
                var process = Process.GetProcessesByName(task.Name).FirstOrDefault();

                if (process != null)
                {
                    task.Time++;
                }
            }

            Console.Clear();

            foreach (var task in tasksForTrack)
            {
                Console.WriteLine(task);
            }

            Console.WriteLine(new String('-', 10));

            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(tasksForTrack));
            }

            wait.Wait();
        }
    }
}