namespace PiticoConsole.Tests;

[TestClass]
public class PiticoSqlTest
{

    private PiticoSql banco;

    public PiticoSqlTest()
    {
        banco = new PiticoSql("Server=DESKTOP-RE767MN;Database=burnoutados;Trusted_Connection=True;MultipleActiveResultSets=true");
    }

    [TestMethod]
    public void QueryTest()
    {
        var usrs = banco.Query<User>("select id, username, birthday from Users");
        Assert.IsNotNull(usrs);
        Assert.AreNotEqual(0, usrs.Count);
    }
}