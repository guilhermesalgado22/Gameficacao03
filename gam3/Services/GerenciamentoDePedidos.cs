using gam3.Interfaces;
using gam3.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gam3.Services
{
    public class GerenciamentoDePedidos : IPedidoRepository, IItemPedidoRepository
    {
        private readonly IPedidoRepository pedidoRepository;
       private readonly IItemPedidoRepository itemPedidoRepository;
        string connectionString = "server=localhost;database=game3;user=root;password=33551427Gui$";


        private GerenciamentoDePedidos repository;

        public GerenciamentoDePedidos(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public GerenciamentoDePedidos(GerenciamentoDePedidos repository)
        {
            this.repository = repository;
        }

        public List<Pedido> ListarPedidos(Func<Pedido, bool> predicate)
      {
          return pedidoRepository.GetPedidos(predicate).ToList();
        }

        public decimal CalcularValorTotalPedido(int id)
        {
            // Removendo a linha que busca os itens de pedido do repositório
            // var itensPedido = itemPedidoRepository.GetItemPedidosByPedidoId(id);

            // Calculando o valor total diretamente no banco de dados utilizando uma query SQL
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT SUM(PrecoUnitario * Quantidade) FROM ItensPedido WHERE PedidoId = @PedidoId;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PedidoId", id);

                    decimal valorTotal = Convert.ToDecimal(command.ExecuteScalar());

                    return valorTotal;
                }
            }
        }


        public ItemPedido CreateItemPedido(ItemPedido itemPedido)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO ItensPedido (PedidoId, Quantidade, PrecoUnitario) VALUES (@PedidoId, @Quantidade, @PrecoUnitario); SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PedidoId", itemPedido.Pedido.Id);
                    command.Parameters.AddWithValue("@Quantidade", itemPedido.Quantidade);
                    command.Parameters.AddWithValue("@PrecoUnitario", itemPedido.PrecoUnitario);

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    itemPedido.Id = id;
                }
            }

            return itemPedido;
        }


        public Pedido CreatePedido(Pedido pedido)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Pedidos (DataPedido, Cliente, Status) VALUES (@DataPedido, @Cliente, @Status); SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DataPedido", pedido.DataPedido);
                    command.Parameters.AddWithValue("@Cliente", pedido.Cliente);
                    command.Parameters.AddWithValue("@Status", pedido.Status);

                    int id = Convert.ToInt32(command.ExecuteScalar());

                    pedido.Id = id;
                }
            }

            return pedido;
        }


public void DeleteItemPedido(int id)
{
    using (var connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        string query = "DELETE FROM ItensPedido WHERE Id = @Id;";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }
    }
}


        public void DeletePedido(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Pedidos WHERE Id = @Id;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }


        public ItemPedido GetItemPedido(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPedido> GetItemPedidosByPedidoId(int pedidoId)
        {
            List<ItemPedido> itensPedido = new List<ItemPedido>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM ItensPedido WHERE PedidoId = @PedidoId;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PedidoId", pedidoId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemPedido itemPedido = new ItemPedido
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Quantidade = Convert.ToInt32(reader["Quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(reader["PrecoUnitario"]),
                                Pedido = new Pedido { Id = pedidoId }
                            };

                            itensPedido.Add(itemPedido);
                        }
                    }
                }
            }

            return itensPedido;
        }


        public Pedido GetPedido(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Pedidos WHERE Id = @Id;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Pedido pedido = new Pedido
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                DataPedido = Convert.ToDateTime(reader["DataPedido"]),
                                Cliente = Convert.ToString(reader["Cliente"]),
                                Status = Convert.ToString(reader["Status"])
                            };

                            return pedido;
                        }
                    }
                }
            }

            return null; // Retornar null caso o pedido não seja encontrado
        }


        public IEnumerable<Pedido> GetPedidos(Func<Pedido, bool> predicate)
        {
            List<Pedido> pedidos = new List<Pedido>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Pedidos;";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pedido pedido = new Pedido
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                DataPedido = Convert.ToDateTime(reader["DataPedido"]),
                                Cliente = Convert.ToString(reader["Cliente"]),
                                Status = Convert.ToString(reader["Status"])
                            };

                            if (predicate(pedido))
                            {
                                pedidos.Add(pedido);
                            }
                        }
                    }
                }
            }

            return pedidos;
        }

        public ItemPedido UpdateItemPedido(ItemPedido itemPedido)
        {
            throw new NotImplementedException();
        }

        public Pedido UpdatePedido(Pedido pedido)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Pedidos SET Status = @Status WHERE Id = @Id;";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", pedido.Status);
                    command.Parameters.AddWithValue("@Id", pedido.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return pedido;
                    }
                }
            }

            return null; // Retornar null caso o pedido não seja encontrado ou a atualização não seja bem-sucedida
        }



    }
}
        //private readonly IPedidoRepository pedidoRepository;
//        private readonly IItemPedidoRepository itemPedidoRepository;

//        public GerenciamentoDePedidos(IPedidoRepository pedidoRepository, IItemPedidoRepository itemPedidoRepository)
//        {
//            this.pedidoRepository = pedidoRepository;
//            this.itemPedidoRepository = itemPedidoRepository;
//        }

//        public Pedido CriarPedido(Pedido novoPedido)
//        {
//            return pedidoRepository.CreatePedido(novoPedido);
//        }

//        public ItemPedido AdicionarItemPedido(ItemPedido novoItemPedido)
//        {
//            return itemPedidoRepository.CreateItemPedido(novoItemPedido);
//        }

//        public Pedido AtualizarStatusPedido(int id, string novoStatus)
//        {
//            var pedido = pedidoRepository.GetPedido(id);
//            pedido.Status = novoStatus;
//            return pedidoRepository.UpdatePedido(pedido);
//        }

//        public void RemoverPedido(int id)
//        {
//            pedidoRepository.DeletePedido(id);
//        }

//        public List<Pedido> ListarPedidos(Func<Pedido, bool> predicate)
//        {
//            return pedidoRepository.GetPedidos(predicate).ToList();
//        }

//        public decimal CalcularValorTotalPedido(int id)
//        {
//            var itensPedido = itemPedidoRepository.GetItemPedidosByPedidoId(id);
//            return itensPedido.Sum(item => item.PrecoUnitario * item.Quantidade);
//        }
//    }

//}
