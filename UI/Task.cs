using System;

namespace UI;

public class Task
{
    public string Name { get; set; }

    public int FullTime { get; set; }
    public string FullTimeStr => $"{FullTime / 60}h {FullTime % 60}m";
    public int CurrentTime { get; set; }
    public string CurrentTimeStr => $"{CurrentTime / 60}h {CurrentTime % 60}m";

    public bool State { get; set; }
    public string StateStr => (State) ? "Running" : "Stopped";

    public Task(string name)
    {
        Name = name;
    }
}
