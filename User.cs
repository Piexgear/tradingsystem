using System.Formats.Tar;

namespace App;

//klass User
class User
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
}

//skapar en klass för traderqusts 
class Traderequest
{
    public User Requester;
    public Item RequestedItem;
    public Item OfferdItem;
    public User Owner;


    public Traderequest(User requester, Item requestedItem, Item offerditem, User owner)
    {
        Requester = requester;
        RequestedItem = requestedItem;
        OfferdItem = offerditem;
        Owner = owner;
    }

    public string ShowRequest()
    {
        return Requester + " wants to trade your: " + RequestedItem + "\nfor: " + OfferdItem;
    }
}

    