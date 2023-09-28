public class User
{
    public User()
    {
        username = "";
        createdAt = DateTime.Now;
        updatedAt = DateTime.Now;
    }

    public string username {get; set;}
    public DateTime birthday {get; set;}
    public int id {get; set;}
    public DateTime createdAt {get; set;}
    public DateTime updatedAt {get; set;}
    public double salary {get; set;}
    
}