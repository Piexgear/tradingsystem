using System.ComponentModel.Design;
using App;

/*A user needs to be able to register an account 
A user needs to be able to log out. X
A user needs to be able to log in. X
A user needs to be able to upload information about the item they wish to trade.
A user needs to be able to browse a list of other users items.
A user needs to be able to request a trade for other users items.
A user needs to be able to browse trade requests.
A user needs to be able to accept a trade request.
A user needs to be able to deny a trade request.
A user needs to be able to browse completed requests.*/











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
    // försöka ändra på inloggningen eftersom att den hoppar tillbaka till menyn även när inlogg är rätt och måste trycka enter en gång innan den går vidare. 
    if (active_user == null)
    {
        // Meny som ger val.
        Console.WriteLine("----------TradePoint----------");
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create acoount");
        string menu_choise = Console.ReadLine();

        if (menu_choise == "1")
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
        else if (menu_choise == "2")
        {
            // skapar en ny användare 
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
                //Console.WriteLine(users.);

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
                active_user = null;
                break;
        }
    }       
}