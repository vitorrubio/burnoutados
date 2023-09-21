Console.WriteLine("Hello, World!");

PiticoSql banco = new PiticoSql("Server=DESKTOP-RE767MN;Database=burnoutados;Trusted_Connection=True;MultipleActiveResultSets=true");

//var usrs = banco.Query<User>("select id, username, birthday from Users");
var usrs = banco.MapRows<User>("select id, username, birthday from Users", (item, record, rownum) =>
{
    item.id = Convert.ToInt32( record["id"]);
    item.username = record["username"].ToString();
    item.numeroLinha = rownum+1;
}).ToList();


Console.WriteLine($"Foram encontrados {usrs.Count} registros");

foreach(User u in usrs)
{
    Console.WriteLine($"{u.id} - {u.username} - {u.birthday.ToString("dd/MM/yyyy")} - {u.salary} - {u.numeroLinha}");
}

Console.WriteLine("Terminou");
Console.ReadLine();