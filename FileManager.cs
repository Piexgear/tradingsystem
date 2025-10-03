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

    public static List<User> LoadUsers(string path)
    {
        List<User> users = new();
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        User user = User.FromFileString(line);
                        users.Add(user);
                    }
                }
            }
        }
        return users;
    }

    public static void LoadItems(string path, List<User> users)
    {
        if (!File.Exists(path)) return;

        string[] lines = File.ReadAllLines(path);
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length != 3)
            {
                continue;
            }

            string itemName = parts[0];
            string description = parts[1];
            string ownerUsername = parts[2];

            User owner = null;
            foreach (User user in users)
            {
                if (user.Username == ownerUsername)
                {
                    owner = user;
                    break;
                }
            }
            if (owner != null)
            {
                owner.items.Add(new Item(itemName, description, owner));
            }
        }
    }

    public static void ShowItem(string pathItem, User activeUser)
    {
        Console.Clear();
        Console.WriteLine("Here are all listed items:\n");

        if (!File.Exists(pathItem))
        {
            Console.WriteLine("No items found: ");
        }
        else
        {
            string[] lines = File.ReadAllLines(pathItem);
            bool anyShown = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 3)
                {
                    string itemName = parts[0];
                    string description = parts[1];
                    string ownerUsername = parts[2];

                    if (ownerUsername != activeUser.Username)
                    {
                        Console.WriteLine($"Item: {itemName}");
                        Console.WriteLine($"Description: {description}");
                        Console.WriteLine($"Listed by: {ownerUsername}");
                        Console.WriteLine("------------------------------");
                        anyShown = true;
                    }
                }
            }
            if (!anyShown)
            {
                Console.WriteLine("No items from other users found.");
            }
        }
        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }

    public static void ShowItemFortrade(string pathItem, User activeUser)
    {
        Console.Clear();
        Console.WriteLine("Here are all listed item:\n");

        if (!File.Exists(pathItem))
        {
            Console.WriteLine("No items found. (File does not exist)");
        }
        else
        {
            string[] lines = File.ReadAllLines(pathItem);
            bool anyShown = false;

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');
                if (parts.Length == 3)
                {
                    string itemName = parts[0];
                    string description = parts[1];
                    string ownerUsername = parts[2];

                    if (ownerUsername != activeUser.Username)
                    {
                        Console.WriteLine($"Item: {itemName}");
                        Console.WriteLine($"Description: {description}");
                        Console.WriteLine($"Listed by: {ownerUsername}");
                        Console.WriteLine("------------------------------");
                        anyShown = true;
                    }
                }
            }
            if (!anyShown)
            {
                Console.WriteLine("No items from other users found.");
            }
        }
    }

    public static void ShowUserItems(string pathItem, User user)
    {
        Console.Clear();
        Console.WriteLine($"Items listed by {user.Username}:\n");

        if (!File.Exists(pathItem))
        {
            Console.WriteLine("No items found (file does not exist).");
            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
            return;
        }

        string[] lines = File.ReadAllLines(pathItem);
        bool found = false;

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');

            if (parts.Length == 3)
            {
                string itemName = parts[0];
                string description = parts[1];
                string ownerUsername = parts[2];

                if (ownerUsername == user.Username)
                {
                    Console.WriteLine($"Item: {itemName}");
                    Console.WriteLine($"Description: {description}");
                    Console.WriteLine("------------------------------");
                    found = true;
                }
            }
        }
        if (!found)
        {
            Console.WriteLine("You have not listed any items.");
        }

        Console.WriteLine("\nPress enter to continue...");
        Console.ReadLine();
    }

    public static bool UserHasItems(string pathItem, User user)
    {
        if (!File.Exists(pathItem)) return false;

        string[] lines = File.ReadAllLines(pathItem);
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                string ownerUsername = parts[2];
                if (ownerUsername == user.Username)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool CreateTradeRequest(string pathItem, User active_user, List<User> users, List<Traderequest> traderequests)
    {
        string[] lines = File.ReadAllLines(pathItem);

        List<Item> otherUserItems = new List<Item>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                string itemName = parts[0];
                string description = parts[1];
                string ownerUsername = parts[2];

                if (ownerUsername != active_user.Username)
                {
                    User owner = null;
                    foreach (User user in users)
                    {
                        if (user.Username == ownerUsername)
                        {
                            owner = user;
                            break;
                        }
                    }
                    if (owner != null)
                    {
                        otherUserItems.Add(new Item(itemName, description, owner));
                    }
                }
            }
        }

        Console.Clear();
        Console.WriteLine("Here are items from other users:\n");
        foreach (Item item in otherUserItems)
        {
            Console.WriteLine($"Item: {item.Items}");
            Console.WriteLine($"Description: {item.Descrition}");
            Console.WriteLine($"Listed by: {item.Owner.Username}");
            Console.WriteLine("------------------------------");
        }

        Console.WriteLine("Enter the name of the item you want to trade:");
        string wishedItemName = Console.ReadLine();

        Item requestedItem = null;
        foreach (Item item in otherUserItems)
        {
            if (item.Items == wishedItemName)
            {
                requestedItem = item;
                break;
            }
        }

        if (requestedItem == null)
        {
            Console.WriteLine("Item not found.");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            return false;
        }

        // Kontrollera att active_user har items i filen
        List<Item> activeUserItems = new List<Item>();
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                string itemName = parts[0];
                string description = parts[1];
                string ownerUsername = parts[2];

                if (ownerUsername == active_user.Username)
                {
                    activeUserItems.Add(new Item(itemName, description, active_user));
                }
            }
        }

        if (activeUserItems.Count == 0)
        {
            Console.WriteLine("You don't have any items listed to offer.");
            Console.WriteLine("You must add an item to trade.");
            FileManager.AddItem(pathItem, active_user);
            return false;
        }

        Console.Clear();
        Console.WriteLine("Your listed items:\n");
        foreach (Item item in activeUserItems)
        {
            Console.WriteLine($"Item: {item.Items}");
            Console.WriteLine($"Description: {item.Descrition}");
            Console.WriteLine("------------------------------");
        }

        Console.WriteLine("Enter the name of the item you want to offer:");
        string offerItemName = Console.ReadLine();

        Item offeredItem = null;
        foreach (Item item in activeUserItems)
        {
            if (item.Items == offerItemName)
            {
                offeredItem = item;
                break;
            }
        }

        if (offeredItem == null)
        {
            Console.WriteLine("Offered item not found.");
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
            return false;
        }

        Traderequest request = new Traderequest(requestedItem, offeredItem, active_user, requestedItem.Owner);
        traderequests.Add(request);

        Console.Clear();
        Console.WriteLine("Trade request sent!");
        Console.WriteLine();
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();

        return true;
    }
}

