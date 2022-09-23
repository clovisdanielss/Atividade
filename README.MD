### Atividade - Criar uma API de Vendas. 📚💰💰

Você pode acessar a API [aqui](https://atividade-jyphyscbcq-rj.a.run.app/swagger). 

A API deve implementar as seguintes operações:
- Cadastrar uma venda.
- Pegar uma venda por Id.
- Realizar transição de estado de uma venda. 

Uma venda deve conter:
- O vendedor (id, cpf, nome).
- A data da venda.
- O pedido (carrinho).
- O estado em que a venda se encontra.
- Um identificador

A transição de estado deve obedecer as seguintes regras:

> Aguardando pagamento --> Pagamento Aprovado --> Enviado --> Entregue

> Aguardando pagamento --> Cancelado

> Aguardando pagamento --> Pagamento Aprovado --> Cancelado
