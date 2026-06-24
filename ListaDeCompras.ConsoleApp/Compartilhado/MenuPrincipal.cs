using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Compartilhado;

public class MenuPrincipal
{
    private readonly RepositorioCategoria repositorioCategoria;
    private readonly RepositorioProduto repositorioProduto;

    public MenuPrincipal()
    {
        Categoria categoriaTeste = new Categoria("Produtos de Limpeza", CorCategoria.Vermelho);
        Produto produtoTeste = new Produto("Amaciante", UnidadeMedida.Caixa, 190);

        repositorioCategoria = new RepositorioCategoria();
        repositorioCategoria.Cadastrar(categoriaTeste);

        repositorioProduto = new RepositorioProduto();
        repositorioProduto.Cadastrar(produtoTeste);

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
            return new TelaCategoria(repositorioCategoria);

        if (opcaoMenuPrincipal == "2")
            return new TelaProduto(repositorioProduto);

        if (opcaoMenuPrincipal == "3")
            return null;

        return null;
    }
}
