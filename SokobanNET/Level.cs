namespace SokobanNET
{
    public class Level
    {
        // TODO: i think this should be in another assembly: every setter should be only used by LevelCollection
        public string[] Data { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
