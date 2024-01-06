using Game.Model;
using Game.ViewModel;
using Game.Persistence;

namespace Game
{
    public partial class AppShell : Shell
    {
        private const int INITIAL_SIZE = 5;

        private GameModel _model;
        private GameViewModel _viewModel;
        private GameDataAccess _gameDataAccess;

        public event EventHandler<NewGameEventArgs> NewGame;

        public AppShell()
        {
            InitializeComponent();

            _model = new GameModel(INITIAL_SIZE);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.LoadGame += ViewModel_LoadGame;
            _gameDataAccess = new GameDataAccess(FileSystem.AppDataDirectory);
            BindingContext = _viewModel;
        }

        private void ViewModel_NewGame(object sender, NewGameEventArgs e)
        {
            _viewModel.NewGame -= ViewModel_NewGame;
            _viewModel.SaveGame -= ViewModel_SaveGame;
            _viewModel.LoadGame -= ViewModel_LoadGame;
            _model = new GameModel(e.Size);
            _viewModel = new GameViewModel(_model);
            _viewModel.NewGame += ViewModel_NewGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.LoadGame += ViewModel_LoadGame;
            BindingContext = _viewModel;
            NewGame.Invoke(this, e);
        }

        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Save Game", "Choose a name for your savefile!");
            if (!string.IsNullOrEmpty(result))
            {
                result += ".save";
                try
                {
                    _gameDataAccess.SaveGame(_model.ToData(), result);

                }
                catch(IOException ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async void ViewModel_LoadGame(object sender, EventArgs e)
        {
            string[] saves = _gameDataAccess.GetFiles().ToArray();

            string file = await DisplayActionSheet("Choose a savefile!", "Cancel", null, saves);
            if (file != "Cancel")
            {
                try
                {
                    LoadGame(file);
                }
                catch (IOException ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        public void Window_Activated()
        {
            if (File.Exists(Path.Join(FileSystem.AppDataDirectory, "SuspendedGame")))
            {
                try
                {
                    LoadGame("SuspendedGame");
                }
                catch { }
            }
        }

        public void Window_Deactivated()
        {
            try
            {
                _gameDataAccess.SaveGame(_model.ToData(), "SuspendedGame");
            }
            catch { }
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
            BindingContext = _viewModel;
            NewGame.Invoke(this, new NewGameEventArgs(_model.Size));
        }
    }
}
