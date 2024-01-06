using Game.Model;
using System.Collections.ObjectModel;

namespace Game.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        public const int FIEL_SIZE = 50;

        private GameModel _model;

        public int Size { get { return _model.Size; } }
        public int TableSize { get { return FIEL_SIZE * Size; } }
        public ObservableCollection<GameField> Fields { get; set; }

        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        public DelegateCommand LoadGameCommand { get; private set; }

        public event EventHandler<NewGameEventArgs>? NewGame;
        public event EventHandler? SaveGame;
        public event EventHandler? LoadGame;

        public GameViewModel(GameModel model)
        {
            _model = model;
            _model.StateChanged += Model_StateChanged;
            _model.GameOver += Model_GameOver;

            NewGameCommand = new DelegateCommand(param =>
            {
                // Do not forget to set CommandParameters
                OnNewGame(int.Parse((param!.ToString())!));
            });
            SaveGameCommand = new DelegateCommand(_ => { OnSaveGame(); });
            LoadGameCommand = new DelegateCommand(_ => { OnLoadGame(); });

            Fields = new ObservableCollection<GameField>();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Fields.Add(new GameField
                    {
                        X = i,
                        Y = j,
                        // Text = _model.GetField(i, j).ToString(),
                        // IsEnabled = true,
                        // Color = 0, -- background color
                        ClickCommand = new DelegateCommand(param =>
                        {
                            (int x, int y) = ((int, int))param!;
                            FieldClicked(x, y);
                        })
                    });
                }
            }
        }

        private void FieldClicked(int x, int y)
        {
            // Button clicked
        }

        private void OnNewGame(int size)
        {
            NewGame?.Invoke(this, new NewGameEventArgs(size));
        }

        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
        }

        private void Model_StateChanged(object? sender, StateChangedEventArgs e)
        {
            // Update the fields based on stateChange
        }

        private void Model_GameOver(object? sender, GameOverEventArgs e)
        {
            // Game Over
        }
    }
}
