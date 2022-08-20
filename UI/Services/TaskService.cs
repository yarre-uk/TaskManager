using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TaskManager.Services;
using UI.Models;

namespace UI.Services;
public class TaskService
{
    private readonly Repository repository;
    private List<TaskDTO> tasks;

    public TaskService()
    {
        repository = new Repository();

        tasks = repository.OnInit();

        System.Threading.Tasks.Task.Run(() =>
        {
            Start();
        });
    }

    public TaskDTO[] GetTasks() =>
        tasks.ToArray().OrderByDescending(x => x.FullTime).ToArray();

    private Process GetProcesse(string name) =>
        Process.GetProcessesByName(name).FirstOrDefault()!;

    public void Start()
    {
        tasks.ForEach(x => x.CurrentTime = 0);

        while (true)
        {
            var wait = System.Threading.Tasks.Task.Delay(60000);

            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                var process = GetProcesse(task.Name);

                if (process != null)
                {
                    task.FullTime++;
                    task.CurrentTime++;
                    task.State = true;
                    continue;
                }

                task.State = false;
            }

            wait.Wait();
        }
    }

    public void CheckState()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            var task = tasks[i];
            var process = GetProcesse(task.Name);

            if (process != null)
            {
                task.State = true;
                continue;
            }

            task.State = false;
        }

        repository.Save(tasks);
    }

    public void Add(string name)
    {
        if (tasks.Where(x => x.Name == name).Any())
        {
            return;
        }

        if (GetProcesse(name) == null)
        {
            return;
        }

        tasks.Add(new TaskDTO(name));
        repository.Save(tasks);
    }

    public void Delete(string name)
    {
        tasks = tasks.Where(x => x.Name != name).ToList();
        repository.Save(tasks);
    }
}
