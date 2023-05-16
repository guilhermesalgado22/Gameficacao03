using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gam3.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        // Adicione uma chave estrangeira para a classe Pedido
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
    }
}

