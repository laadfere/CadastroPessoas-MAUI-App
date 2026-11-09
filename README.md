# 📱 CadastroPessoas-Maui

Aplicativo mobile desenvolvido em **.NET MAUI** utilizando arquitetura em camadas (**MVVM / DAL**) com persistência de dados local assíncrona via **SQLite**.

O projeto permite o gerenciamento completo de registos de pessoas (CRUD), oferecendo uma interface reativa, busca em tempo real e suporte a múltiplos campos de contacto.

---

## 🚀 Funcionalidades

- **Gerenciamento Completo (CRUD):**
  - **Inclusão:** Cadastro de pessoas com nome, idade e e-mail.
  - **Listagem:** Visualização reativa com indicador de carregamento (*pull-to-refresh*).
  - **Alteração:** Edição de dados cadastrados.
  - **Exclusão:** Remoção de registos com diálogo de confirmação.
- **Busca em Tempo Real:** Filtragem dinâmica de registos diretamente na barra de pesquisa (`SearchBar`).
- **Persistência de Dados Local:** Armazenamento seguro de dados offline no dispositivo SQLite de forma assíncrona.

---

## 🛠️ Tecnologias e Arquitetura

- **Linguagem & Framework:** C# | .NET MAUI (Cross-platform para Windows/Android/iOS)
- **Banco de Dados:** SQLite (`sqlite-net-pcl`)
- **Arquitetura:** 3-Tier / MVVM (Model, DAL - Data Access Layer, Views)
- **Manipulação de Ficheiros:** `System.IO` e `FileSystem.AppDataDirectory` para abstração de caminhos multiplataforma
- **Reatividade na UI:** Utilização de `ObservableCollection` para sincronização em tempo real entre dados e a interface gráfica.

---

## 📂 Estrutura do Projeto

```text
CadastroPessoas-Maui/
├── Model/
│   └── Pessoa.cs              # Entidade do modelo de dados
├── DAL/
│   └── crudPessoaSQLite.cs    # Camada de acesso ao banco SQLite (Assíncrona)
├── Views/
│   ├── TelaListaPessoa.xaml   # Interface gráfica da lista e busca
│   └── TelaIncluirPessoa.xaml # Formulário de criação de registos
└── App.xaml.cs                # Inicialização global do banco de dados e navegação
