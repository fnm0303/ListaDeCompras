using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloItemListaCompras;

public static class GeradorIdsItemListaCompras
{
    private static int contadorIds = 1;

    public static int GerarId()
    {
        return contadorIds++;
    }
}

public class ItemListaCompras
{
    public int Id { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }

    public double PrecoTotal
    {
        get
        {
            return Produto.PrecoAproximado * Quantidade;
        }
    }

    public ItemListaCompras(Produto produto, int quantidade)
    {
        Id = GeradorIdsItemListaCompras.GerarId();

        Produto = produto;
        Quantidade = quantidade;
    }

}
