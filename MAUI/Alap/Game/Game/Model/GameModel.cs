namespace Game.Model
{
    public class GameModel
    {
        public int Size { get; private set; }

        private int[,] Fields;
        public int GetField(int i, int j)
        {
            return Fields[i, j];
        }

        public event EventHandler<StateChangedEventArgs> StateChanged;
        public event EventHandler<GameOverEventArgs> GameOver;

        public GameModel(int size)
        {
            Size = size;

            Fields = new int[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Fields[i, j] = 0;
                }
            }
        }
    }
}
