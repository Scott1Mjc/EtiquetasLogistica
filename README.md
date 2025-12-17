# EtiquetasLogistica

Aplicação desktop em **C# (WPF)** voltada para a geração e impressão de etiquetas em ambiente logístico.

O projeto surgiu a partir de uma necessidade prática observada no dia a dia: a impressão manual de etiquetas  
por volume, processo repetitivo e suscetível a erros, principalmente quando a carga possui muitos volumes.

---

## Objetivo

Simplificar e automatizar a criação de etiquetas, reduzindo o trabalho manual e garantindo mais clareza visual na identificação das cargas.

---

## Funcionalidades

Atualmente, o sistema conta com:

- Interface simples e direta
- Entrada dos dados principais da carga:
  - Destinatário
  - Nota fiscal
  - Quantidade de volumes
- Preview da etiqueta em tempo real
- Ajuste automático do tamanho da fonte do destinatário para melhor aproveitamento do espaço
- Controle incremental de volumes (ex: 1/8, 2/8, 3/8…)
- Estrutura inicial preparada para impressão sequencial

---

## Layout da Etiqueta

- Dimensão padrão: **10cm x 7cm**
- Impressora alvo: **Zebra GC420t**
- Informações exibidas:
  - Destinatário em destaque
  - Nota fiscal
  - Volume atual e total

---

## Tecnologias

- C#
- .NET
- WPF / XAML
- Visual Studio Community

---

## Próximos Passos

- Implementar a impressão direta na GC420t
- Gerar comandos de impressão (ZPL / EPL)
- Criar histórico de impressões
- Refinar validações e ajustes visuais

---

## Contexto

Este projeto foi desenvolvido com foco em aprendizado e aplicação prática, simulando uma demanda real de um ambiente logístico e explorando boas práticas de organização de interface e lógica de negócio.

---

## Autor

Gustavo Felizardo
