
# Questor API - Avaliação

API para gerenciamento de bancos, juros e boletos, desenvolvida com ASP.NET Core 6 usando Minimal API e Entity Framework Core com PostgreSQL.

---

## 📚 Tecnologias Utilizadas

* ASP.NET Core 6 (Minimal API)
* Entity Framework Core
* PostgreSQL
* Swashbuckle (Swagger)
* C# 6
* Microsoft Visual Studio 2022

---

## 🚀 Funcionalidades

* Consultar todos os bancos (GET)
* Consultar banco por ID (GET)
* Criar banco (POST)
* Consultar boletos à partir de ID (GET)
* Consultar todos os boletos (GET)
* Criar boleto (POST)
* Documentação da API via Swagger

---

## 🔧 Configuração do Ambiente

1. **Clone o repositório (COM SSH e HTTPS):**

```bash
git clone git@github.com:DanielAlmeidaSC/ProjetoQuestor.git 
ou
git clone https://github.com/DanielAlmeidaSC/ProjetoQuestor.git
```

2. **Configure o banco de dados:**

* Tenha o PostgreSQL instalado e rodando
* Crie um banco de dados para o projeto
* Configure a string de conexão no `appsettings.json` ou `appsettings.Development.json`:

```json
{
  "database": {
    "PostgreSQL": "Host=localhost;Port=5432;Database=nome_do_banco;Username=usuario;Password=senha"
  }
}
```

3. **Rode as migrations:**

```bash
dotnet ef database update
```

---

## ▶️ Como executar

Na pasta do projeto, rode:

```bash
dotnet run
```

A aplicação iniciará e estará disponível em:

```
http://localhost:7006/
```

---

## 📄 Documentação

A documentação interativa da API está disponível via Swagger UI em:

```
http://localhost:7006/swagger
```

---

## 📦 Estrutura do Projeto

* **Endpoints:** Classes que definem os caminhos, métodos HTTP e comportamentos da API
* **Database:** Contém o contexto e configurações do Entity Framework
* **Entities:** Classes modelo das tabelas do banco
* **Program.cs:** Configuração principal da aplicação, registro de serviços e rotas

---

## 📞 Contato

Para dúvidas ou sugestões, envie um email para: \[[danielexpeditocomercial@gmail.com](mailto:danielexpeditocomercial@gmail.com)]

---

## 📄 Licença

Este projeto está licenciado sob a MIT License.

---
