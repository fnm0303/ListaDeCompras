using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

public class TelaProduto : TelaBase<Produto>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioProduto repositorioProduto;
    private readonly RepositorioCategoria repositorioCategoria;

    public TelaProduto(RepositorioProduto repositorioProduto, RepositorioCategoria repositorioCategoria) : base("Produto", repositorioProduto)
    {
        this.repositorioProduto = repositorioProduto;
        this.repositorioCategoria = repositorioCategoria;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Visualização de Produtos");
            Console.WriteLine("------------------------");
        }

        Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -12} | {4, -10}",
                            "Id", "Nome", "Un. Medida", "Preço aprox.", "Categoria");

        EntidadeBase[] registros = repositorioProduto.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            Produto p = (Produto)registros[i];
            if (p == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -12} | {4, -10}",
                            p.Id, p.Nome, p.UniMedida, p.PrecoAproximado.ToString("N2"), p.Categoria.Nome);
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Produto ObterDadosCadastrais()
    {
        Console.WriteLine("Informe o nome do produto: ");
        string? nome = Console.ReadLine();

        Console.WriteLine("------------------------");
        Console.WriteLine("Selecione uma unidade de medida para o produto: ");
        Console.WriteLine("------------------------");
        Console.WriteLine("1 - Unidade");
        Console.WriteLine("2 - Kg");
        Console.WriteLine("3 - Litro");
        Console.WriteLine("4 - Caixa");
        Console.WriteLine("------------------------");
        Console.WriteLine("Informe a unidade de medida escolhida: ");
        string? unidadeSelecionada = Console.ReadLine();

        UnidadeMedida medida;

        switch (unidadeSelecionada)
        {
            case "1":
                medida = UnidadeMedida.Unidade;
                break;
            case "2":
                medida = UnidadeMedida.Kg;
                break;
            case "3":
                medida = UnidadeMedida.Litro;
                break;
            case "4":
                medida = UnidadeMedida.Caixa;
                break;

            default:
                medida = UnidadeMedida.Unidade;
                break;
        }

        Console.WriteLine("Informe o preço aproximado do produto: ");
        double preco = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("------------------------");
        Console.WriteLine("Visualização de Categorias");
        Console.WriteLine("------------------------");

        Console.WriteLine("{0, -7} | {1, -20} | {2, -10}",
                            "Id", "Nome", "Cor");

        EntidadeBase[] registros = repositorioCategoria.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            Categoria c = (Categoria)registros[i];
            if (c == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -10}",
                            c.Id, c.Nome, c.Cor);
        }

        Console.WriteLine("------------------------");
        Console.WriteLine("Digite o ID do registro que deseja selecionar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Categoria? categoriaSelecionada = repositorioCategoria.SelecionarPorId(idSelecionado);

        return new Produto(nome!, medida, preco, categoriaSelecionada!);

    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Produto entidade, int? idIgnorado = null)
    {
        Produto[] produtos = repositorioProduto.SelecionarTodos();

        for (int i = 0; i < produtos.Length; i++)
        {
            Produto p = produtos[i];

            if (p == null)
                continue;

            if (p.Id != idIgnorado && p.Nome == entidade.Nome.ToLower() && p.Categoria == entidade.Categoria)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Já existe um produto com o nome \"{p.Nome}\".");
                Console.WriteLine("------------------------");
                return true;
            }
        }
        return base.ExisteRegistroComInformacoesExclusivas(entidade, idIgnorado);
    }

}
