using System.Security.Cryptography.X509Certificates;
using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloItemListaCompras;
using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

public class TelaListaCompras : TelaBase<ListaCompras>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioListaCompras repositorioListaCompras;
    private readonly RepositorioProduto repositorioProduto;

    public TelaListaCompras(RepositorioListaCompras repositorioListaCompras,
    RepositorioProduto repositorioProduto) : base("Lista de Compras", repositorioListaCompras)
    {
        this.repositorioListaCompras = repositorioListaCompras;
        this.repositorioProduto = repositorioProduto;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Gestão de Listas de Compras");
        Console.WriteLine("------------------------");
        Console.WriteLine($"1 - Cadastrar Lista de Compra");
        Console.WriteLine($"2 - Editar Lista de Compra");
        Console.WriteLine($"3 - Excluir Lista de Compra");
        Console.WriteLine($"4 - Visualizar Lista de Compras");
        Console.WriteLine($"5 - Adicionar Item à Lista de Compras");
        Console.WriteLine($"6 - Remover Item de Lista de Compras");
        Console.WriteLine($"7 - Visualizar Itens de Lista de Compras");
        Console.WriteLine("S - Sair");
        Console.WriteLine("------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
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

    public void AdicionarItem()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("Adição de Item à Lista de Compras");
        Console.WriteLine("------------------------");

        VisualizarTodos(false);

        Console.WriteLine("------------------------");
        Console.WriteLine("Informe o Id da lista que deseja editar: ");
        int idListaSelecionada = Convert.ToInt32(Console.ReadLine());

        ListaCompras? listaSelecionada = repositorioListaCompras.SelecionarPorId(idListaSelecionada);

        Console.WriteLine("------------------------");
        Console.WriteLine($"Gestão da Lista de Compras \"{listaSelecionada.Nome}\"");
        Console.WriteLine("------------------------");

        VisualizarProdutos();

        Console.WriteLine("------------------------");
        Console.WriteLine("Informe o Id do produto que deseja adicionar: ");
        int idProdutoSelecionado = Convert.ToInt32(Console.ReadLine());

        Produto? produtoSelecionado = repositorioProduto.SelecionarPorId(idProdutoSelecionado);

        Console.WriteLine("Informe a quantidade do produto: ");
        int quantidadeProduto = Convert.ToInt32(Console.ReadLine());

        ItemListaCompras itemListaCompras = new ItemListaCompras(produtoSelecionado!, quantidadeProduto);

        Console.WriteLine($"Deseja realmente adicionar o produto: {produtoSelecionado.Nome} x{quantidadeProduto} à lista? (s/N): ");
        string opcaoSelecionada = Console.ReadLine();

        if (opcaoSelecionada.ToUpper() != "S")
            return;

        listaSelecionada.AdicionarItem(itemListaCompras);

        repositorioListaCompras.Editar(idListaSelecionada, listaSelecionada);

        Console.WriteLine($"O item \"{produtoSelecionado.Nome} x{quantidadeProduto}\" foi cadastrado com sucesso.");
        Console.WriteLine("------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void RemoverItem()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("Remoção de Item à Lista de Compras");
        Console.WriteLine("------------------------");

        ListaCompras listaSelecionada = VisualizarItens(false);

        Console.WriteLine("------------------------");
        Console.WriteLine("Informe o ID do item da lista que deseja remover: ");
        int idItemListaSelecionado = Convert.ToInt32(Console.ReadLine());

        listaSelecionada.RemoverItem(idItemListaSelecionado);

        repositorioListaCompras.Editar(listaSelecionada.Id, listaSelecionada);

        Console.WriteLine($"O item foi removido com sucesso.");
        Console.WriteLine("------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public ListaCompras VisualizarItens(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Visualização de Itens de Lista de Compras");
            Console.WriteLine("------------------------");
        }

        VisualizarTodos(false);

        Console.WriteLine("------------------------");
        Console.WriteLine("Informe o Id da lista que deseja visualizar: ");
        int idListaSelecionada = Convert.ToInt32(Console.ReadLine());

        ListaCompras? listaSelecionada = repositorioListaCompras.SelecionarPorId(idListaSelecionada);

        Console.WriteLine("------------------------");
        Console.WriteLine($"Gestão da Lista de Compras \"{listaSelecionada.Nome}\"");
        Console.WriteLine("------------------------");

        Console.WriteLine("{0, -7} | {1, -20} | {2, -15} | {3, -12}",
                            "Id", "Produto", "Quantidade", "Preço Total");

        ItemListaCompras[] itensDaLista = listaSelecionada.Itens; ;

        for (int i = 0; i < itensDaLista.Length; i++)
        {
            ItemListaCompras item = itensDaLista[i];
            if (item == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -15} | {3, -12}",
                            item.Id, item.Produto.Nome, item.Quantidade, item.PrecoTotal.ToString("C2"));
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }

        return listaSelecionada;
    }

    private void VisualizarProdutos()
    {
        Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -12} | {4, -10}",
                     "Id", "Nome", "Un. Medida", "Preço aprox.", "Categoria");

        Produto[] produtos = repositorioProduto.SelecionarTodos();

        for (int i = 0; i < produtos.Length; i++)
        {
            Produto p = produtos[i];
            if (p == null)
                continue;

            Console.WriteLine("{0, -7} | {1, -20} | {2, -12} | {3, -12} | {4, -10}",
                            p.Id, p.Nome, p.UniMedida, p.PrecoAproximado.ToString("N2"), p.Categoria.Nome);
        }
    }

    protected override ListaCompras ObterDadosCadastrais()
    {
        Console.Write("Informe o nome da lista de compras: ");
        string? nome = Console.ReadLine();

        return new ListaCompras(nome!);
    }
}
