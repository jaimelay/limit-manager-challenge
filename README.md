# Limit Manager Challenge

---

## Descrição

O projeto é uma API que permite:

- Criar uma conta com cpf, agência, numero da conta, saldo e limite de pix
- Consultar dados da conta e limite
- Remover conta
- Ajustar limites (por exemplo aumentar ou diminuir)  
- Realizar transações entre contas

---

## Tecnologias Usadas

- .NET 8.0
- ASP.NET Core Minimal API 
- Swagger / OpenAPI (para documentação interativa)
- XUnit / NSubstitute
- DynamoDb

## Arquitetura, Padrões e Principios

- Clean Architecture
- DDD
- Result Pattern
- Dependency Injection
- Repository
- SOLID
- RESTful API

## Estrutura do Projeto

```
/src
  /LimitManager.API
  /LimitManager.Application
  /LimitManager.Domain
  /LimitManager.Infrastructure
  /LimitManager.Shared
/tests
  /LimitManager.Tests
```
---

## Uso da aplicação

1. Configure seu Amazon DynamoDb
    1. Crie a tabela `accounts` com partition key igual a `pk` e sort key igual a `sk`
    2. Crie a tabela `transactions` com partition key igual a `pk` e sort key igual a `sk`
    3. Configure pelo CLI seus accessKey, secretKey
2. Rode a aplicação .NET, a API será exposta em `http://localhost:5204` ou conforme configurado no `launchSettings.json`.
    1. Acesse: http://localhost:5204/swagger

3. Utilize o endpoint `POST /api/v1/account` para criar uma conta com suas respectivas informações (`cpf, agency, accountNumber, balance, pixLimit`)
4. Utilize o endpoint `GET /api/v1/transaction/pix` para fazer uma transferência de PIX para testar


### Endpoints

#### Conta
* **Criar conta**

```
POST /api/v1/account
```

* **Obter limite de PIX da conta**

```
GET /api/v1/account/pixlimit
```

* **Ajustar limite**

```
PUT /api/v1/account/pixlimit
```

* **Deletar contar**

```
PUT /api/v1/account
```

#### Transação

* **Fazer uma transação do tipo PIX**

```
POST /api/v1/transaction/pix
```