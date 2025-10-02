namespace App;

public static class FileManager
{
    public static void EnsureDirectoryExists(string filePath)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public static void SaveUsersToFile(string filePath, List<User> users)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (User user in users)
            {
                writer.WriteLine($"{user.Username};{user.GetPassword()}");
            }
        }
    }

    public static void SaveItemsToFile(string filePath, List<User> users)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (User user in users)
            {
                foreach (Item item in user.items)
                {
                    writer.WriteLine($"{item.Items};{item.Descrition};{user.Username}");
                }
            }
        }
    }
}
