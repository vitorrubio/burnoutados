namespace MvcTradicional.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }


        public static readonly List<Produto> BdProdutos = new List<Produto> {
            new Produto{Id = 1, Nome= "Biscoito", Preco = 5},
            new Produto{Id = 2, Nome= "Suco", Preco = 7.45m},
            new Produto{Id = 3, Nome= "Chocolate", Preco = 10},
        };
    }



}
