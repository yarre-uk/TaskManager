using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UI;

public class TaskService
{
    private List<Task> tasks;
    private string path = Directory.GetCurrentDirectory() + @"\save.txt";

    public TaskService()
    {
        try
        {
            using var sr = new StreamReader(path);
            tasks = JsonConvert.DeserializeObject<List<Task>>(sr.ReadToEnd())!;

            if (tasks.Count == 0)
            {
                throw new Exception();
            }
        }

        catch
        {
            tasks = new List<Task>
            {
                new Task("Opera"),
                new Task("Discord"),
                new Task("Telegram")
            };

            using var sw = new StreamWriter(path);
            sw.WriteLine(JsonConvert.SerializeObject(tasks));
        }

        System.Threading.Tasks.Task.Run(() =>
        {
            Start();
        });
    }

    public List<Task> GetTasks()
    {
        return tasks;
    }

    public void Start()
    {
        while (true)
        {
            var wait = System.Threading.Tasks.Task.Delay(60000);

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                var process = Process.GetProcessesByName(task.Name).FirstOrDefault();

                if (process != null)
                {
                    task.Time++;
                }
            }

            using (var sw = new StreamWriter(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(tasks));
            }

            wait.Wait();
        }
    }

    public void Add(string name)
    {
        if (tasks.Where(x => x.Name == name).Any())
        {
            return;
        }

        tasks.Add(new Task(name));

        using (var sw = new StreamWriter(path))
        {
            sw.WriteLine(JsonConvert.SerializeObject(tasks));
        }
    }

    public void Delete(string name)
    {
        tasks = tasks.Where(x => x.Name != name).ToList();

        using var sw = new StreamWriter(path);
        sw.WriteLine(JsonConvert.SerializeObject(tasks));
    }
}
