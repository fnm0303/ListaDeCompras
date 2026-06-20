namespace ListaDeCompras.ConsoleApp.Compartilhado;

public abstract class EntidadeBase
{
    public int Id { get; set; }

    public abstract void Atualizar(EntidadeBase entidadeAtualizada);

}
