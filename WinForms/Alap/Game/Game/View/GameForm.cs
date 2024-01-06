using Game.Model;

namespace Game.View
{
    public partial class GameForm : Form
    {
        private const int INITIAL_SIZE = 5;

        private const int ABOVE_MARGIN = 100;
        private const int BELOW_MARGIN = 100;
        private const int HORIZONTAL_MARGIN = 50;
        private const int FIELD_SIZE = 80;

        private GameModel _model = null!;
        private Button[,] _buttonGrid = null!;

        public int GameSize { get {  return _model.Size; } }

        public GameForm()
        {
            InitializeComponent();

            _model = new GameModel(INITIAL_SIZE);
            _model.StateChanged += Model_StateChanged;
            _model.GameOver += Model_GameOver;

            InitializeGrid();
            ResizeToFit();
        }

        private void StartNewGame(int size)
        {
            _model.StateChanged -= Model_StateChanged;
            _model.GameOver -= Model_GameOver;
            _model = new GameModel(size);
            _model.StateChanged += Model_StateChanged;
            _model.GameOver += Model_GameOver;

            RemoveGrid();
            InitializeGrid();
            ResizeToFit();
        }

        private void InitializeGrid()
        {
            _buttonGrid = new Button[GameSize, GameSize];

            for (int i = 0; i < GameSize; i++)
            {
                for (int j = 0; j < GameSize; j++)
                {
                    _buttonGrid[i, j] = new Button
                    {
                        //Text = (i * GameSize + j).ToString(),
                        Location = new Point(HORIZONTAL_MARGIN + j * FIELD_SIZE, ABOVE_MARGIN + i * FIELD_SIZE),
                        Size = new Size(FIELD_SIZE, FIELD_SIZE),
                        Font = new Font(FontFamily.GenericSansSerif, 18, FontStyle.Bold),
                        Enabled = true,
                        TabIndex = 100 + i * GameSize + j,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.White,
                    };
                    _buttonGrid[i, j].MouseClick += ButtonGrid_MouseClick;
                    Controls.Add(_buttonGrid[i, j]);
                }
            }
        }

        private void RemoveGrid()
        {
            for (int i = 0; i < _buttonGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _buttonGrid.GetLength(1); j++)
                {
                    Controls.Remove(_buttonGrid[i, j]);
                }
            }
        }

        private void ButtonGrid_MouseClick(object? sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {

                int x = (button.TabIndex - 100) / GameSize;
                int y = (button.TabIndex - 100) % GameSize;

                // Do things
            }
        }

        private void Model_StateChanged(object? sender, StateChangedEventArgs e)
        {
            // State Changed
        }

        private void Model_GameOver(object? sender, GameOverEventArgs e)
        {
            // Game Over
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(5); 
        private void x7ToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(7);
        private void x9ToolStripMenuItem_Click(object sender, EventArgs e) => StartNewGame(9);

        private void ResizeToFit()
        {
            Width = 2 * HORIZONTAL_MARGIN + FIELD_SIZE * GameSize;
            Height = ABOVE_MARGIN + BELOW_MARGIN + FIELD_SIZE * GameSize;
        }
    }
}
