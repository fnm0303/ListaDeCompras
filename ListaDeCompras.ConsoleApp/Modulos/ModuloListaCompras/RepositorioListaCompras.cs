using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

public class RepositorioListaCompras : RepositorioBase<ListaCompras>
{
    public RepositorioListaCompras(ContextoJson contexto) : base(contexto)
    {
    }

    protected override List<ListaCompras> ObterRegistros()
    {
        return contexto.ListasDeCompras;
    }
}
