/*
Regras de Negócio:
- Campos obrigatórios:
- Nome (2 a 100 caracteres)

- Categoria (seleção obrigatória)
- Unidade de medida (ex: kg, unidade, litro, caixa)
- Preço aproximado
- Não pode haver produtos com o mesmo nome na mesma categoria
*/

using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;

public static class GeradorIdsProdutos
{
    private static int contadorIds = 1;

    public static int GerarId()
    {
        return contadorIds++;
    }
}
public enum UnidadeMedida
{
    Unidade,
    Kg,
    Litro,
    Caixa
}

public class Produto : EntidadeBase
{
    public string Nome { get; set; }
    public UnidadeMedida UniMedida { get; set; }
    public double PrecoAproximado { get; set; }

    public Categoria Categoria { get; set; }

    public Produto()
    {

    }

    public Produto(string nome, UnidadeMedida uniMedida, double precoAproximado, Categoria categoria)
    {
        Id = GeradorIdsProdutos.GerarId();
        Nome = nome;
        UniMedida = uniMedida;
        PrecoAproximado = precoAproximado;
        Categoria = categoria;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");

        if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve ter entre 2 e 100 caracteres.");

        if (Categoria == null)
            erros.Add("O campo \"Categoria\" deve ser preenchido.");

        if (PrecoAproximado == 0 || PrecoAproximado < 0)
            erros.Add("O campo \"Preço aproximado\" não pode ser zero ou negativo.");

        if (!Enum.IsDefined(UniMedida))
            erros.Add("O campo \"Unidade Medida\" deve conter uma seleção válida (Unidade, Kg, Litro ou Caixa).");

        return erros;
    }
    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Produto produtoAtualizado = (Produto)entidadeAtualizada;
        Nome = produtoAtualizado.Nome;
        UniMedida = produtoAtualizado.UniMedida;
        PrecoAproximado = produtoAtualizado.PrecoAproximado;
        Categoria = produtoAtualizado.Categoria;
    }


}