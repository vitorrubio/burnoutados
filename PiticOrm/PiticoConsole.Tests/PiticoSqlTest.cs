using PiticoOrm;

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


    [TestMethod]
    public void AddTest()
    {
        // arrange
        var sobrenome = Guid.NewGuid();
        User usuario = new User
        {
            birthday = DateTime.Today,
            salary = 20000,
            username = $"José {sobrenome}"
        };

        // act
        usuario = banco.Add(usuario);

        //assert
        Assert.IsNotNull(usuario);
        Assert.AreNotEqual(0, usuario.id);
        Assert.AreEqual($"José {sobrenome}", usuario.username);

        var usrBanco = banco.Query<User>($"select * from Users where id = {usuario.id}").FirstOrDefault();

        Assert.IsNotNull(usrBanco);
        Assert.AreEqual(usuario.id, usrBanco.id);
        Assert.AreEqual($"José {sobrenome}", usrBanco.username);

    }





    [TestMethod]
    public void UpdateTest()
    {
        // arrange
        var id = 1;
        var usuario =  banco.Query<User>($"select * from Users where id = {id}").FirstOrDefault();
        var novonome = $"Alex {Guid.NewGuid()}";

        // act
        usuario.username = novonome;
        usuario = banco.Update(usuario);

        //assert
        Assert.IsNotNull(usuario);
        Assert.AreEqual(id, usuario.id);
        Assert.AreEqual(novonome, usuario.username);

    }


    [TestMethod]
    public void DeleteTest()
    {
        // arrange
        var id = 1;
        var usuario = banco.Query<User>($"select * from Users where id = {id}").FirstOrDefault();


        // act
        banco.Delete(usuario);

        //assert
        usuario = banco.Query<User>($"select * from Users where id = {id}").FirstOrDefault();
        Assert.IsNull(usuario);


    }

}