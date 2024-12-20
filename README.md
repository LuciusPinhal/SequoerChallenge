# Sistema de Apontamento de Produção

Este repositório contém a solução completa para o desafio proposto, com funcionalidades para o front-end e back-end, incluindo CRUDs adicionais para gerenciar materiais, produtos, ordens de produção e usuários.

## Funcionalidades

### Front-End

A interface do usuário foi desenvolvida para atender os seguintes requisitos principais:

1. **Tela de Login e Cadastro do Usuário**  
   O sistema conta com uma tela de login onde o usuário pode se autenticar utilizando e-mail e senha.  
   - Caso o usuário ainda não tenha cadastro, existe a opção de se registrar criando um novo usuário com e-mail e senha.
   - Após o login bem-sucedido, o usuário tem acesso ao sistema e suas funcionalidades.

2. **Autenticação por E-mail**  
   O sistema solicita o e-mail do usuário para validação e identificação.

3. **Seleção de Ordem de Produção**  
   Exibe uma lista de Ordens de Produção, oriunda do endpoint `GetOrders`.  
   - Após a seleção, exibe o produto correspondente e sua imagem.

4. **Data de Apontamento**  
   Campo para preenchimento da data em que foi realizado o apontamento.

5. **Seleção de Material Utilizado**  
   Lista de materiais vinculados à Ordem selecionada.

6. **Quantidade Produzida**  
   Permite ao usuário informar a quantidade de produção dentro do limite permitido.

7. **Cálculo do Tempo de Ciclo**  
   O sistema calcula automaticamente o tempo decorrido entre a seleção da Ordem e o envio do apontamento.

8. **Envio de Dados**  
   O botão de envio só é habilitado se o tempo de ciclo for maior ou igual ao tempo cadastrado na Ordem.  
   Após o envio bem-sucedido, os campos são limpos.

9. **Consulta de Apontamentos**  
   Tela adicional para exibição dos apontamentos realizados, com dados retornados pelo endpoint `GetProduction`.

---

### Back-End

A API foi projetada para oferecer suporte às funcionalidades do front-end e inclui os seguintes métodos principais e suas respectivas validações:

#### Endpoints

1. **GetOrders**  
   Retorna uma lista de Ordens de Produção.  
   - **Rota:** `/api/orders/GetOrders`.

2. **GetProduction**  
   Retorna a lista de produções associadas a um e-mail.  
   - **Rota:** `/api/orders/GetProduction?email={email}`.

3. **SetProduction**  
   Recebe os dados de apontamento de produção e valida-os.  
   - **Rota:** `/api/orders/SetProduction`.

#### Validações

- **E-mail:** O e-mail informado deve constar no cadastro de usuários.
- **Ordem:** A ordem selecionada deve estar cadastrada e ativa.
- **Data de Apontamento:** Deve estar dentro do período permitido.
- **Quantidade:** A quantidade deve ser maior que 0 e menor ou igual ao limite permitido pela Ordem.
- **Material:** O material deve pertencer à lista de materiais da Ordem selecionada.
- **Tempo de Ciclo:** O tempo de ciclo deve ser maior que 0. Se menor que o cadastrado, permite o apontamento com uma mensagem informativa.

---

### Funcionalidades Adicionais

Foram adicionados CRUDs para materiais, produtos, ordens de produção e usuários, além de funcionalidades de vinculação entre materiais e produtos:

#### CRUDs

- **Materiais:**  
  - `getMaterial`, `setMaterial`, `updateMaterial`, `deleteMaterial`

- **Produtos:**  
  - `getProduct`, `setProduct`, `updateProduct`, `deleteProduct`

- **Ordens de Produção:**  
  - `getOrder`, `setOrder`, `updateOrder`, `deleteOrder`

- **Produções:**  
  - `getProduction`, `setProduction`, `updateProduction`, `deleteProduction`

- **Usuários:**  
  - `getUser`, `setUser`, `updateUser`, `deleteUser`

#### Vinculação entre Materiais e Produtos

- **`getProductInMaterial`**  
- **`getMaterialInProduct`**

---

## Tela de Login e Cadastro de Usuário

O sistema possui uma tela de login e uma tela de cadastro, que permite ao usuário se autenticar ou criar uma nova conta. As funcionalidades incluem:

1. **Tela de Login:**  
   - O usuário insere seu e-mail.
   - Caso a autenticação seja bem-sucedida, o usuário é redirecionado para a tela principal do sistema.
   - Se os dados estiverem incorretos, uma mensagem de erro será exibida.

2. **Tela de Cadastro:**  
   - O usuário pode criar uma nova conta fornecendo seu e-mail e senha.
   - Após o cadastro, o usuário pode realizar o login e acessar o sistema.

---

## Tecnologias Utilizadas

### Front-End

- **Framework:** Windows Forms  
- **Biblioteca:** System.Net.Http para comunicação com a API  
- **Interface:** Controles nativos do Windows Forms (como DataGridView, ComboBox, TextBox)

### Back-End

- **Linguagem:** C# (.NET Core)  
- **Estrutura:** Web API  
- **Banco de Dados:** SQL Server  
- **Validações:** Validação robusta implementada diretamente nos endpoints.

---

## Como Executar

### Back-End

1. Configure a string de conexão no arquivo `appsettings.json`.
2. Compile e inicie o projeto.
3. Acesse a API nos endpoints especificados.

### Front-End

1. Configure a URL base da API no código (variável `apiUrl`).
2. Compile e inicie o projeto no Visual Studio.

---

## Melhorias Futuras

1. **Implementação de Autenticação e Autorização:** Melhorar a segurança do sistema.
2. **Filtro em lisa:** Adicionar funcionalidades de filtro nas listas de materiais e produtos relacionados, permitindo uma busca mais eficiente e rápida.
3. **Ajuste de Layout:** Melhorar o alinhamento e o posicionamento dos componentes da interface para garantir uma experiência mais fluida e visualmente agradável ao usuário.
4. **Melhoria nas Mensagens de Erro** Refinar as mensagens de erro, tornando-as mais claras e informativas para facilitar a compreensão e a resolução de problemas pelo usuário.

---

Este sistema foi projetado para ser escalável e atender tanto os requisitos do desafio quanto necessidades reais em cenários industriais. 😊
