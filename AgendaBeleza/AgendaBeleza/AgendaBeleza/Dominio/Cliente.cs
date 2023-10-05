namespace AgendaBeleza.Dominio
{
    /// <summary>
    /// POCO: Plain old C# object (modelo anêmico) 
    /// </summary>
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string? Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }

        public bool Bloqueado { get; set; }
        
    }
}
