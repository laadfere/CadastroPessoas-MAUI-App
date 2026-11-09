using appClassePessoaBD.Model;
using System;
using System.IO;

namespace appClassePessoaBD.Views
{
    public partial class TelaIncluirPessoa : ContentPage
    {
        // Instância local da classe DAL do SQLite
        readonly string caminhoBanco = Path.Combine(FileSystem.AppDataDirectory, "pessoas.db3");

        public TelaIncluirPessoa()
        {
            InitializeComponent();
        }

        private async void OnSalvarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                await DisplayAlert("Aviso", "Por favor, informe o nome.", "OK");
                return;
            }

            int.TryParse(txtIdade.Text, out int idade);

            Pessoa novaPessoa = new Pessoa
            {
                pesNome = txtNome.Text,
                pesIdade = idade,
                pesEmail = txtEmail.Text
            };

            // Abre a conexão DAL e insere o registro no banco SQLite
            DAL.crudPessoaSQLite db = new DAL.crudPessoaSQLite(caminhoBanco);
            await db.Insert(novaPessoa);

            await DisplayAlert("Sucesso", "Pessoa cadastrada com sucesso!", "OK");
            await Navigation.PopAsync(); // Volta para a tela anterior
        }
    }
}