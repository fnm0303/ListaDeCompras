using ListaDeCompras.ConsoleApp.Compartilhado;

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
    Aberto,
    Concluído
}

public class ListaCompras : EntidadeBase
{
    public ListaCompras(string nome)
    {
        Id = GeradorIdsListaCompras.GerarId();
        Nome = nome;
        DataCriacao = DateTime.Now;
    }

    public string Nome { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public StatusListaCompras Status { get; private set; } = StatusListaCompras.Aberto;
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        ListaCompras listaAtualizada = (ListaCompras)entidadeAtualizada;

        Nome = listaAtualizada.Nome;
        Status = listaAtualizada.Status;
    }
}
