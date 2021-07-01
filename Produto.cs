using System;
using System.Collections.Generic;
using System.Data;
using Mono.Data.Sqlite;

public class Produto
{
  
  private int _id;
  private string _nm;
  private double _pc;

  public int Id
  {
    get {return _id;}
  }
  public string Nm
  {
    get {return _nm;}
  }
  public double _pc
  {
    get {return _pc;}
  }

  public Produto(string nm, double pc)
  {
    this._nome = nm ;
    this._preco = pc;
  }
  public void Imprimir()
  {
    Console.WriteLine("ID:\t\t\t{0}", this._id);
    Console.WriteLine("Produto:\t{0}", this._nm);
    Console.WriteLine("Preço:\t\tR$ {0:0.00}\n",
    this._pc);
  }

  public void Persistir()
  {
    using (var connection = new SqliteConnection("Data Source=banco.db"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        INSERT INTO produto (nm, pc)
        VALUES ($nome, $preco);
      ";
      command.Parameters.AddWithValue("$nome", this._nm);
      command.Parameters.AddWithValue("$preco", this._pc);

      command.ExecuteNonQuery();
    }
  }

  public static List<Produto> ConsultarProdutos()
  {
    List<Produto> produtos = new List<Produto>();

    using (var connection = new SqliteConnection("Data Source=banco.db"))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText =
      @"
        SELECT id, nm, pc
        FROM produto;
      ";

      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var id = reader.GetInt32(0);
          var nm = reader.GetString(1);
          var pc = reader.GetDouble(2);

          Produto p = new Produto(nm, pc);
          p._id = id;

          produtos.Add(p);
        }
      }
    }

    return produtos;
  }
  public static List<Produto> ConsultarProdutos(string produtoEscolhido)
  {
    List<Produto> produtos = new List<Produto>();

    using (var connection = new SqliteConnection("Data Source=banco.db"))
    {
      connection.Open();
     
      var command = connection.CreateCommand();
      command.CommandText = @"SELECT * FROM produto  
      WHERE produto.nm LIKE ($produtoEscolhido);";
      command.Parameters.AddWithValue("$produtoEscolhido",produtoEscolhido);
      using (var reader = command.ExecuteReader())
      {
        while (reader.Read())
        {
          var id = reader.GetInt32(0);
          var nm = reader.GetString(1);
          var pc = reader.GetDouble(2);

          Produto p = new Produto(nm, pc);
          p._id = id;

          produtos.Add(p);
        }
      }
    }

    return produtos;
  }
}