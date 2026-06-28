using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Modulos.ModuloListaCompras;

MenuPrincipal menuPrincipal = new MenuPrincipal();

while (true)
{
    ITelaOpcoes? telaSelecionada = menuPrincipal.ObterOpcaoMenuPrincipal();

    if (telaSelecionada == null)
        break;

    while (true)
    {
        string? opcaoMenuInterno = telaSelecionada.ObterOpcaoMenu();

        if (opcaoMenuInterno == "S")
            break;

        if (telaSelecionada is TelaBase telaBase)
        {
            if (opcaoMenuInterno == "1")
                telaBase.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaBase.Editar();

            else if (opcaoMenuInterno == "3")
                telaBase.Excluir();

            else if (opcaoMenuInterno == "4")
                telaBase.VisualizarTodos(true);

            if (telaBase is TelaListaCompras telaListaCompras)
            {
                if (opcaoMenuInterno == "5")
                    telaListaCompras.AdicionarItem();

                else if (opcaoMenuInterno == "6")
                    telaListaCompras.RemoverItem();

                else if (opcaoMenuInterno == "7")
                    telaListaCompras.VisualizarItens(true);
            }
        }
    }
}
