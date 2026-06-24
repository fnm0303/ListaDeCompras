using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

public class TelaProduto : TelaBase, ITelaOpcoes
{
    private readonly RepositorioProduto repositorioProduto;

    public TelaProduto(RepositorioProduto repositorioProduto) : base("Produto", repositorioProduto)
    {
        this.repositorioProduto = repositorioProduto;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Visualização de Produtos");
            Console.WriteLine("------------------------");
        }

        Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -10}",
                            "Id", "Nome", "Un. Medida", "Preço aprox.");

        EntidadeBase[] registros = repositorioProduto.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            Produto p = (Produto)registros[i];
            if (p == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -10}",
                            p.Id, p.Nome, p.UniMedida, p.PrecoAproximado.ToString("N2"));
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
        Console.WriteLine("Informe o nome do produto: ");
        string? nome = Console.ReadLine();

        Console.WriteLine("------------------------");
        Console.WriteLine("Selecione uma unidade de medida para o produto: ");
        Console.WriteLine("------------------------");
        Console.WriteLine("1 - Kg");
        Console.WriteLine("2 - Unidade");
        Console.WriteLine("3 - Litro");
        Console.WriteLine("4 - Caixa");
        Console.WriteLine("------------------------");
        Console.WriteLine("Informe a unidade de medida escolhida: ");
        string? unidadeSelecionada = Console.ReadLine();

        UnidadeMedida medida;

        switch (unidadeSelecionada)
        {
            case "1":
                medida = UnidadeMedida.Caixa;
                break;
            case "2":
                medida = UnidadeMedida.Unidade;
                break;
            case "3":
                medida = UnidadeMedida.Litro;
                break;
            case "4":
                medida = UnidadeMedida.Caixa;
                break;

            default:
                medida = UnidadeMedida.Caixa;
                break;
        }

        Console.WriteLine("Informe o preço aproximado do produto: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        return new Produto(nome!, medida, preco);

    }
}
