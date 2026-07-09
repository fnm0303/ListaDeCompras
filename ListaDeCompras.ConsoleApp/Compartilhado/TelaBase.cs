namespace ListaDeCompras.ConsoleApp.Compartilhado;

public abstract class TelaBase<TEntidade> where TEntidade : EntidadeBase
{
    private string nomeEntidade = string.Empty; //string vazia
    private readonly RepositorioBase<TEntidade> repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<TEntidade> repositorio) //apenas classes que herdam vão poder acessar esse construtor
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }
    public virtual string? ObterOpcaoMenu() //virtual = vai manter uma implementação base e permite que outras classes implementem esse método (override)
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}s");
        Console.WriteLine("------------------------");
        Console.WriteLine($"1 - Cadastrar {nomeEntidade}");
        Console.WriteLine($"2 - Editar {nomeEntidade}");
        Console.WriteLine($"3 - Excluir {nomeEntidade}");
        Console.WriteLine($"4 - Visualizar {nomeEntidade}s");
        Console.WriteLine("S - Sair");
        Console.WriteLine("------------------------");
        Console.Write("> ");
        string? opcaoMenuInterno = Console.ReadLine()?.ToUpper();

        return opcaoMenuInterno;
    }

    public void Cadastrar()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Cadastro de {nomeEntidade}s");
        Console.WriteLine("------------------------");

        TEntidade novaEntidade = ObterDadosCadastrais();

        List<string> erros = novaEntidade.Validar();

        if (erros.Count > 0)
        {
            string erro = erros.First(); //mostrando o PRIMEIRO erro
            Console.WriteLine("------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erro);
            Console.ResetColor();
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.WriteLine("------------------------");
            Console.ReadLine();
            return;
        }

        if (ExisteRegistroComInformacoesExclusivas(novaEntidade))
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.WriteLine("------------------------");
            Console.ReadLine();
            return;
        }

        repositorio.Cadastrar(novaEntidade);

        Console.WriteLine("------------------------");
        Console.WriteLine($"O registro \"{novaEntidade.Id}\" foi cadastrado com sucesso.");
        Console.WriteLine("------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void Editar()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Edição de {nomeEntidade}");
        Console.WriteLine("------------------------");

        VisualizarTodos(false);

        Console.WriteLine("------------------------");
        Console.Write("Digite o ID do registro que deseja editar: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("------------------------");

        TEntidade entidadeAtualizada = ObterDadosCadastrais();

        List<string> erros = entidadeAtualizada.Validar();

        if (erros.Count > 0)
        {
            string erro = erros.First(); //mostrando o PRIMEIRO erro
            Console.WriteLine("------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erro);
            Console.ResetColor();
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.WriteLine("------------------------");
            Console.ReadLine();
            return;
        }

        if (ExisteRegistroComInformacoesExclusivas(entidadeAtualizada, idSelecionado))
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        repositorio.Editar(idSelecionado, entidadeAtualizada);

        Console.WriteLine("------------------------");
        Console.WriteLine($"O registro \"{entidadeAtualizada.Id}\" foi atualizado com sucesso.");
        Console.WriteLine("------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void Excluir()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine($"Exclusão de {nomeEntidade}");
        Console.WriteLine("------------------------");

        VisualizarTodos(false);

        Console.WriteLine("------------------------");
        Console.Write("Digite o ID do registro que deseja excluir: ");
        int idSelecionado = Convert.ToInt32(Console.ReadLine());

        if (ExistemDependenciasAtivasDoRegistro(idSelecionado))
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        repositorio.Excluir(idSelecionado);

        Console.WriteLine("------------------------");
        Console.WriteLine($"O registro \"{idSelecionado}\" foi excluído com sucesso.");
        Console.WriteLine("------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public abstract void VisualizarTodos(bool deveExibirCabecalho);

    protected abstract TEntidade ObterDadosCadastrais();

    protected virtual bool ExisteRegistroComInformacoesExclusivas(TEntidade entidade, int? idIgnorado = null)
    {
        return false;
    }

    protected virtual bool ExistemDependenciasAtivasDoRegistro(int idRegistro)
    {
        return false;
    }

}
