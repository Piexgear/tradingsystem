namespace App;

//klass User
class User
{
    //skapar variabler som User ska ha
    public string Username;
    string _password;

    // konstruktor
    public User(string username, string password)
    {
        Username = username;
        _password = password;

    }

    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }
}

    