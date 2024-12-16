
# Schedule App

## Descrição
Schedule App é uma aplicação desenvolvida em .NET com o objetivo de gerenciar tarefas e compromissos diários. A aplicação envia e-mails automáticos para os usuários confirmados contendo suas tarefas agendadas para o dia.

## Tecnologias Utilizadas

- **.NET 7**
- **Entity Framework Core** (EF Core) para acesso ao banco de dados
- **PostgreSQL** como banco de dados
- **Quartz.NET** para agendamento de tarefas
- **JWT (JSON Web Token)** para autenticação segura
- **SMTP** para envio de e-mails
- **Docker** para conteinerização

## Recursos Principais

- Gerenciamento de usuários e confirmação por e-mail
- Agendamento automático de tarefas diárias
- Envio de e-mails utilizando SMTP
- Integração com banco de dados via EF Core
- Job agendado às 06:00 AM diariamente utilizando Quartz.NET
- API RESTful para gerenciar dados

## Configuração e Execução

### 1. Configurar as Variáveis de Ambiente
Crie um arquivo `.env` na raiz do projeto com os seguintes valores:


```env
# Smpt config
SMTP_HOST=seu-smtp.server.com
SMTP_PORT=sua-porta-smtp
SMTP_EMAIL=seu-email@gmail.com
SMTP_PASSWORD=sua-senha

# Jwt
KEY_JWT=sua-key-jwt

# Site Url
BASE_URI=sua-base-uri

# Database
DATABASE=sua-connection-string
```

### 2. Configurar o Banco de Dados
1. Certifique-se de que o PostgreSQL esteja em execução.
2. Aplique as migrações do EF Core para criar o esquema do banco de dados:
   ```bash
   dotnet ef database update
   ```

### 3. Executar o Projeto
1. Inicie o projeto com o comando:
   ```bash
   dotnet run
   ```
2. A aplicação estará disponível em `http://localhost:5206`.

## Estrutura do Projeto

- **Controllers/**: Contém os controladores da API RESTful.
- **Services/**: Contém os serviços responsáveis por regras de negócio e comunicação externa (ex.: envio de e-mails).
- **Models/**: Define as classes de modelo utilizadas pela aplicação.
- **Entities/**: Contém as entidades do EF Core que representam as tabelas do banco de dados.
- **Job/**: Contém o job agendado para envio de e-mails utilizando Quartz.NET.

## Configuração do Quartz.NET
O job de envio de e-mails é configurado no `Program.cs`:

```csharp
builder.Services.AddQuartz(qtz =>
{
    qtz.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SendScheduleJob");

    qtz.AddJob<JobSchedule>(options => options.WithIdentity(jobKey));

    qtz.AddTrigger(options => options
        .ForJob(jobKey)
        .WithIdentity("SendScheduleTrigger")
        .StartNow()
        .WithCronSchedule("0 0 6 * * ?") // Executa às 06:00 AM
    );
});

builder.Services.AddQuartzHostedService(qtz => qtz.WaitForJobsToComplete = true);
```

## APIs Disponíveis

### Autenticação
- **POST /api/login**: Realiza login e retorna um token JWT.
- **POST /api/register**: Registra um novo usuário.

### Tarefas
- **GET /api/**: Lista todas as tarefas do usuário autenticado.
- **POST /api/**: Cria uma nova tarefa.
- **GET /api/{id}**: Lista uma tarefa por ID.
- **PUT /api/{id}**: Atualiza uma tarefa.
- **DELETE /api/{id}**: Deleta uma tarefa.


## Dockerização
1. Crie a imagem Docker:
   ```bash
   docker build -t schedule-app .
   ```
2. Execute o contêiner:
   ```bash
   docker run -p 5000:5000 --env-file .env schedule-app
   ```
3. Acesse a aplicação em `http://localhost:5000`.

## Contribuições
Contribuições são bem-vindas! Sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Licença
Este projeto está licenciado sob a licença MIT.

