namespace BravoOne.lib.Objects
{
    public class Options
    {
        public bool AutoSave { get; set; }

        // 1 = Easy ($200k), 2 = Normal ($100k), 3 = Hard ($50k)
        public int Difficulty { get; set; } = 2;
    }
}