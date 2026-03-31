# 🏢 Sistema de Gestão de Funcionários (RH)

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET 8](https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-07405E?style=for-the-badge&logo=sqlite&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-339933?style=for-the-badge&logo=entity-framework&logoColor=white)

---

## 📋 Identificação do Projeto
* **Título do projeto:** Sistema de Gestão de Funcionários (RH)
* **Integrantes:**
  * [Luiz Henrique Magnagnagno](https://github.com/lumagno)
  * [Matheus Müller dos Santos](https://github.com/pacamole)
* **Curso:** Análise e Desenvolvimento de Sistemas (ADS) - Tópicos Especiais de Sistemas
* **Turma:** 2208230

---

## 📖 Resumo

**gestao-funcionarios**

Plataforma de gestão de RH desenvolvida para trabalho acadêmico. O sistema permite o controle administrativo de usuários, setores, cargos e funcionários, integrando a estrutura organizacional em uma interface unificada.

---

## 🏗️ Arquitetura e Modelagem

Nossa arquitetura foi desenhada com foco em simplicidade, performance e clareza, adotando a abordagem *API-First*. 

### 1. Visão de Contêiner (C4 Model)
A aplicação foca estritamente no processamento de *Back-end*, recebendo requisições HTTP (via Swagger ou Postman) e interagindo com um banco de dados local via Entity Framework Core.

<img width="6635" height="4155" alt="C4 - Arquitetura Backend" src="https://github.com/user-attachments/assets/b1cf19e9-6066-4f79-b32b-c1eff9fc2dc5" />

### 2. Modelo de Entidade-Relacionamento (DER)
O banco de dados foi modelado para respeitar a cardinalidade de uma hierarquia corporativa real, contendo as 4 tabelas centrais do sistema e seus relacionamentos:

<img width="4693" height="4914" alt="DER - Gestão de Funcionários" src="https://github.com/user-attachments/assets/5d5d2b7e-7a8d-4179-864f-94eeabb95b91" />

### 3. Fluxo de Dados (Diagrama de Sequência)
Exemplo do comportamento interno da nossa Minimal API processando uma requisição de cadastro de Setor, desde o recebimento do JSON até a persistência no SQLite:

<img width="7362" height="2925" alt="Sequence Diagram - POST Setor" src="https://github.com/user-attachments/assets/b99ee25e-76f8-401f-a382-9e974a974ac5" />

---

## ✨ Funcionalidades

* 🏢 **Gestão de Setores (CRUD Completo)**
* 💼 **Gestão de Cargos (CRUD Completo com Relacionamento)**
* 👥 **Controle de Funcionários e Vínculos (CRUD Completo)**
* 🔐 **Gestão de Usuários e Acessos**
* 🧠 **Transferência e Promoção de Colaboradores (Regra de Negócio)**
* 💰 **Cálculo de Folha de Pagamento Setorial (Regra de Negócio)**

---

## 🔍 Descrição das Funcionalidades

* **Gestão de Setores:** Permite inserir, listar, atualizar e remover departamentos da empresa. Suporta a definição de um Setor Pai para mapeamento hierárquico e a designação de um funcionário responsável.
* **Gestão de Cargos:** Vinculada obrigatoriamente a um Setor pré-existente (cumprindo a regra de dependência relacional). Define o teto salarial e a nomenclatura da posição.
* **Controle de Funcionários e Vínculos:** Gerencia os dados dos colaboradores, atrelando-os a um Cargo específico. Permite diferenciar a modalidade de contratação (CLT ou PJ) e registrar a validade de contratos temporários.
* **Gestão de Usuários:** Permite o cadastro de credenciais (email e senha) e permissões no sistema. Um usuário pode ou não estar vinculado a um perfil de funcionário.
* **Transferência e Promoção de Colaboradores:** Um endpoint customizado que recebe o ID de um novo cargo e atualiza o funcionário, aplicando validações para garantir que a transição ocorra sem quebrar a integridade dos dados históricos.
* **Cálculo de Folha de Pagamento Setorial:** Endpoint analítico que consolida os dados do banco. Ele recebe o ID de um setor, busca todos os cargos atrelados a ele, identifica os funcionários ativos nestes cargos e retorna a soma total da folha salarial daquele departamento.

---

## 🚀 Como Executar o Projeto

**Pré-requisitos:**
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
* Git instalado.
* Editor de sua preferência (recomendado: Visual Studio Code ou Visual Studio 2022).

**Passo a passo:**
1. Clone este repositório:
   ```bash
   git clone [https://github.com/pacamole/gestao-funcionarios.git](https://github.com/pacamole/gestao-funcionarios.git)
   ```
2. Acesse a pasta do projeto e restaure os pacotes:
   ```bash
   cd gestao-funcionarios
   dotnet restore
   ```
3. Execute a aplicação (o banco SQLite será criado localmente):
   ```bash
   dotnet run
   ```
4. **Para testar a API:** Abra o arquivo gestao-funcionarios.http localizado na raiz do projeto diretamente no seu editor. Clique em Send Request (ou Enviar Requisição) acima de cada bloco de código para disparar as chamadas para a API rodando localmente, sem necessidade de ferramentas externas como Postman.

## 🤖 Uso de IA

Conforme diretriz da disciplina, toda a concepção arquitetural, estruturação e documentação deste projeto foram guiadas com o suporte de Inteligência Artificial, que atuou como uma mentora técnica de *System Design*.

* **Ferramenta utilizada:** Google Gemini (Versão 3.1 Pro / Advanced).
* **Forma de uso (Foco Arquitetural):** * **Design de Sistema e Decisões Técnicas:** Atribuímos à IA a *persona* de uma "Arquiteta de Sistemas Principal". Ela foi utilizada para debater padrões arquiteturais, resultando na escolha estrutural de uma arquitetura *Backend-Only* (API-First) e na adoção do padrão de *Feature Folders* (em detrimento de Controllers monolíticos), otimizando a organização da *Minimal API*.
  * **Engenharia de Dados e DER:** A IA avaliou nossos modelos visuais iniciais de banco de dados e refinou os relacionamentos. Ela nos auxiliou a aplicar o princípio *KISS* (redução de complexidade), transformando um auto-relacionamento estrito da tabela de Setores em um campo descritivo para evitar gargalos arquiteturais de referência circular no Entity Framework.
  * **Diagramação como Código:** A IA foi responsável por traduzir nossas regras de arquitetura para a linguagem *Mermaid.js*, gerando os diagramas formais (C4 Model para infraestrutura e Diagrama de Sequência para o fluxo de dados).
  * **Documentação Técnica:** Estruturação profissional do README, gerando a redação técnica do resumo e a especificação das funcionalidades.
* **Revisões e Decisões realizadas pela equipe:**
  * **Modelagem e Flexibilidade do Domínio:** Como "Tech Leads" do projeto, a equipe (Luiz e Matheus) foi inteiramente responsável por definir as entidades iniciais e a lógica de negócios. Nós tomamos a decisão da flexibilidade hierárquica do sistema, estabelecendo a cascata estrutural obrigatória (`Setores -> Cargos -> Funcionários`) e definindo que um mesmo Cargo (ex: Analista) poderia abrigar Funcionários com modalidades de contrato distintas (CLT ou PJ).
  * Atuamos ativamente na avaliação de *trade-offs* arquiteturais: vetamos sugestões iniciais da IA que propunham o desenvolvimento de *Front-end* ou a adoção de microsserviços, forçando a arquitetura a se manter rigorosamente alinhada às restrições acadêmicas (Backend enxuto em C# e banco SQLite local).
  * Inserimos a decisão arquitetural de utilizar arquivos `.http` para testes versionados no lugar de ferramentas externas, revisamos todo o texto garantindo o uso da norma culta e organizamos os artefatos visuais na pasta `/docs`.
