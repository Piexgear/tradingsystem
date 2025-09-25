using System.Runtime.CompilerServices;

namespace App;

class Item
{
    public string Items;
    public string Descrition;

    public User Owner;


    public Item(string items, string descrition, User owner)
    {
        Items = items;
        Descrition = descrition;
        Owner = owner;
    }
    public string ShowItem()
    {
        return "Item: \n" + Items + "\n\nDescription: \n" + Descrition + "\n\nOwner: \n" + Owner.Username;
    }
}
