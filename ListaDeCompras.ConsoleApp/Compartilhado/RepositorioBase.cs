namespace ListaDeCompras.ConsoleApp.Compartilhado;

public abstract class RepositorioBase<TEntidade> where TEntidade : EntidadeBase //where = ONDE TEndidade É uma EB
{  //Tipo Entidade, pode ser qualquer tipo
    private readonly TEntidade[] registros = new TEntidade[100];

    public void Cadastrar(TEntidade novoRegistro)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            if (registros[i] == null)
            {
                registros[i] = novoRegistro;
                break;
            }
        }
    }

    public bool Editar(int idSelecionado, TEntidade entidadeAtualizada)
    {
        TEntidade? entidadeSelecionada = SelecionarPorId(idSelecionado);

        if (entidadeSelecionada == null)
            return false;

        entidadeSelecionada.Atualizar(entidadeAtualizada);

        return true;
    }

    public bool Excluir(int idSelecionado)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            TEntidade o = registros[i];

            if (o == null)
                continue;

            if (o.Id == idSelecionado)
            {
                registros[i] = null;
                return true;
            }
        }
        return false;
    }

    public TEntidade? SelecionarPorId(int idSelecionado)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            TEntidade o = registros[i];

            if (o == null)
                continue;

            if (o.Id == idSelecionado)
                return o;

        }
        return null;
    }
    public TEntidade[] SelecionarTodos()
    {
        return registros;
    }
}
