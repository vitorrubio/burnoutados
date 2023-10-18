using System.ComponentModel.DataAnnotations;

namespace AgendaBeleza.Dominio
{
    public class Servico
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [Range(0, 1000, ErrorMessage = "Valor de {0} precisa ser entre {1} e {2}.")]
        public decimal Preco { get; set; }
        [Range(30, 480, ErrorMessage = "Valor de {0} precisa ser entre {1} e {2}.")]
        public int DuracaoMinutos { get; set; }
        public bool Disponivel { get; set; }

    }
}
