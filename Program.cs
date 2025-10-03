using System.ComponentModel.Design;
using System.Security.Cryptography;
using App;

/*
A user needs to be able to register an account X
A user needs to be able to log out. X
A user needs to be able to log in. X
A user needs to be able to upload information about the item they wish to trade. X
A user needs to be able to browse a list of other users items. X
A user needs to be able to request a trade for other users items. X
A user needs to be able to browse trade requests. X
A user needs to be able to accept a trade request. X
A user needs to be able to deny a trade request. X
A user needs to be able to browse completed requests.
The program needs to save relevant data to the computers file system whenever a state change is made.
The program needs to be able to start and then automatically load all relevant data so it can function as if it was never closed.
*/









//Path till filer
string pathUser = Path.Combine("Data", "User.txt");
string pathItem = Path.Combine("Data", "Item.txt");

// Kalla metod för att se ifall directoryn finns. 
FileManager.EnsureDirectoryExists(pathUser);
FileManager.EnsureDirectoryExists(pathItem);


//skapar listor för användare och föremål
List<User> users = new List<User>();
List<Traderequest> traderequests = new List<Traderequest>();

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
            FileManager.AddUser(pathUser);
            // skapar en ny användare 
            /*Console.Clear();
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
            Console.ReadLine();*/
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
        Console.WriteLine("5. Complete trades");
        Console.WriteLine("6. Log out");
        Console.WriteLine("7. Quit");
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
                Console.WriteLine("Here is all listed items:");

                //skapar en variabel för att en tom lista ska kunna fungera
                int count = 0;
                // loppar igenom alla användare 
                foreach (User user in users)
                {
                    //användaren är inte active user
                    if (user != active_user)
                    {
                        // loopar igenom användarens items.
                        foreach (Item item in user.items)
                        {
                            Console.WriteLine(item.ShowItem());
                            Console.WriteLine("---------------------");
                            count++;
                        }
                    }
                }
                //ifall den inte hittar något listat item så visar jag det
                if (count == 0)
                {
                    Console.WriteLine("No items found from other users.");
                }

                Console.WriteLine();
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();
                break;


            //case 3 för att starta en trade 
            case "3":
                Console.Clear();

                // Visa alla items som tillhör andra användare än den inloggade
                foreach (User user in users)
                {
                    if (user != active_user)
                    {
                        foreach (Item item in user.items)
                        {
                            Console.WriteLine(item.ShowItem());
                            Console.WriteLine("---------------------");
                        }
                    }
                }

                Console.WriteLine("What item would you like to trade? ");
                string Wish_trade = Console.ReadLine();

                // Leta upp det önskade itemet bland andra användares items
                Item? requestedItem = null;
                User? itemOwner = null;
                //loppar igenom andra användares items 
                foreach (User user in users)
                {
                    if (user != active_user)
                    {
                        foreach (Item item in user.items)
                        {
                            if (item.Items == Wish_trade)
                            {
                                // när det önskade item är hittat så bryts loopen
                                requestedItem = item;
                                itemOwner = user;
                                break;
                            }
                        }
                    }
                    if (requestedItem != null) break;
                }

                //ifall önskade föremålet inte hittades visas felmeddelande. 
                if (requestedItem == null)
                {
                    Console.Clear();
                    Console.WriteLine("No item found");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
                }

                // Om användaren inte har egna items, be hen lägga till ett item att erbjuda
                if (active_user.items.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any listed items to trade with.");
                    Console.WriteLine("You must add an item to trade");
                    Console.WriteLine();

                    Console.WriteLine("What is your offering item: ");
                    string offer = Console.ReadLine();
                    Console.Clear();

                    Console.WriteLine("Description of your item:");
                    string offer_description = Console.ReadLine();
                    Console.Clear();

                    active_user.Additem(offer, offer_description, active_user);
                }

                // Visa användarens items för att välja vad de vill erbjuda
                Console.Clear();
                Console.WriteLine("What item would you like to offer?");
                foreach (Item item in active_user.items)
                {
                    Console.WriteLine(item.ShowItem());
                    Console.WriteLine("---------------------");
                }
                string offerItem = Console.ReadLine();

                // Leta upp det erbjudna itemet i användarens lista
                Item? offeredItem = null;
                foreach (Item item in active_user.items)
                {
                    if (item.Items == offerItem)
                    {
                        offeredItem = item;
                        break;
                    }
                }

                if (offeredItem == null)
                {
                    Console.Clear();
                    Console.WriteLine("No item found");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue...");
                    Console.ReadLine();
                    break;
                }

                // Skapa trade request och lägg till i listan
                Traderequest request = new Traderequest(requestedItem, offeredItem, active_user, itemOwner);
                traderequests.Add(request);

                Console.Clear();
                Console.WriteLine("Trade request sent!");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue...");
                Console.ReadLine();

                break;


            //case 4 för att se alla aktiva eller väntande trades.
            case "4":
                Console.Clear();
                List<Traderequest> userRequest = new List<Traderequest>();
                foreach (Traderequest requests in traderequests)
                {
                    if (requests.Requester == active_user || requests.Owner == active_user)
                    {
                        userRequest.Add(requests);
                    }
                }

                if (userRequest.Count == 0)
                {
                    Console.WriteLine("No trade requests found.");
                }
                else
                {
                    Console.WriteLine("Active trade requests:\n");
                    foreach (Traderequest request1 in traderequests)
                    {
                        if (request1.status == Tradestatus.Pending)
                        {
                            Console.WriteLine($"Requested Item: {request1.RequestedItem.Items}");
                            Console.WriteLine($"Offered Item: {request1.OfferdItem.Items}");
                            Console.WriteLine($"From: {request1.Requester.Username}");
                            Console.WriteLine($"To: {request1.Owner.Username}");
                            Console.WriteLine($"Status: {request1.status}");
                            Console.WriteLine("------------------------------");
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine("\nAccept or Decline offer:");
                Console.WriteLine("Press enter to continue...");
                string AcceptOrDeclined = Console.ReadLine();

                if (AcceptOrDeclined == "accept" || AcceptOrDeclined == "decline")
                {
                    Console.WriteLine("Which requested item would you like to respond to?");
                    string itemName = Console.ReadLine();

                    Traderequest? selectedRequest = null;

                    // Hitta rätt request med foreach
                    foreach (Traderequest req in userRequest)
                    {
                        if (req.RequestedItem.Items == itemName && req.Owner == active_user && req.status == Tradestatus.Pending)
                        {
                            selectedRequest = req;
                            break;
                        }
                    }

                    if (selectedRequest != null)
                    {

                        if (AcceptOrDeclined == "accept")
                        {
                            selectedRequest.status = Tradestatus.Accepted;
                            Console.WriteLine("You accepted the trade request.\n");
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();

                        }


                        else if (AcceptOrDeclined == "decline")
                        {
                            selectedRequest.status = Tradestatus.Denied;
                            Console.WriteLine("You declined the trade request.\n");
                            Console.WriteLine("Press enter to continue...");
                            Console.ReadLine();
                        }
                    }

                    else
                    {
                        Console.WriteLine("No matching trade request found.\n");
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadLine();
                    }

                }

                else
                {
                    Console.WriteLine("Invalid input.");
                }
                    break;                

            //case 5 för att visa alla färdiga trades alltså alla som är complete (Accepted eller declined)
            case "5":
                foreach (Traderequest trade in traderequests)
                {
                    if (trade.status == Tradestatus.Accepted || trade.status == Tradestatus.Denied)
                    {
                        Console.WriteLine("-----Complete trades-----\n");
                        Console.WriteLine($"Requested Item: {trade.RequestedItem.Items}");
                        Console.WriteLine($"Offered Item: {trade.OfferdItem.Items}");
                        Console.WriteLine($"From: {trade.Requester.Username}");
                        Console.WriteLine($"To: {trade.Owner.Username}");
                        Console.WriteLine($"Status: {trade.status}");
                        Console.WriteLine("------------------------------");
                    }
                }
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
                break;


            //case 6 för att kunna logga ut.
            case "6":
                active_user = null;
                break;


            // case 7 är för att avsluta programmet
            case "7":
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