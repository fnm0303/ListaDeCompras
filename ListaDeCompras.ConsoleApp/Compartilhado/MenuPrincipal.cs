using ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.ConsoleApp.Modulos.ModuloItemListaCompras;
using ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;
using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Compartilhado;

public class MenuPrincipal
{
    private readonly RepositorioCategoria repositorioCategoria;
    private readonly RepositorioProduto repositorioProduto;
    private readonly RepositorioListaCompras repositorioListaCompras;

    public MenuPrincipal()
    {
        ContextoJson contexto = new ContextoJson();

        contexto.Carregar();

        repositorioCategoria = new RepositorioCategoria(contexto);
        repositorioProduto = new RepositorioProduto(contexto);
        repositorioListaCompras = new RepositorioListaCompras(contexto);

    }
    public ITelaOpcoes? ObterOpcaoMenuPrincipal()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("LISTA DE COMPRAS");
        Console.WriteLine("------------------------");
        Console.WriteLine("1 - Gerenciar Categorias");
        Console.WriteLine("2 - Gerenciar Produtos");
        Console.WriteLine("3 - Gerenciar Listas de Compras");
        Console.WriteLine("S - Sair");
        Console.WriteLine("------------------------");
        Console.Write("> ");

        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCategoria(repositorioCategoria, repositorioProduto);

        if (opcaoMenuPrincipal == "2")
            return new TelaProduto(repositorioProduto, repositorioCategoria);

        if (opcaoMenuPrincipal == "3")
            return new TelaListaCompras(repositorioListaCompras, repositorioProduto);

        return null;
    }
}
