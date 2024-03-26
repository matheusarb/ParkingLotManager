<h1>Gerenciador de Estacionamento v1</h1>

<img src="https://i.imgur.com/ektThjj.png" width="640px" height="480px" alt="logo_do_Gerenciador_de_Estacionamento"></img>

<hr>

![GitHub repo size](https://img.shields.io/github/repo-size/matheusarb/ParkingLotManager?style=for-the-badge)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

> <b>Gerenciador de Estacionamento v1</b> é um projeto backend em REST API que simula o funcionamento de um gerenciador de estacionamento na vida real.


## 💻 Requisitos
Para instalar o <b>Gerenciador de Estacionamento v1</b> é necessário o .NET Core 8.0.X instalado em sua máquina. Você pode obtê-lo <a href="https://dotnet.microsoft.com/en-us/download/dotnet">aqui</a>
<br>
Após clonar o projeto, execute os seguintes comandos dentro da pasta raíz do .csproj:
```
dotnet restore
```
```
dotnet build
```
```
dotnet run
```
Você também pode executar esses comandos através de sua IDE preferida (recomendamos o uso do Visual Studio 2022)

## :star: Funcionalidades
+ Operações CRUD
+ Serviços de autenticação e autorização utilizando o protocolo RBAC(Controle de Acesso baseado em funções) e chave API
+ Testes de unidade com XUnit e Moq
+ Banco de dados com SqlServer e EntityFramework Core
+ API completamente documentada
+ Métodos de extensão customizados
+ Integração de API com a Report.Api para gerar relatórios sobre o fluxo de veículos
+ Script SQL para facilitar a população do banco de dados SQL
+ Coleção no Postman(arquivo JSON na pasta raíz) já organizada para teste dos endpoints

## :bulb: Dicas sobre como usar
* O propósito desse projeto é simular o backend do fluxo de trabalho de um gerenciador de estacionamento através de requisições a uma API
* ParkingLotManager.WebApi é integralmente documentada e você encontrará todas as informações necessárias sobre os endpoints e o que cada um faz
* Crie e gerencie o seu banco de dados através das _migrations_
   * Execute os seguintes comandos:
   ```
   dotnet ef migrations add InitialCreate
   ```
   ```
   dotnet ef database update
   ```
* Você pode executar o script.sql(Pasta raíz) para adicionar registros ao seu banco de dados
* RBAC está configurado para ser usado no endpoint GetFordBrand(Vehicles/GetFordBrand). Primeiro, você precisa criar um admin(Account/CreateAdmin) e depois logar no sistema(Account/Login). Ao logar no sistema será gerado um Bearar Token que precisa ser passado na requisição ao endpoint.
  * Passe o token na tab "Authorization" do Postman. Selecione o tipo "Bearer Token"
  * Antes de enviar a requisição, certifique-se de ter pelo menos um veículo cadastrado de marca "Ford"
  * ![image](https://github.com/matheusarb/ParkingLotManager/assets/89713533/a46526e9-d382-488d-a538-86d3d4d9db9d)
* Configure sua ConnectionString dentro do arquivo appsettings.Development.json

