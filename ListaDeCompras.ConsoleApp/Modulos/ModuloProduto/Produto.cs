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
    Kg,
    Unidade,
    Litro,
    Caixa
}

public class Produto : EntidadeBase
{
    public string Nome { get; private set; }
    public UnidadeMedida UniMedida { get; private set; }
    public double PrecoAproximado { get; private set; }

    public Categoria Categoria { get; private set; }

    public Produto(string nome, UnidadeMedida uniMedida, double precoAproximado, Categoria categoria)
    {
        Id = GeradorIdsProdutos.GerarId();
        Nome = nome;
        UniMedida = uniMedida;
        PrecoAproximado = precoAproximado;
        Categoria = categoria;
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