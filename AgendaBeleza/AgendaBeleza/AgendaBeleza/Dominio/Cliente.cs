using System.ComponentModel.DataAnnotations;

namespace AgendaBeleza.Dominio
{
    /// <summary>
    /// POCO: Plain old C# object (modelo anêmico) 
    /// </summary>
    public class Cliente
    {

        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(11)]
        public string CPF { get; set; }
        [MaxLength(25)]
        public string? Apelido { get; set; }
        public DateTime? DataNascimento { get; set; }

        public bool Bloqueado { get; set; }


    }
}
