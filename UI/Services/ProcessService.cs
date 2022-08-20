using System.Diagnostics;
using System.Linq;
using UI.Models;

namespace UI.Services;

public class ProcessService
{
    public ProcessDTO[] GetProcess()
    {
        return MapProcesses(Process.GetProcesses());
    }

    public ProcessDTO[] MapProcesses(Process[] p)
    {
        ProcessDTO[] res = new ProcessDTO[p.Length];

        for (int i = 0; i < p.Length; i++)
        {
            var nP = new ProcessDTO(p[i].ProcessName, p[i].PrivateMemorySize64);
            res[i] = nP;
        }

        return res.OrderByDescending(x => x.Memory).ToArray();
    }
}
