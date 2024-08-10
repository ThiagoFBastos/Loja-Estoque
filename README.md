# API de controle de estoque
API de controle de estoque para cadastro de Lojas (Store), Produtos (Products) e Items de Estoque (StockItem).

## Entidades
- Product: informações de produto (id, nome e preço)
- Store: Informações sobre a loja (id, nome e endereço)
- StockItem: Deve relacionar uma loja a um produto e armazenar a quantidade de itens em estoque

## Operações
- Product: Criar, Alterar, Deletar, Obter um produto, Obter todos os produtos.
- Store: Criar, Alterar, Deletar, Obter uma loja, Obter todas as lojas.
- StockItem: Criar, Deletar, Obter um item de estoque, Adicionar items, Remover items.

## Requisitos
- .NET 8
- Postgres

## Instruções
1. Edite a senha na string de conexão (DefaultConnection) que está em **API/appsettings.json**
2. Baixe o comando dotnet ef: **dotnet tool update --global dotnet-ef**
3. Execute o comando para criar a migração: **dotnet ef --project Persistence --startup-project API migrations add "LojaEstoque-Database-Init" --context RepositoryContext**
4. Execute o comando para executar a migração: **dotnet ef --project Persistence --startup-project API database update --context RepositoryContext**
5. Execute o comando para criar a API: **dot net run --project API**
6. Visite **https://localhost:5001/swagger/index.html**

## Testes
Execute **dotnet test** UnitTest para rodar os testes unitários