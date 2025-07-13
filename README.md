# Limit Manager Backend Challenge

### Uso da aplicaçåo

1 - Crie a tabela `accounts` com partition key igual a `pk` e sort key igual a `sk`
2 - Crie a tabela `transactions` com partition key igual a `pk` e sort key igual a `sk`
3 - Configure pelo CLI seus accessKey, secretKey
4 - Rode a aplicação .NET, ele irá inicializar o Swagger onde estará todas APIs
5 - Utilize o endpoint `POST /api/v1/account` para criar uma conta com suas respectivas informações (`cpf, agency, accountNumber, balance, pixLimit`)
6 - Utilize o endpoint `GET /api/v1/transaction/pix` para fazer uma transferência de PIX para testar

### Pontos a melhorar:

- Melhorar as validações dos campos e retorno dos erros (não gastei muito tempo nisso)