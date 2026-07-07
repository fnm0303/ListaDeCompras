using System.Text.Json;
using System.Text.Json.Serialization;
using ListaDeCompras.ConsoleApp.Modulos.ModuloCategoria;
using ListaDeCompras.ConsoleApp.Modulos.ModuloProduto;
using ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

namespace ListaDeCompras.ConsoleApp.Compartilhado.Arquivos;

public class ContextoJson
{
    public List<Categoria> Categorias { get; set; } = new List<Categoria>(); //já inicializando a lista

    public List<Produto> Produtos { get; set; } = new List<Produto>();

    public List<ListaCompras> ListasDeCompras { get; set; } = new List<ListaCompras>();

    private readonly string caminhoArquivoDados;

    public ContextoJson()
    {
        string caminhoAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        string caminhoDiretorioAplicativo = Path.Join(caminhoAppData, "ListaDeCompras"); //criando string da pasta

        Directory.CreateDirectory(caminhoDiretorioAplicativo); //criando a pasta no sistema

        caminhoArquivoDados = Path.Join(caminhoDiretorioAplicativo, "dados.json"); //criando string do arquivo
    }

    public void Salvar()
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        string jsonString = JsonSerializer.Serialize(this, options);

        File.WriteAllText(caminhoArquivoDados, jsonString);
    }

    public void Carregar()
    {
        if (!File.Exists(caminhoArquivoDados))
            return;

        string jsonString = File.ReadAllText(caminhoArquivoDados);

        JsonSerializerOptions options = new JsonSerializerOptions();
        options.WriteIndented = true;
        options.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoJson? contextoSalvo =
            JsonSerializer.Deserialize<ContextoJson>(jsonString, options);

        if (contextoSalvo == null)
            return;

        Categorias = contextoSalvo.Categorias;
        Produtos = contextoSalvo.Produtos;
        ListasDeCompras = contextoSalvo.ListasDeCompras;
    }
}