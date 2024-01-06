using System.Windows;

using Game.Model;
using Game.ViewModel;
using Game.View;

namespace Game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const int INITIAL_SIZE = 5;
        private readonly (int, int) INITIAL_WINDOW_SIZE = (400, 500);

        private GameModel _model = null!;
        private GameViewModel _viewModel = null!;
        private MainWindow _view = null!;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            _model = new GameModel(INITIAL_SIZE);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _view = new MainWindow();
            _view.Deactivated += View_Deactivated;
            _view.DataContext = _viewModel;

            SetWindowSize(INITIAL_WINDOW_SIZE);
            _view.Show();
        }

        private void View_Deactivated(object? sender, EventArgs e)
        {
            if (!_viewModel.Paused) _viewModel.TogglePause();
        }

        private void ViewModel_NewGame(object? sender, NewGameEventArgs e)
        {
            _model = new GameModel(e.Size);
            _viewModel.NewGame -= ViewModel_NewGame;
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            
            // Dummy Resize
            if (e.Size < 6) { SetWindowSize((400, 500)); }
            else if (e.Size < 8) { SetWindowSize((450, 550)); }
            else { SetWindowSize((500, 600)); }

            _view.DataContext = _viewModel;
        }

        private void SetWindowSize((int, int) size)
        {
            _view.Width = size.Item1;
            _view.Height = size.Item2;
        }
    }

}
