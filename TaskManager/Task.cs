namespace Project;

static partial class Program
{
    class Task
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public Task(string name)
        {
            Name = name;
        }

        public override string ToString() => $"{Name} - {Time} seconds";
    }
}