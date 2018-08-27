# Agenda NUINF


### Processo de Deploy

##### Passo 1

Fazer um publish dos projetos API e WebView

##### Passo 2

**Instalar o IIS**

Em Recursos do Windows selecionar o recurso "Serviços de Informações da Internet". 
Necessário também selecionar as opções "ASP.NET 4.7" e "Extensibilidade .NET 4.7" (ambas ficam dentro de "Recursos de Desenvolvimento de Aplicativos")

**Configurar os sites no IIS**

É necessário criar 2 sites para os projetos API e View.
Para criar siga as seguintes instuções: 
- Em Sites clique com o botão direito na opção "Adicionar site". 
- Na opção "Nome do Site" informar "API". 
- Na opção "caminho Fisico" indicar uma pasta (Ex. C:\Inetpub\API) ou outra pasta da sua escolha
- Na opção Porta indicar um valor da sua escolha. 

*Repita o processo para criar o site View em outra porta.*

#### Passo 3
Copiar as pastas publicadas de WebView e API para dentro das pastas configuradas nos sites. 

#### Passo 4
Criar um banco SQLite e colocar na pasta da sua escolha (Ex. agenda.db). 

#### Passo 5
**Realizar duas configurações**

* No arquivo web.config mais externo do site API, procurar pela chave Banco.Arquivo e colocar o caminho do arquivo SQLite criado
* No arquivo web.config mais externo do site WebView, procurar pela chave Url.API e preencher com o endereço web do site API

API => `<add key="Banco.Arquivo" value="caminho/diretorio/agenda.db" />`

WebView => `<add key="URL.API" value="http://localhost:81/api" />`
 
 #### Passo 6
 Reiniciar os sites API  e WebView aatravés do Gerenciador de Serviço de Informações (IIS)
