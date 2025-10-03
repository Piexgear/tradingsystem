using System.Formats.Tar;

namespace App;

//klass User
public class User
{
    //skapar variabler som User ska ha
    public string Username;
    string _password;

    public List<Item> items;

    // konstruktor med ett försök till att ge varje användare en egen lista. 
    public User(string username, string password)
    {
        Username = username;
        _password = password;
        items = new List<Item>();

    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }

    public void Additem(string items, string description, User owner)
    {
        Item item = new Item(items, description, owner);
        this.items.Add(item);
    }

    //Spara användare till fil
    public string ToFileString()
    {
        return $"{Username};{_password}";
    }

    //läser från fil till string
    public static User FromFileString(string line)
    {
        // Delar upp textraden vid semikolon (resulterar i en array)
        string[] parts = line.Split(';');
        // Skapar och returnerar ett nytt User-objekt med email och lösenord
        return new User(parts[0], parts[1]);
    }
}

//skapar en klass för traderqusts 
public class Traderequest
{
    public User Requester;
    public Item RequestedItem;
    public Item OfferdItem;
    public User Owner;

    public Tradestatus status;


    public Traderequest(Item requesteditem, Item offerditem, User requester, User owner)
    {
        RequestedItem = requesteditem;
        OfferdItem = offerditem;
        Requester = requester;
        Owner = owner;
        status = Tradestatus.Pending;

    }

    public string ShowRequest()
    {
        return Requester + " wants to trade your: " + RequestedItem + "\nfor: " + OfferdItem;
    }
}

    