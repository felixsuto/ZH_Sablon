using System.Timers;

namespace Game.Model
{
    public class GameModel
    {
        private const int TIMER_INTERVAL = 500;
        private System.Timers.Timer _timer;
        public bool IsPaused { get { return !_timer.Enabled; } }
        public TimeSpan ElapsedTime { get; private set; }

        public int Size { get; private set; }

        private int[,] Fields;
        public int GetField(int i, int j)
        {
            return Fields[i, j];
        }

        public event EventHandler<StateChangedEventArgs>? StateChanged;
        public event EventHandler<GameOverEventArgs>? GameOver;
        public event EventHandler<ElapsedEventArgs>? Elapsed;
        public event EventHandler? Paused;
        public event EventHandler? Resumed;

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

            _timer = new System.Timers.Timer();
            _timer.Interval = TIMER_INTERVAL;
            _timer.AutoReset = true;
            _timer.Elapsed += Timer_Elapsed;

            ElapsedTime = TimeSpan.Zero;
        }

        public void StartGame()
        {
            _timer.Start();
        }

        public void Pause()
        {
            if (!IsPaused)
            {
                _timer.Stop();
                Paused?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Resume()
        {
            if (IsPaused)
            {
                _timer.Start();
                Resumed?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            // Do things

            ElapsedTime += TimeSpan.FromMilliseconds(TIMER_INTERVAL);
            Elapsed?.Invoke(sender, e);
        }
    }
}
