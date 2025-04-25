# Windows Service - .NET Framework 4.7.2

Este projeto demonstra como criar e configurar um serviço do Windows utilizando o .NET Framework 4.7.2.

## Iniciando projeto do zero

### Create a new project -> Windows Service (.NET Framework)

## Passo 1
![Passo 1](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo3.png?raw=true)

**Descrição:**  
Tela de criação do novo projeto no Visual Studio.  
Aqui você deve selecionar **"Windows Service (.NET Framework)"**.  
É importante garantir que o Framework selecionado seja o **.NET Framework 4.7.2**. Clique em “Next” para prosseguir.

## Passo 2
![Passo 2](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo1.png?raw=true)

**Descrição:**  
Tela de nomeação e localização do projeto.  
Insira um nome para seu serviço (por exemplo, `MeuServicoWindows`) e escolha um diretório para salvar o projeto.  
Clique em “Create”.

## Passo 3
![Passo 3](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo4.png?raw=true)

**Descrição:**  
Visualização da estrutura inicial do projeto.  
O Visual Studio cria automaticamente o arquivo `Service1.cs`, que representa o seu serviço.  
Clique duas vezes sobre o arquivo para abrir o designer.

## Passo 4
![Passo 4](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo5.png?raw=true)

**Descrição:**  
Tela do designer do serviço.  
Clique com o botão direito sobre o fundo cinza e selecione **"Add Installer"**.  
Isso cria os arquivos `ProjectInstaller.cs` e adiciona componentes que permitem a instalação do serviço.

## Passo 5
![Passo 5](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo6.png?raw=true)

**Descrição:**  
Visualização do `ProjectInstaller.cs`.  
Nesta etapa são adicionados os componentes `ServiceProcessInstaller` e `ServiceInstaller`.  
É possível configurar o tipo de conta e o nome do serviço nesta interface.

## Passo 6
![Passo 6](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo6.1.png?raw=true)

**Descrição:**  
Configuração dos componentes do instalador.  
- **ServiceName:** Defina o nome do seu serviço (ex: `MeuServicoWindows`).
- **StartType:** Escolha se o serviço será iniciado automaticamente.
- **Account:** Defina a conta que será usada para executar o serviço (`LocalSystem`, `NetworkService`, etc).

## Passo 7
![Passo 7](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo7.png?raw=true)

**Descrição:**  
Código do método `OnStart()`.  
É aqui que você define o que será executado quando o serviço for iniciado.  
Você pode iniciar timers, tarefas em segundo plano, leitura de arquivos, etc.

## Passo 8
![Passo 8](https://github.com/DanielSCaldeira/ServicoWindows/blob/main/Service/Service/imagen/passo8.png?raw=true)

**Descrição:**  
Compilação do projeto em modo Release.  
Após configurar o serviço, selecione a opção **Release** e compile o projeto.  
Os arquivos gerados estarão na pasta `bin\Release`.

---

## Instalação do Serviço

1. Abra a pasta `bin\Release` do seu projeto.
2. Copie os arquivos gerados para uma nova pasta (por exemplo, `C:\MeuServicoWindows`).
3. Abra o Prompt de Comando (CMD) como Administrador.
4. Navegue até o diretório do .NET Framework:
   ```cmd
   cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319
