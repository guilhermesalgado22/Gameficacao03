using gam3.Models;
using gam3.Services;
using System;

namespace gam3
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;database=game3;user=root;password=33551427Gui$";
            GerenciamentoDePedidos repository = new GerenciamentoDePedidos(connectionString);
            GerenciamentoDePedidos gerenciamentoDePedidos = new GerenciamentoDePedidos(repository);


            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1. Criar um novo pedido");
                Console.WriteLine("2. Adicionar um novo item de pedido");
                Console.WriteLine("3. Deletar um item Pedido");
                Console.WriteLine("4. Deletar pedido");
                Console.WriteLine("5. Obter Itens de um pedido por ID");
                Console.WriteLine("6. Listar Pedidos por filtros");
                Console.WriteLine("7. Atualizar Status de um Pedido");
                Console.WriteLine("8. Calcular o valor total de um pedido");



                Console.WriteLine("0. Sair");
                Console.WriteLine();

                Console.Write("Opção selecionada: ");
                string opcao = Console.ReadLine();

                Console.WriteLine();

                switch (opcao)
                {
                    case "1":
                        // Criar um novo pedido
                        Pedido novoPedido = CriarPedido();
                        Pedido pedidoCriado = gerenciamentoDePedidos.CreatePedido(novoPedido);
                        Console.WriteLine("Novo pedido criado com o ID: " + pedidoCriado.Id);
                        Console.WriteLine();
                        break;

                    case "2":
                        // Adicionar um novo item de pedido
                        ItemPedido novoItemPedido = CriarItemPedido();
                        ItemPedido itemPedidoAdicionado = gerenciamentoDePedidos.CreateItemPedido(novoItemPedido);
                        Console.WriteLine("Novo item de pedido adicionado com o ID: " + itemPedidoAdicionado.Id);
                        Console.WriteLine();
                        break;

                    case "3":
                        // Deletar um item de pedido
                        int idDeletar = ObterItemPedidoId();
                        gerenciamentoDePedidos.DeleteItemPedido(idDeletar);
                        Console.WriteLine("Item de pedido removido com sucesso.");
                        Console.WriteLine();
                        break;


                    case "4":
                        // Deletar um pedido
                        int idDeletarPedido = ObterPedidoId();
                        gerenciamentoDePedidos.DeletePedido(idDeletarPedido);
                        Console.WriteLine("Pedido removido com sucesso.");
                        Console.WriteLine();
                        break;

                    case "5":
                        // Listar itens de um pedido
                        int idPedido = ObterPedidoId();
                        var itensPedido = gerenciamentoDePedidos.GetItemPedidosByPedidoId(idPedido);
                        Console.WriteLine("Itens de pedido do pedido com ID " + idPedido + ":");
                        foreach (var itemPedido in itensPedido)
                        {
                            Console.WriteLine($"ID: {itemPedido.Id}, Quantidade: {itemPedido.Quantidade}, Preço Unitário: {itemPedido.PrecoUnitario}");
                        }
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine("Digite o predicado para filtrar os pedidos:");
                        Console.WriteLine("1. Filtrar por status");
                        Console.WriteLine("2. Filtrar por cliente");
                        Console.WriteLine("3. Filtrar por data");
                        Console.WriteLine();

                        Console.Write("Opção selecionada: ");
                        string opcaoPredicado = Console.ReadLine();
                        Console.WriteLine();

                        switch (opcaoPredicado)
                        {
                            case "1":
                                Console.Write("Digite o status para filtrar os pedidos: ");
                                string status = Console.ReadLine();
                                var pedidosPorStatus = gerenciamentoDePedidos.GetPedidos(p => p.Status == status);
                                Console.WriteLine("Pedidos com o status '" + status + "':");
                                foreach (var pedido in pedidosPorStatus)
                                {
                                    Console.WriteLine($"ID: {pedido.Id}, Data do Pedido: {pedido.DataPedido}, Cliente: {pedido.Cliente}, Status: {pedido.Status}");
                                }
                                Console.WriteLine();
                                break;

                            case "2":
                                Console.Write("Digite o nome do cliente para filtrar os pedidos: ");
                                string cliente = Console.ReadLine();
                                var pedidosPorCliente = gerenciamentoDePedidos.GetPedidos(p => p.Cliente == cliente);
                                Console.WriteLine("Pedidos do cliente '" + cliente + "':");
                                foreach (var pedido in pedidosPorCliente)
                                {
                                    Console.WriteLine($"ID: {pedido.Id}, Data do Pedido: {pedido.DataPedido}, Cliente: {pedido.Cliente}, Status: {pedido.Status}");
                                }
                                Console.WriteLine();
                                break;

                            case "3":
                                Console.Write("Digite a data do pedido (yyyy-MM-dd) para filtrar os pedidos: ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime dataPedido))
                                {
                                    var pedidosPorData = gerenciamentoDePedidos.GetPedidos(p => p.DataPedido.Date == dataPedido.Date);
                                    Console.WriteLine("Pedidos da data '" + dataPedido.ToString("yyyy-MM-dd") + "':");
                                    foreach (var pedido in pedidosPorData)
                                    {
                                        Console.WriteLine($"ID: {pedido.Id}, Data do Pedido: {pedido.DataPedido}, Cliente: {pedido.Cliente}, Status: {pedido.Status}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Data inválida.");
                                }
                                Console.WriteLine();
                                break;

                            default:
                                Console.WriteLine("Opção inválida.");
                                Console.WriteLine();
                                break;
                        }
                        break;

                    case "7":               
                        int idPedidos = ObterPedidoId();
                        Console.Write("Digite o novo status do pedido: ");
                        string novoStatus = Console.ReadLine();

                        Pedido pedidoAtualizado = gerenciamentoDePedidos.GetPedido(idPedidos);

                        if (pedidoAtualizado != null)
                        {
                            pedidoAtualizado.Status = novoStatus;
                            Pedido pedidoAtualizados = gerenciamentoDePedidos.UpdatePedido(pedidoAtualizado);

                            if (pedidoAtualizado != null)
                            {
                                Console.WriteLine("Status do pedido atualizado com sucesso.");
                                Console.WriteLine($"ID: {pedidoAtualizado.Id}, Data do Pedido: {pedidoAtualizado.DataPedido}, Cliente: {pedidoAtualizado.Cliente}, Status: {pedidoAtualizado.Status}");
                            }
                            else
                            {
                                Console.WriteLine("Falha ao atualizar o status do pedido.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Pedido não encontrado.");
                        }

                        Console.WriteLine();
                        break;


                    case "8":
                        int idPedidoCalcularTotal = ObterPedidoId();

                        decimal valorTotal = gerenciamentoDePedidos.CalcularValorTotalPedido(idPedidoCalcularTotal);

                        Console.WriteLine($"Valor total do pedido (ID: {idPedidoCalcularTotal}): R$ {valorTotal}");
                        Console.WriteLine();
                        break;



                    case "0":
                        sair = true;
                        break;

                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.WriteLine();
                        break;
                }
            }
        }

        static Pedido CriarPedido()
        {
            Console.WriteLine("Digite os detalhes do pedido:");
            Console.Write("Data do pedido: ");
            DateTime dataPedido = DateTime.Parse(Console.ReadLine());

            Console.Write("Cliente: ");
            string cliente = Console.ReadLine();

            Console.Write("Status: ");
            string status = Console.ReadLine();

            Pedido pedido = new Pedido
            {
                DataPedido = dataPedido,
                Cliente = cliente,
                Status = status
            };

            return pedido;
        }

        static ItemPedido CriarItemPedido()
        {
            Console.WriteLine("Digite os detalhes do item de pedido:");
            Console.Write("ID do pedido: ");
            int pedidoId = int.Parse(Console.ReadLine());

            Console.Write("Quantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            Console.Write("Preço unitário: ");
            decimal precoUnitario = decimal.Parse(Console.ReadLine());

            ItemPedido itemPedido = new ItemPedido
            {
                Pedido = new Pedido { Id = pedidoId },
                Quantidade = quantidade,
                PrecoUnitario = precoUnitario
            };

            return itemPedido;
        }

        static int ObterItemPedidoId()
        {
            Console.Write("Digite o ID do item de pedido: ");
            int id = int.Parse(Console.ReadLine());
            return id;
        }

        static int ObterPedidoId()
        {
            Console.Write("Digite o ID do pedido: ");
            int id = int.Parse(Console.ReadLine());
            return id;
        }


    }
}