for (long q = 1; q < 100000000000; q += 5000000)
{
    Console.WriteLine(FormatBytes(q));
    Thread.Sleep(5);
}

string FormatBytes(long bytes)
{
    string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
    int i;
    double dblSByte = bytes;
    for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
    {
        dblSByte = bytes / 1024.0;
    }

    return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
}