using Game.ViewModel;

namespace Game
{
    public partial class App : Application
    {
        private readonly (int, int) INITIAL_WINDOW_SIZE = (750, 500);

        private AppShell _appShell;
        private Window _window;

        public App()
        {
            InitializeComponent();

            _appShell = new AppShell();
            _appShell.NewGame += AppShell_NewGame;
            MainPage = _appShell;
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            _window = base.CreateWindow(activationState);
            _window.Activated += Window_Activated;
            _window.Deactivated += Window_Deactivated;
            _window.Destroying += Window_Destroying;

            SetWindowSize(INITIAL_WINDOW_SIZE);

            return _window;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _appShell.Window_Activated();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            _appShell.Window_Deactivated();
        }

        private void Window_Destroying(object sender, EventArgs e)
        {
            _appShell.Window_Deactivated();
        }

        private void AppShell_NewGame(object sender, NewGameEventArgs e)
        {
            // Resize to appropriate size
            // Dummy resize :

            if (e.Size < 6)
            {
                SetWindowSize((750, 500));
            }
            else
            {
                SetWindowSize((800, 550));
            }
        }

        private void SetWindowSize((int, int) size)
        {
            _window.MinimumWidth = size.Item1;
            _window.MaximumWidth = size.Item1;
            _window.MinimumHeight = size.Item2;
            _window.MaximumHeight = size.Item2;
        }
    }
}
