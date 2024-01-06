using System.Windows;
using System.IO;

using Game.Model;
using Game.ViewModel;
using Game.View;
using Game.Persistence;
using Microsoft.Win32;

namespace Game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly string suspendedGamePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SuspendedGame");

        private const int INITIAL_SIZE = 5;
        private readonly (int, int) INITIAL_WINDOW_SIZE = (400, 500);

        private GameModel _model = null!;
        private GameViewModel _viewModel = null!;
        private MainWindow _view = null!;
        private GameDataAccess _gameDataAccess = null!;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            _model = new GameModel(INITIAL_SIZE);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.LoadGame += ViewModel_LoadGame;
            _view = new MainWindow();
            _view.Activated += View_Activated;
            _view.Deactivated += View_Deactivated;
            _view.DataContext = _viewModel;
            _gameDataAccess = new GameDataAccess();

            SetWindowSize(INITIAL_WINDOW_SIZE);
            _view.Show();
        }

        private void View_Activated(object? sender, EventArgs e)
        {
            if (File.Exists(suspendedGamePath))
            {
                try
                {
                    LoadGame(suspendedGamePath);
                } catch { }
            }
        }

        private void View_Deactivated(object? sender, EventArgs e)
        {
            try
            {
                _gameDataAccess.SaveGame(_model.ToData(), suspendedGamePath);
            } catch { }
        }

        private void ViewModel_NewGame(object? sender, NewGameEventArgs e)
        {
            _model = new GameModel(e.Size);
            _viewModel.NewGame -= ViewModel_NewGame;
            _viewModel.SaveGame -= ViewModel_SaveGame;
            _viewModel.LoadGame -= ViewModel_LoadGame;
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.LoadGame += ViewModel_LoadGame;

            // Dummy Resize
            if (e.Size < 6) { SetWindowSize((400, 500)); }
            else if (e.Size < 8) { SetWindowSize((450, 550)); }
            else { SetWindowSize((500, 600)); }

            _view.DataContext = _viewModel;
        }

        private void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Title = "Save Game",
                    Filter = "Savefile|*.save"
                };
                if (dialog.ShowDialog() == true)
                {
                    _gameDataAccess.SaveGame(_model.ToData(), dialog.FileName);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Title = "Load Game",
                    Filter = "Savefile|*.save"
                };
                if (dialog.ShowDialog() == true)
                {
                    LoadGame(dialog.FileName);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetWindowSize((int, int) size)
        {
            _view.Width = size.Item1;
            _view.Height = size.Item2;
        }

        private void LoadGame(string file)
        {
            GameData data = _gameDataAccess.LoadGame(file);
            _model = new GameModel(data);

            _viewModel.NewGame -= ViewModel_NewGame;
            _viewModel.SaveGame -= ViewModel_SaveGame;
            _viewModel.LoadGame -= ViewModel_LoadGame;
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.LoadGame += ViewModel_LoadGame;

            // Dummy Resize
            if (_viewModel.Size < 6) { SetWindowSize((400, 500)); }
            else if (_viewModel.Size < 8) { SetWindowSize((450, 550)); }
            else { SetWindowSize((500, 600)); }

            _view.DataContext = _viewModel;
        }
    }

}
