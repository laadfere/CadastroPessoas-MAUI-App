using appClassePessoaBD.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace appClassePessoaBD.DAL
{
    /*
     * Definição da classe crudPessoaSQLite que funciona como uma abstração de acesso ao arquivo db3 do SQLite.
     * A classe contém a conexão e os métodos assíncronos para realizar o CRUD.
     */
    public class crudPessoaSQLite
    {
        readonly SQLiteAsyncConnection _conexao;

        public crudPessoaSQLite(string path)
        {
            // Conexão com o banco SQLite via NuGet Package
            _conexao = new SQLiteAsyncConnection(path);

            // Garante a criação da tabela baseada no Model Pessoa
            _conexao.CreateTableAsync<Pessoa>().Wait();
        }

        // Insere um novo registro no banco de dados
        public async Task<int> Insert(Pessoa pessoa1)
        {
            return await _conexao.InsertAsync(pessoa1);
        }

        // Atualiza os dados de um registro existente
        public async Task<int> Update(Pessoa pessoa1)
        {
            return await _conexao.UpdateAsync(pessoa1);
        }

        // Retorna a lista completa de pessoas cadastradas
        public async Task<List<Pessoa>> GetAll()
        {
            return await _conexao.Table<Pessoa>().ToListAsync();
        }

        // Remove um registro pelo seu ID utilizando LINQ
        public async Task<int> Delete(int idPes)
        {
            return await _conexao.Table<Pessoa>().DeleteAsync(i => i.pesID == idPes);
        }

        // Busca registros cujo nome contenha o termo pesquisado
        public async Task<List<Pessoa>> Search(string buscaPesssoaNome)
        {
            return await _conexao.Table<Pessoa>()
                                 .Where(pessoa1 => pessoa1.pesNome != null && pessoa1.pesNome.Contains(buscaPesssoaNome))
                                 .ToListAsync();
        }

        // Método adicional: Busca registros pelo e-mail cadastrado
        public async Task<List<Pessoa>> SearchByEmail(string email)
        {
            return await _conexao.Table<Pessoa>()
                                 .Where(p => p.pesEmail != null && p.pesEmail.Contains(email))
                                 .ToListAsync();
        }
    }
}