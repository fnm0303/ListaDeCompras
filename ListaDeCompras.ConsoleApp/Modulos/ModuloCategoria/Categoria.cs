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

    public override void Atualizar(EntidadeBase entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

        Nome = categoriaAtualizada.Nome;
        Cor = categoriaAtualizada.Cor;
    }
}
