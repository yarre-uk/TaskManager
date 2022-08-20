using System;

namespace UI.Models;

public class ProcessDTO
{
    public string Name { get; set; }
    public long Memory { get; set; }
    public string MemoryStr
    {
        get
        {
            string[] Suffix = { "B", "KB", "MB", "GB" };
            long bytes = Memory;
            double dblSByte = bytes;
            int i;

            for (i = 0;
                i < Suffix.Length && bytes >= 1024;
                i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.#} {1}", dblSByte, Suffix[i]);
        }
    }

    public ProcessDTO(string name, long memory)
    {
        Name = name;
        Memory = memory;
    }
}