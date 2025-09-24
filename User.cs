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
}

    