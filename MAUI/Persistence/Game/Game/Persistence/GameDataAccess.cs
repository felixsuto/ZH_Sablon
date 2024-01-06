namespace Game.Persistence
{
    public class GameDataAccess
    {
        private string _dir;

        public GameDataAccess(string dir = "")
        {
            _dir = dir;
        }

        public GameData LoadGame(string path)
        {
            if (_dir != "") { path = Path.Combine(_dir, path); }
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    string[] numbers = line.Split(' ');
                    int size = int.Parse(numbers[0]);
                    GameData data = new GameData(size);
                    // set other numbers

                    for (int i = 0; i < size; i++)
                    {
                        line = reader.ReadLine();
                        numbers = line.Split(" ");
                        for (int j = 0; j < size; j++)
                        {
                            data.SetField(i, j, int.Parse(numbers[j]));
                        }
                    }

                    return data;
                }
            }
            catch
            {
                throw new IOException("Error loading game.");
            }
        }

        public void SaveGame(GameData data, string path)
        {
            if (_dir != "") { path = Path.Combine(_dir, path); }

            try
            {
                using (StreamWriter writer  = new StreamWriter(path))
                {
                    writer.WriteLine(data.Size);
                    //writer.WriteLine(data.Size + " " + data.OtherEntry + ....)

                    for (int i = 0; i < data.Size; i++)
                    {
                        for (int j = 0; j < data.Size; j++)
                        {
                            writer.Write(data.GetField(i, j) + " ");
                        }
                        writer.WriteLine();
                    }
                }
            }
            catch
            {
                throw new IOException("Error saving game.");
            }
        }

        public IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(FileSystem.AppDataDirectory)
                .Select(Path.GetFileName)
                .Where(name => name?.EndsWith(".save") ?? false)
                .OfType<string>();
        }
    }
}
