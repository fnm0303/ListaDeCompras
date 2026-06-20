using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.Compartilhado;

public class MenuPrincipal
{
    private readonly RepositorioCategoria repositorioCategoria;

    public MenuPrincipal()
    {
        Categoria categoriaTeste = new Categoria("Produtos de Limpeza", CorCategoria.Vermelho);

        repositorioCategoria = new RepositorioCategoria();
        repositorioCategoria.Cadastrar(categoriaTeste);
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
            return null;

        if (opcaoMenuPrincipal == "2")
            return null;

        if (opcaoMenuPrincipal == "3")
            return null;

        return null;
    }
}
