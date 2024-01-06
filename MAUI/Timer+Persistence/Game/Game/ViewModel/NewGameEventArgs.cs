namespace Game.ViewModel
{
    public class NewGameEventArgs: EventArgs
    {
        public int Size { get; private set; }

        public NewGameEventArgs(int size)
        {
            Size = size;
        }
    }
}
