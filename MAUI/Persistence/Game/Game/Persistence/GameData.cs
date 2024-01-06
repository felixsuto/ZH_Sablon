namespace Game.Persistence
{
    public class GameData
    {
        public int Size { get; private set; }

        private int[,] Fields;
        public int GetField(int i, int j) { return Fields[i, j]; }
        public void SetField(int i, int j, int val) { Fields[i, j] = val; }
        // Add extra entries as needed

        public GameData(int size)
        {
            Size = size;
            Fields = new int[Size, Size];
        }
    }
}
