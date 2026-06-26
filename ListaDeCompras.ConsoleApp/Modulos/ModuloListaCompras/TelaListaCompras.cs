using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

public class TelaListaCompras : TelaBase, ITelaOpcoes
{
    private readonly RepositorioListaCompras repositorioListaCompras;

    public TelaListaCompras(RepositorioListaCompras repositorioListaCompras) : base("Lista de Compras", repositorioListaCompras)
    {
        this.repositorioListaCompras = repositorioListaCompras;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Visualização de Lista de Compras");
            Console.WriteLine("------------------------");
        }

        Console.WriteLine("{0, -7} | {1, -20} | {2, -15} | {3, -12}",
                            "Id", "Nome", "Data de Criação", "Status");

        EntidadeBase[] registros = repositorioListaCompras.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            ListaCompras l = (ListaCompras)registros[i];
            if (l == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -15} | {3, -12}",
                            l.Id, l.Nome, l.DataCriacao.ToShortDateString(), l.Status);
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        Console.Write("Informe o nome da lista de compras: ");
        string? nome = Console.ReadLine();

        return new ListaCompras(nome!);
    }
}
