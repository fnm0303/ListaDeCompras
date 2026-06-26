using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
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
        repositorioCategoria = new RepositorioCategoria();
        repositorioProduto = new RepositorioProduto();

        Categoria categoriaTeste = new Categoria("Produtos de Limpeza", CorCategoria.Vermelho);
        repositorioCategoria.Cadastrar(categoriaTeste);

        Produto produtoTeste = new Produto("Amaciante", UnidadeMedida.Caixa, 190, categoriaTeste);
        repositorioProduto.Cadastrar(produtoTeste);

        ListaCompras listaTeste = new ListaCompras("Compras da semana");
        repositorioListaCompras = new RepositorioListaCompras();
        repositorioListaCompras.Cadastrar(listaTeste);

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
            return new TelaListaCompras(repositorioListaCompras);

        return null;
    }
}
