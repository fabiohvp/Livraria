# Livraria
[Entrevista] SoftDesign

### Ferramentas utilizadas
- Asp.Net MVC 4.8
- Entity Framework 6.4.4
- MSTest
- LocalDB
- Effort (para banco de dados em memória para os testes unitários)
- LinqKit (para permitir utilização de rules e projections reutilizáveis)
- Enflow (para criação de rules e projections reutilizáveis e workflows)

Obs.: Como eu trabalho durante o dia, tive apenas algumas horas nas noites para o desenvolvimento e por isso o método de encriptação e a injeção de dependência foram apenas simulados ou prototipados, também não foram feito teste para todo o código para que desse tempo de construir um exemplo mais completo, e por último também não foi adicionado interface por limitação de tempo (preferi dar mais importância ao backend já que a vaga é para backend developer). Mas se acharem interessante eu posso desenvolver um mvp em react ou svelte utilizando tailwindcss.


### Conceitos utilizados

- Teste unitário
- Autenticação via Token Bearer
- Padrão UnitOfWork (através dos workflows)
- Separação em camadas
- Web Api
- Code First

### Descrição das atividades
Neste teste, você deve construir uma aplicação web em .NET, conforme a descrição abaixo.
Para entregar, commite o código em um repositório publico e informe o link do repositório como resposta à esta questão.


Funcionalidades:
1) Login
- Tela para login
- Não deve ser possível acessar as outras telas sem realizar o login


2) Lista de livros
- Tela para exibir uma lista com todos os livros cadastrados, com opção para pesquisa
- Tela para exibir detalhes de um livro
- Na lista de livros permitir alugar um livro
- Não permitir alugar um livro já alugado


Premissas:
- Usar base de dados SQL Express
- Uso de padrão MVC 4.0
- Uso de C#
- Telas devem ter algum layout CSS
- Usar repositório público (Bitbucket, Github).
- Presença de testes unitários.
- Comunicação entre o frontend e o backend ser por WEBAPI

Critérios de Avaliação:
- Estruturação do código
- Validação das regras de negócio
- Presença de testes unitários.
- Uso de GIT


Prazo:
- Prazo de 3 dias após o envio desta especificação.
- Caso não consiga finalizar completamente dentro do prazo estipulado, não deixe de enviar seu código para nossa avaliação. Envie e comunique o que você desenvolveu e o que ficou faltando.

