using appClassePessoaBD.Model;
using System.Collections.ObjectModel;

namespace appClassePessoaBD.Views;

public partial class TelaListaPessoa : ContentPage
{
    /* A ObservableCollection é uma classe que armazena um array de objetos do tipo Pessoa.
     * Utilizamos essa classe quando estamos apresentando um array de objetos ao usuário.
     * O Diferencial dessa classe é que toda vez que um item é add, removido ou modificado
     * no array de objetos a interface gráfica também é atualizada.
     * Assim as modificações feitas no array sempre estão atualizadas para o usuário.*/
    ObservableCollection<Pessoa> listagemPessoas = new ObservableCollection<Pessoa>();

    public TelaListaPessoa()
	{
		InitializeComponent();
        //Desabilita a seta de voltar nesta página
        NavigationPage.SetHasBackButton(this, false);
        /*
         * Referenciando a fonte itens (a serem mostrados ao usuário),
         * que está na linha 19 do XAMl, a ListView é a ObservableCollection definida acima.
         * Fazendo essa definição no construtor estamos amarrando a fonte de dados
         * da ListView assim que ela é criada. */
        lstPessoas.ItemsSource = listagemPessoas;
    }
    /*
     * Tratamento do evento de clique no ToolBarItem, queestá na linha 11 do XAML,
     * que fará a navegação da tela de listagem até a tela Incluir Pessoa.
     * A navegação está envolvida em um try catch e se algum problema acontecer
     * a mensagem da exceção será mostrada ao usuário via DisplayAlert. */
    private async void irTelaIncluirPessoa(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new TelaIncluirPessoa());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro no Cadastro da Pessoa !!!!", ex.Message, "OK");
        }
    }
    /*
     * Método executado quando a página é exibida ao usuário.
     */
    protected async override void OnAppearing()
    {
        try
        {
            // Limpa a lista atual
            listagemPessoas.Clear();
            // Carrega todas as pessoas do banco de dados de forma assíncrona
            List<Pessoa> temp = await App.Database.GetAll();
            // Percorre a lista retornada e adiciona cada pessoa à coleção exibida na interface
            temp.ForEach(i => listagemPessoas.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no Carregamento da Lista de Pessoas !!!!", ex.Message, "OK");
        }
    }
    /* Trata o evento Clicked do MenuItem da ViewCell.ContextActions, conforme linha 39 di XAML,
 * perguntando ao usuário se ele realmente deseja remover aquele item do arquivo db3. */
    private async void excluirPessoa(object sender, EventArgs e)
    {
        try
        {
            // Reconhecendo qual foi a linha do ListView que disparou o evento de exclusão.
            MenuItem itemSelecionado = sender as MenuItem;
            // Obtendo qual foi a Pessoa que estava anexada no BindingContext
            Pessoa pessoaSelecionada = itemSelecionado.BindingContext as Pessoa;
            /* Perguntando ao usuário se ele realmente deseja excluir. Note o await para aguardar
             * a resposta do usuário antes de prosseguir com o código. */
            bool confirmacao = await DisplayAlert("Tem Certeza que quer excluir a Pessoa?",
            $"Excluir {pessoaSelecionada.pesNome}", "Sim", "Não");
            if (confirmacao)
            {
                // Removendo o registro do db3 via método Delete da classe crudPessoaSQLite.
                await App.Database.Delete(pessoaSelecionada.pesID);
                /* Removendo o item da ObservableCollection também, que é automaticamente
                 * removida da visão do usuário na ListView.  */
                listagemPessoas.Remove(pessoaSelecionada);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Exclusão da Pessoa !!!!", ex.Message, "OK");
        }
    }
    /* Trata o evento TextChanged da SearchBar recebendo os novos valores digitados,
 * conforme linha 17 do arquivo XAML. */
    private async void txtBuscarPessoa(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Obtendo o valor que foi digitado no Search (busca), conforme linha 19 do XAML
            string busca = e.NewTextValue;
            lstPessoas.IsRefreshing = true;
            // Limpando a ObservableCollection antes de add os itens vindos da busca.
            // Limpa todos os itens atualmente exibidos na lista de pessoas
            listagemPessoas.Clear();
            // Busca todas as pessoas cadastradas no banco de dados de forma assíncrona
            List<Pessoa> temp = await App.Database.Search(busca);
            // Percorre a lista retornada e adiciona cada pessoa à coleção exibida na interface
            temp.ForEach(i => listagemPessoas.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro na Busca de Pessoas !!!!", ex.Message, "OK");
        }
        finally
        {
            // Encerra a animação de atualização (refresh) da lista,
            // ocultando o indicador visual de carregamento
            lstPessoas.IsRefreshing = false;
        }
    }
    /* Trata o evento ItemSelected da ListView navegando para a página de detalhes,
     * conforme linha 20 do arquivo xaml. */
    private void lstPessoasItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        /* Forma contraída de definir o BindingContext da página TelaAlterarPessoa,
         * como sendo a Pessoa que foi selecionada na ListView (item da ListView) e
         * em seguida já redirecionando na navegação. */
        try
        {
            // Obtém o item selecionado na lista e o converte para o tipo Pessoa
            Pessoa pessoa1 = e.SelectedItem as Pessoa;
            // Navega para a tela de alteração de pessoa
            Navigation.PushAsync(new TelaAlterarPessoa
            {
                // Define a pessoa selecionada como contexto de dados da nova tela,
                // permitindo exibir e editar suas informações
                BindingContext = pessoa1,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro Desconhecido na Seleção de Pessoa !!!!", ex.Message, "OK");
        }
    }
    // Método executado quando o usuário realiza o gesto de atualização (pull-to-refresh)
    // na lista de dados. O modificador async permite utilizar operações assíncronas com await.
    private async void refCarregando(object sender, EventArgs e)
    {
        try
        {
            // Limpa a lista atual
            listagemPessoas.Clear();
            // Carrega todas as pessoas do banco de dados de forma assíncrona
            List<Pessoa> temp = await App.Database.GetAll();
            // Percorre a lista retornada e adiciona cada pessoa à coleção exibida na interface
            temp.ForEach(i => listagemPessoas.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro Desconhecido no carregamento de Pessoas !!!!", ex.Message, "OK");
        }
        finally
        {
            // Encerra a animação de atualização (refresh) da lista,
            // ocultando o indicador visual de carregamento
            lstPessoas.IsRefreshing = false;
        }
    }

}