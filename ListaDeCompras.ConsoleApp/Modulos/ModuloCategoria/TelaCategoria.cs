using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

public class TelaCategoria : TelaBase<Categoria>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioCategoria repositorioCategoria;
    private readonly RepositorioProduto repositorioProduto;

    public TelaCategoria(RepositorioCategoria repositorioCategoria, RepositorioProduto repositorioProduto) : base("Categoria", repositorioCategoria)
    {
        this.repositorioCategoria = repositorioCategoria;
        this.repositorioProduto = repositorioProduto;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Visualização de Categorias");
            Console.WriteLine("------------------------");
        }

        Console.WriteLine("{0, -7} | {1, -20} | {2, -10}",
                            "Id", "Nome", "Cor");

        Categoria[] registros = repositorioCategoria.SelecionarTodos();

        for (int i = 0; i < registros.Length; i++)
        {
            Categoria c = registros[i];
            if (c == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -10}",
                            c.Id, c.Nome, c.Cor);
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override Categoria ObterDadosCadastrais()
    {
        Console.Write("Informe o nome da categoria: ");
        string? nome = Console.ReadLine();

        Console.WriteLine("------------------------");
        Console.WriteLine("Selecione uma cor disponível para a categoria");
        Console.WriteLine("------------------------");
        Console.WriteLine("1 - Branco (padrão)");
        Console.WriteLine("2 - Vermelho");
        Console.WriteLine("3 - Verde");
        Console.WriteLine("4 - Azul");

        Console.Write("Informe a cor da categoria: ");
        string? corSelecionada = Console.ReadLine();

        CorCategoria cor;

        switch (corSelecionada)
        {
            case "1":
                cor = CorCategoria.Branco;
                break;
            case "2":
                cor = CorCategoria.Vermelho;
                break;
            case "3":
                cor = CorCategoria.Verde;
                break;
            case "4":
                cor = CorCategoria.Azul;
                break;

            default:
                cor = CorCategoria.Branco;
                break;
        }

        return new Categoria(nome!, cor);
    }

    protected override bool ExisteRegistroComInformacoesExclusivas(Categoria entidade, int? idIgnorado = null)
    {
        Categoria[] categorias = repositorioCategoria.SelecionarTodos();

        for (int i = 0; i < categorias.Length; i++)
        {
            Categoria c = categorias[i];

            if (c == null)
                continue;

            if (idIgnorado != c.Id && entidade.Nome == c.Nome)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Já existe uma categoria com o nome \"{c.Nome}\".");
                Console.WriteLine("------------------------");
                return true;
            }
        }

        return base.ExisteRegistroComInformacoesExclusivas(entidade);
    }

    protected override bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        Produto[] produtos = repositorioProduto.SelecionarTodos();

        for (int i = 0; i < produtos.Length; i++)
        {
            Produto p = produtos[i];

            if (p == null)
                continue;

            if (p.Categoria.Id == idRegistro)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine($"Existe(m) produto(s) relacionado(s) a essa categoria.");
                Console.WriteLine("------------------------");
                return true;
            }

        }

        return base.ExistemDependenciasAtivasDoRegistro(idRegistro);
    }
}
