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

    public static void AddUser(string pathUser)
    {
        Console.Clear();
        // Ber användaren skriva in ett användarnamn
        Console.Write("Type your Username: ");
        // Läser användarnamnet från konsolen
        string newUsername = Console.ReadLine();

        // Ber användaren skriva in lösenord
        Console.Clear();
        Console.Write("Type your Password: ");
        // Läser lösenord från konsolen
        string newPassword = Console.ReadLine();

        // Skapar ett nytt User-objekt med den inmatade datan
        User newUser = new User(newUsername, newPassword);

        // Öppnar filen för att lägga till text i slutet (append mode)
        using (StreamWriter writer = new StreamWriter(pathUser, append: true))
        {
            // Skriver användarens data som en rad i filen
            writer.WriteLine(newUser.ToFileString());
            // Filen sparas och stängs automatiskt här
        }

        // Bekräftar att användaren sparades
        Console.Clear();
        Console.WriteLine($"Användare '{newUsername}' har lagts till!");
        Console.ReadLine();
    }

    public static void AddItem(string pathItem, User active_user)
{
    Console.Clear();
    Console.WriteLine("What item would you like to list?");
    string itemName = Console.ReadLine();

    Console.Clear();
    Console.WriteLine("Description:");
    string description = Console.ReadLine();

    // Skapa item
    Item newItem = new Item(itemName, description, active_user);

    // Lägg till i items lista
    active_user.items.Add(newItem);

    using (StreamWriter writer = new StreamWriter(pathItem, append: true))
        {
            // Skriver användarens data som en rad i filen
            writer.WriteLine(newItem.ToFileString());
            // Filen sparas och stängs automatiskt här
        }

    Console.Clear();
    Console.WriteLine("Item added to your list!");
    Console.WriteLine();
    Console.WriteLine("Press enter to continue...");
    Console.ReadLine();
}


    public static bool UserExists(string filePath, string username)
    {
        if (!File.Exists(filePath))
            return false;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    User user = User.FromFileString(line);
                    if (user.Username == username)
                    {
                        // Användaren finns redan
                        return true;
                    }
                }
            }
        }
        // Användaren hittades inte
        return false;
    }

}
