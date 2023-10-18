Console.WriteLine("Hello, World!");

PiticoSql banco = new PiticoSql("Server=DESKTOP-RE767MN;Database=burnoutados;Trusted_Connection=True;MultipleActiveResultSets=true");


User usuario = new User
{
    birthday = DateTime.Today,
    salary = 10000,
    username = $"Maria {Guid.NewGuid()}"

};

usuario = banco.Add(usuario);



var usrs = banco.Query<User>("select id, username, birthday from Users");
// var usrs = banco.MapRows<User>("select id, username, birthday from Users", (item, record, rownum) =>
// {
//     item.id = Convert.ToInt32( record["id"]);
//     item.username = record["username"].ToString();
//     item.numeroLinha = rownum+1;
// }).ToList();


Console.WriteLine($"Foram encontrados {usrs.Count} registros");

foreach(User u in usrs)
{
    Console.WriteLine($"{u.id} - {u.username} - {u.birthday.ToString("dd/MM/yyyy")} - {u.salary}");
}

Console.WriteLine("Terminou");

