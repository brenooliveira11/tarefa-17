using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;
//nome: breno oliveira//
//turma:ds204//

class MainClass {

  public static void Cadastrar(){
    Console.WriteLine("\n\nCadastro de Produto");
    Console.Write("Preço: ");
    double preco = Convert.ToDouble(Console.ReadLine());
    Console.Write("Nome: ");
    string nome = Console.ReadLine();
    Produto p = new Produto(preco, nome);
    p.Persistir();
  }

  public static void Listar()
  {
    Console.WriteLine("\n");
    List<Produto> produtos = Produto.ConsultarProdutos();
    foreach(var produto in produtos)
    {
      produto.Imprimir();
    }
  }
  public static void Consultar()
  {
    Console.WriteLine("\n");
    Console.WriteLine("digite uma palavra chave do produto para consulta");
    string produtoEscolhido=Console.ReadLine();
    List<Produto> produtos = Produto.ConsultarProdutos("%"+produtoEscolhido+"%");
    foreach(var produto in produtos)
    {
      produto.Imprimir();
    }
  }

  public static void Menu(){
    char opcao;
    do {
      Console.WriteLine("\n Opções: [CA]dastrar [LI]star [PE]squisar [SA]ir");
      opcao = Char.ToLower(Console.ReadKey().KeyChar);
      switch (opcao)
      {
        case "CA":
          Cadastrar();
          break;

        case "li":
          Listar();
          break;

         case "PE":
          Consultar();
          break;

        case "SA":
          break;

        default:
          Console.WriteLine("Opção Inválida!");
          break;
      } 
    } while (opcao != "SA");
  }
  public static void Main (string[] args) {
    Console.WriteLine("Seja bem vindo ao cadastro de produtos!\n");
    Menu();
    Console.WriteLine("\nAté breve!");
  }
}