

# Projeto de Sistema de Reserva de Veículos

Este projeto é um sistema de reserva de veículos que permite que usuários registrem-se, façam login e reservem veículos disponíveis para locação. Ele possui autenticação por cookies e sessões, além de gerenciamento de reservas com verificação de disponibilidade de veículos.

## Tecnologias Utilizadas

- **Linguagem de Programação:** C#
- **Framework:** ASP.NET Core 8.0
- **Banco de Dados:** MySQL
- **ORM:** Entity Framework Core
- **Autenticação:** Cookies
- **Gerenciamento de Sessão:** ASP.NET Core Session
- **Estilização:** CSS para estilizar as páginas de Login, Registro e Reservas.

### Pacotes e Bibliotecas Utilizadas

- **Microsoft.AspNetCore.Authentication.Cookies:** Para autenticação baseada em cookies.
- **Microsoft.AspNetCore.Session:** Para gerenciamento de sessões.
- **Microsoft.EntityFrameworkCore:** Para mapeamento objeto-relacional (ORM) com o Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Design:** Para ferramentas de design usadas durante o desenvolvimento com o Entity Framework.
- **Pomelo.EntityFrameworkCore.MySql:** Para integração do Entity Framework Core com o banco de dados MySQL.
  
## Funcionalidades

### 1. Autenticação de Usuário
- Os usuários podem criar uma conta, fazer login e logout.
- A autenticação é baseada em cookies e a sessão do usuário é gerenciada com ASP.NET Core Session.
- Apenas usuários autenticados podem realizar reservas.

### 2. Reservas de Veículos
- Usuários podem ver a lista de veículos disponíveis para reserva.
- Reservas só podem ser feitas para datas futuras e a data de início da reserva não pode ser posterior à data de término.
- Um veículo só pode ser reservado por um usuário em uma data específica. Se o veículo já estiver reservado no período escolhido, o sistema informa quando ele estará disponível novamente.
- O sistema impede que o mesmo usuário tenha mais de uma reserva ativa.
- Caso o usuário possua pendências financeiras, ele não pode realizar uma nova reserva.

### 3. Gestão de Erros e Validações
- O sistema exibe mensagens de erro personalizadas quando ocorre algum problema na tentativa de reservar um veículo, como:
  - **Veículo indisponível:** Exibe a data de disponibilidade do veículo.
  - **Reserva ativa:** Informa que o usuário já possui uma reserva ativa.
  - **Pendências financeiras:** Informa ao usuário sobre pendências financeiras.

### 4. Layout Responsivo e Estilizado
- O sistema possui um layout responsivo e estilizado utilizando CSS, oferecendo uma interface amigável para login, registro e reserva de veículos.

## Configuração do Projeto

### 1. Clonar o repositório
```bash
git clone https://github.com/usuario/projeto-reserva-veiculos.git
```

### 2. Configurar a string de conexão do MySQL
No arquivo `appsettings.json`, configure a string de conexão para o seu banco de dados MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LoginDB;User=SeuUsuário;Password=SuaSenha;"
  }
}
```

### 3. Migrar e atualizar o banco de dados
Execute as migrações do Entity Framework para garantir que o banco de dados esteja atualizado.

```bash
dotnet tool install --global dotnet-ef
```

```bash
dotnet ef database update
```

### 4. Executar o projeto
Após configurar o banco de dados e as dependências, execute o projeto localmente.

```bash
dotnet run
```

O projeto será iniciado e estará disponível em `http://localhost:5000`.

## Rotas Principais

- **/Account/Login**: Página de login do usuário.
- **/Account/Register**: Página de registro de novos usuários.
- **/Reserva/Reservar**: Página para os usuários autenticados visualizarem os veículos e realizarem uma reserva.
- **/Account/Logout**: Realiza o logout e redireciona para a página de login.

## Melhorias Futuras

- Implementação de um painel administrativo para gerenciar usuários e reservas.
- Envio de notificações por e-mail para confirmações de reservas.
- Adição de filtros avançados para pesquisa de veículos por características.

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir um *issue* ou enviar um *pull request* com melhorias ou correções.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

