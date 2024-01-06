using Game.Model;
using Game.ViewModel;

namespace Game
{
    public partial class AppShell : Shell
    {
        private const int INITIAL_SIZE = 5;

        private GameModel _model;
        private GameViewModel _viewModel;

        public event EventHandler<NewGameEventArgs> NewGame;

        public AppShell()
        {
            InitializeComponent();

            _model = new GameModel(INITIAL_SIZE);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            BindingContext = _viewModel;
        }

        private void ViewModel_NewGame(object sender, NewGameEventArgs e)
        {
            _viewModel.NewGame -= ViewModel_NewGame;
            _model = new GameModel(e.Size);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            BindingContext = _viewModel;
            NewGame.Invoke(this, e);
        }

        public void Window_Deactivated()
        {
            if (!_viewModel.Paused) _viewModel.TogglePause();
        }
    }
}
