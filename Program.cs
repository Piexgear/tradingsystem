using App;

//skapar listor för användare och föremål
List<User> users = new List<User>();
List<Item> items = new List<Item>();

//Lägger till föremål i listan 
items.Add(new Item("Cykel", "Fin cykel med 5 växlar"));
items.Add(new Item("Klocka", "Visar tiden"));

// Lägger till users i listan
users.Add(new User("david", "password"));
users.Add(new User("manuel", "hej"));
users.Add(new User("elias", "lasso"));

//skapar en variabel som visar ifall man är inloggad   
User? active_user = null;

bool running = true;

// skapar en while loop för att programmet ska köra tills jag vill avsluta.
while (running)
{

    Console.Clear();
    // Meny som ger val.
    Console.WriteLine("----------TradePoint----------");
    Console.WriteLine("1. Log in");
    Console.WriteLine("2. Create acoount");
    string menu_choise = Console.ReadLine();


    switch (menu_choise)
    {
        case "1":
            // en if satts för att se ifall användaren finns och ifall den inte finns så börjar den om.
            if (active_user == null)
            {
                Console.Clear();
                //Läser in username
                Console.Write("Username: ");
                string username = Console.ReadLine();

                Console.Clear();
                // läser in lösenord
                Console.Write("Password: ");
                string password = Console.ReadLine();

                //går igenom alla användare och lösenord.
                foreach (User user in users)
                {
                    if (user.TryLogin(username, password))
                    {
                        active_user = user;
                        break;
                    }
                }
            }

            // Skapar en ny meny när man är inloggad. 
            else
            {
                Console.WriteLine("----------TradePoint----------");
                Console.WriteLine("1. Uppload item");
                Console.WriteLine("2. Brows");
                Console.WriteLine("3. Start trade");
                Console.WriteLine("4. Requests");
                Console.WriteLine("5. Log out");
                string input = Console.ReadLine();

                switch (input)
                {
                    //case 1 för uppladdandet av items  
                    case "1":
                        Console.Clear();
                        Console.WriteLine("What item would you like to list");
                        string add = Console.ReadLine();

                        Console.Clear();
                        Console.WriteLine("Description");
                        string info = Console.ReadLine();

                        items.Add(new Item(add, info));
                        Console.Clear();
                        Console.WriteLine("Item added to list");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to continue...");

                        // försöker skriva ut vad som finns i personens lista. 
                        Console.WriteLine(users.)

                        break;


                    // case 2 för browsing av listade items
                    case "2":

                        break;


                    //case 3 för att starta en trade 
                    case "3":
                        break;


                    //case 4 för att se alla aktiva eller väntande trades.
                    case "4":
                        break;

                    
                    //case 5 för att kunna logga ut.
                    case "5":
                        break;
                }
            }

            break;
        // skapar en ny användare 
        case "2":
            Console.Clear();
            Console.Write("Type your username: ");
            string newUsername = Console.ReadLine();

            Console.Clear();
            Console.Write("Type your password: ");
            string new_password = Console.ReadLine();

            // lägger till användaren i listan 
            users.Add(new User(newUsername, new_password));
            Console.Clear();

            Console.WriteLine("Successfull registration for user: " + newUsername);

            break;
    }
}