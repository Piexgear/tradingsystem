using System.ComponentModel.Design;
using App;

/*
A user needs to be able to register an account X
A user needs to be able to log out. X
A user needs to be able to log in. X
A user needs to be able to upload information about the item they wish to trade. X
A user needs to be able to browse a list of other users items. X
A user needs to be able to request a trade for other users items. 
A user needs to be able to browse trade requests.
A user needs to be able to accept a trade request.
A user needs to be able to deny a trade request.
A user needs to be able to browse completed requests.
*/











//skapar listor för användare och föremål
List<User> users = new List<User>();

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

    // meny som ger första valen
    Console.Clear();
    if (active_user == null)
    {
        // Menyn
        Console.WriteLine("----------TradePoint----------");
        Console.WriteLine("1. Log in");
        Console.WriteLine("2. Create acoount");
        Console.WriteLine("3. Quit");
        string menu_choise = Console.ReadLine();

        // inloggnings kommandot
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
        // skapa användare kommandot
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
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        else if (menu_choise == "3")
        {
            Console.WriteLine("Are you sure? Y/N");
            string yesno = Console.ReadLine();
            if (yesno == "y")
            {
                running = false;
            }
        }
        else
        {
            Console.WriteLine("You must enter a valid command. 1, 2 or 3");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
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
        Console.WriteLine("6. Quit");
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

                // lägger till item i active users personliga lista.
                User owner = active_user;
                active_user.Additem(add, info, owner);

                Console.Clear();
                Console.WriteLine("Item added to list");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue...");

                // försöker skriva ut vad som finns i personens lista. 
                //Console.WriteLine(users.);

                break;


            // case 2 för browsing av listade items
            case "2":
                Console.Clear();
                Console.WriteLine("Here is all listed items: ");
                //går igenom varje användare
                foreach (User user in users)
                {
                    // går igenom varje användares items 
                    foreach (Item item in user.items)
                    {
                        //skriver ut allas föremål som inte är den activa användarens föremål                        
                        if (active_user != user)
                            Console.WriteLine(item.ShowItem());
                        Console.WriteLine("---------------------");
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                break;


            //case 3 för att starta en trade 
            case "3":
                
                //går igenom varje användare
                foreach (User user in users)
                {
                    // går igenom varje användares items 
                    foreach (Item item in user.items)
                    {
                        //skriver ut allas föremål som inte är den activa användarens föremål
                        if (active_user != user)
                        {
                            Console.WriteLine(item.ShowItem());
                            Console.WriteLine("---------------------");
                        }
                    }
                }
                Console.WriteLine("What item would you like to trade? ");
                string Wish_trade = Console.ReadLine();

                // skapar en boolian för att se längre fram ifall det föremålet hittades
                bool found = false;

                foreach (User user in users)
                {
                    foreach (Item item in user.items)
                    {
                        if (active_user != user && Wish_trade == item.Items)
                        {
                            Console.WriteLine(item.Items);
                            Console.WriteLine("---------------------");
                            // ändrar found till true ifall sökningen matchar
                            found = true;
                            break;
                        }
                    }
                    if (found) break;
                }
                
                // låter användaren veta att inget item hittades 
                if (!found)
                {
                    Console.Clear();
                    Console.WriteLine("No item found");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                }

                // ifall användaren inte har ett listat item så skapar användaren ett bytes föremål 
                if (active_user.items == null)
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any listed items to trade with.");
                    Console.WriteLine("You must add an item to trade");
                    Console.WriteLine();

                    User Owner = active_user;

                    //Ber användaren lägga till ett item att trade med.
                    Console.WriteLine("What is your offering item: ");
                    string offer = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine("Description of your item:");
                    string offer_description = Console.ReadLine();
                    Console.Clear();
                    //Lägger till föremålet i användarens lista.
                    active_user.Additem(offer, offer_description, Owner);
                }
                
                break;


            //case 4 för att se alla aktiva eller väntande trades.
            case "4":
                break;


            //case 5 för att kunna logga ut.
            case "5":
                active_user = null;
                break;

            case "6":
                Console.WriteLine("Are you sure? Y/N");
                string yesno = Console.ReadLine();
                if (yesno == "y")
                {
                    running = false;
                }
                break;
        }
    }       
}