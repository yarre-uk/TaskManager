using System;

namespace UI;

public class ProcessDTO
{
    public string Name { get; set; }
    public long Memory { get; set; }
    public string MemoryStr
    {
        get
        {
            string[] suffixes = { " B", " KB", " MB", " GB", " TB", " PB" };

            for (int i = 0; i < suffixes.Length; i++)
            {
                long temp = Memory / (int)Math.Pow(1024, i + 1);

                if (temp == 0)
                {
                    return (Memory / (int)Math.Pow(1024, i)) + suffixes[i];
                }
            }

            return Memory.ToString();
        }
    }

    public ProcessDTO(string name, long memory)
    {
        Name = name;
        Memory = memory;
    }
}