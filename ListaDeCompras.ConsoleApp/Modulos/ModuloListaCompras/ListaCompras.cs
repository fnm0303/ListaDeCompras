using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloItemListaCompras;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

public static class GeradorIdsListaCompras
{
    private static int contadorIds = 1;

    public static int GerarId()
    {
        return contadorIds++;
    }
}
public enum StatusListaCompras
{
    Aberta,
    Concluído
}

public class ListaCompras : EntidadeBase
{
    public string Nome { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public StatusListaCompras Status { get; private set; } = StatusListaCompras.Aberta;

    public ItemListaCompras[] Itens { get; private set; } = new ItemListaCompras[100];

    public ListaCompras(string nome)
    {
        Id = GeradorIdsListaCompras.GerarId();
        Nome = nome;
        DataCriacao = DateTime.Now;
    }

    public void AdicionarItem(ItemListaCompras itemLista)
    {
        for (int i = 0; i < Itens.Length; i++)
        {
            if (Itens[i] == null)
            {
                Itens[i] = itemLista;
                return;
            }
        }
    }

    public void RemoverItem(int idItemLista)
    {
        for (int i = 0; i < Itens.Length; i++)
        {
            if (Itens[i] == null)
                continue;

            if (Itens[i].Id == idItemLista)
            {
                Itens[i] = null;
                return;
            }
        }
    }

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        ListaCompras listaAtualizada = (ListaCompras)entidadeAtualizada;

        Nome = listaAtualizada.Nome;
        Status = listaAtualizada.Status;
    }


}
