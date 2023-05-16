using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gam3.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string? Cliente { get; set; }
        public string? Status { get; set; }
        // Adicione uma lista de itens de pedido
        public ICollection<ItemPedido>? ItensPedido { get; set; }
    }
}
