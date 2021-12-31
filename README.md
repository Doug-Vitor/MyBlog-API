<h1 align="center">Meu blog (API)</h1>
<h4 align="center">Aplicação responsável pela persistência e pelo acesso a dados do MyBlog (confira <a href="https://github.com/Doug-Vitor/MyBlog-Web">aqui</a>).</h3>

<br/>
<p>:warning: É interessante utilizar a <a href="https://github.com/Doug-Vitor/MyBlog-Web">esta Aplicação Web</a> (cliente dessa API) para um melhor proveito deste projeto. Certifique-se de ler os requisitos deste projeto e do projeto citado antes de testar.<p>

<br/>
<h3>:computer: Tecnologias utilizadas:</h3>
<h4>
 <ul>
  <li>DotNET 6.0</li>
  <li>SQL Server</li>
  <li>Entity Framework Core</li>
  </ul>
</h4>

<br/>
<h3>:wrench: Quer rodar o projeto? Siga os passos:</h3>
<h4>
 <ul><li>É necessário instalar o Visual Studio 2022 ou Visual Studio Code e SQL Server</li></ul>
 
 <br/>
 <ol>
  <li>Faça o download ou clone o projeto.</li>
  <li>Abra o arquivo de solução chamado MyBlog.sln</li>
  <li>No arquivo appsettings.json (projeto MyBlog.Application), altere os endereços de conexão na chave "ConnectionStrings" para sua conexão local. Queira utilizar:
   <blockquote>
    "ConnectionStrings": { 
     <p>"AccountsDb": "Server=NomeDoSeuServidor;DATABASE=Accounts;Trusted_Connection=True;MultipleActiveResultSets=True"</p>
     <p>"BlogDb": "Server=NomeDoSeuServidor;DATABASE=Blog;Trusted_Connection=True;MultipleActiveResultSets=True"</p>
    }
   </blockquote>
  </li>
  <li>Restaure os pacotes NuGet da solução:
   <ul>
    <p>Pelo CLI: <blockquote>dotnet restore</blockquote></p>
    <p>Pelo CLI do NuGet: <blockquote>nuget restore MyBlog.sln</blockquote></p>
   </ul>
  </li>
  
  <li>Abra o Console de Gerenciador de Pacotes do Nuget e execute os seguintes comandos para criar e restaurar as tabelas dos bancos de dados:
  <blockquote>Update-Database -Project src\MyBlog.Infrastructure -StartupProject src\MyBlog.Application -Context AuthenticationContext</blockquote>
  <blockquote>Update-Database -Project src\MyBlog.CoreInfrastructure -StartupProject src\MyBlog.Application -Context CoreContext</blockquote>
  </li>
 </ol>
</h4>

<br/>
<h3>O que aprendi neste projeto:</h3>
<h4>
 <ul>
  <li>Autenticação e autorização do usuário com Bearer, JWT e sem frameworks.</li>
  <li>Validações customizadas utilizando Regex e atributos personalizados.</li>
  <li>Tratamento global de erros.</li>
 </ul>
</h4>

<br/>
<h3>Referências:</h3>
<ul>
  <li>Autenticação e autorização com Bearer e JWT: <a href="https://balta.io/blog/aspnet-5-autenticacao-autorizacao-bearer-jwt">Clique aqui!</a> | <a href="https://www.youtube.com/watch?v=vAUXU0YIWlU">Clique aqui!</a></li>
  <li>Validações com Regex<a href="https://stackoverflow.com/questions/34715501/validating-password-using-regex-c-sharp/43085890">Clique aqui!</a></li>
  <li>Validações customizadas com atributos personalizados<a href="https://www.youtube.com/watch?v=kgzc_gw2pi8">Clique aqui!</a></li>
  <li>Tratamento global de erros e exceções<a href="https://henriquemauri.net/tratamento-global-erros-no-net-6/">Clique aqui!</a></li>
</ul>
