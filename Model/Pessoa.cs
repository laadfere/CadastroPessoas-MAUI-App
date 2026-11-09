using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace appClassePessoaBD.Model
{
    // Define que esta entidade será mapeada para a tabela "Pessoa" no banco de dados
    [Table("Pessoa")]
    public class Pessoa
    {
        // Define que este campo é a chave primária da tabela, auto-incrementada e única
        [PrimaryKey, AutoIncrement, Unique, NotNull]
        public int pesID { get; set; }

        // Nome da pessoa (máximo de 1000 caracteres)
        [MaxLength(1000)]
        public string? pesNome { get; set; }

        // Idade da pessoa
        public int pesIdade { get; set; }

        // Novo campo autoral: E-mail da pessoa (máximo de 250 caracteres)
        [MaxLength(250)]
        public string? pesEmail { get; set; }
    }
}