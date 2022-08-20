using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Models;

namespace TaskManager.Services;

public class Repository
{
    private readonly string path = Directory.GetCurrentDirectory() + @"\save.txt";

    public void Save(List<TaskDTO> tasks)
    {
        using (var sw = new StreamWriter(path))
        {
            sw.WriteLine(JsonConvert.SerializeObject(tasks, new JsonSerializerSettings() { Formatting = Formatting.Indented }));
        }
    }

    public List<TaskDTO> OnInit()
    {
        List<TaskDTO> tasks;

        try
        {
            using var sr = new StreamReader(path);
            tasks = JsonConvert.DeserializeObject<List<TaskDTO>>(sr.ReadToEnd(), new JsonSerializerSettings() { Formatting = Formatting.Indented })!;

            if (tasks.Count == 0)
            {
                throw new Exception();
            }
        }

        catch
        {
            tasks = new List<TaskDTO>
            {
                new TaskDTO("Opera"),
                new TaskDTO("Discord"),
                new TaskDTO("Telegram")
            };

            using var sw = new StreamWriter(path);
            sw.WriteLine(JsonConvert.SerializeObject(tasks, new JsonSerializerSettings() { Formatting = Formatting.Indented }));
        }

        return tasks;
    }
}
