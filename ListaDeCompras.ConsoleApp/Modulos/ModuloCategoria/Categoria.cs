/*
- Campos obrigatórios:
- Nome (texto único, máximo 50 caracteres)
- Cor (seleção de paleta ou hexadecimal)
- Não pode haver categorias com nomes duplicados
- Não permitir excluir uma categoria caso tenha produtos vinculados
*/

using ListaDeCompras.ConsoleApp.Compartilhado;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

public static class GeradorIdsCategoria
{
    private static int contadorIds = 1;

    public static int GerarId()
    {
        return contadorIds++;
    }
}
public enum CorCategoria
{
    Branco,
    Vermelho,
    Verde,
    Azul
}
public class Categoria : EntidadeBase
{
    public string Nome { get; set; }
    public CorCategoria Cor { get; set; } //por padrão primeiro valor será BRANCO

    public Categoria()
    {

    }

    public Categoria(string nome, CorCategoria cor)
    {
        Id = GeradorIdsCategoria.GerarId();
        Nome = nome;
        Cor = cor;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome)) //se nome estiver vazio
            erros.Add("O campo \"Nome\" precisa ser preenchido.");

        else if (Nome.Length > 50)
            erros.Add("O campo \"Nome\" pode ter no máximo 50 caracteres.");

        if (!Enum.IsDefined(Cor)) //se a cor não estiver definida no nosso Enum
            erros.Add("O campo \"Cor\" deve conter uma seleção válida (Branco, Vermelho, Verde, Azul).");

        return erros;
    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

        Nome = categoriaAtualizada.Nome;
        Cor = categoriaAtualizada.Cor;
    }

}
