using System;

namespace UI;

public class Task
{
    public string Name { get; set; }
    public int Time { get; set; }
    public string TimeStr => $"{Time / 60}h {Time % 60}m";

    public Task(string name)
    {
        Name = name;
    }

    public override string ToString() => $"{Name} - {Time} seconds";
}