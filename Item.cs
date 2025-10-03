using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace App;

 public class Item
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

    public string ToFileString()
    {
        return $"{Items};{Descrition};{Owner.Username}";
    }

    // Läser alla items. 
    public static Item FromFileString(string line, User owner)
    {
        string[] parts = line.Split(";");
        string itemNamen = parts[0];
        string description = parts[1];
        string ownerUser = parts[2];

        if (owner.Username != ownerUser)
        {
            return null;
        }
        return new Item(itemNamen, description, owner);
    }

}
public enum Tradestatus
{
    Pending,
    Accepted,
    Denied,
    Completed
}