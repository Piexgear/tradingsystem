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
        Console.WriteLine($"Användare '{newUsername}' har lagts till!");
        Console.ReadLine();
    }
}
