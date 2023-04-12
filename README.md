# Sistema de Fechamento de Ponto e Ordem de Pagamento ⏰💰

Este é um sistema de fechamento de ponto e ordem de serviço escrito em C# que apresenta uma tela na qual o usuário pode informar a pasta que contém os arquivos no formato .csv. Esses arquivos contêm as informações referentes ao ponto dos funcionários de um determinado departamento e devem seguir o seguinte formato:

## Campos do arquivo

* Código do funcionário: numério
* Nome: texto
* Valor da hora: decimal 💵
* Data do registro: data 📅
* Expediente (início e fim): hora ⏰
* Almoço (início e fim): hora 🍴

Os formatos dos campos devem seguir os seguintes padrões:

* Hora: Hh:mm ⏰
* Valor da hora: R$ dd,dd 💵
* Almoço: Hh:mm - Hh:mm 🍴

Se o formato dos campos não for respeitado, o sistema informará o usuário e solicitará se deseja começar uma nova iteração.

## Estrutura do projeto

O projeto segue o padrão de camadas, incluindo:

* Infraestrutura: biblioteca contendo as definições bases para as demais camadas.
* Serviço: biblioteca contendo as definições responsáveis pela leitura, fechamento do ponto e cálculo da ordem de serviço.
* Negócio: biblioteca que contém as definições das regras de negócio.
* ConsoleApp: camada que consome os recursos das camadas anteriores e interage com o usuário.

## Tecnologias utilizadas

* Plataforma: .NET (versão 6.0.15) 🖥️
* Linguagem: C# 🧑‍💻
* Bibliotecas externas: Newtonsoft.Json 📚
