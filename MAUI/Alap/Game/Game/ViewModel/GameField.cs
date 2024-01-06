namespace Game.ViewModel
{
    public class GameField: ViewModelBase
    {
        public int X { get; set; }
        public int Y { get; set; }
        public (int, int) XY { get { return (X, Y); } }

        public DelegateCommand ClickCommand { get; set; }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        private Color _color = new Color(255, 255, 255);
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
    }
}
