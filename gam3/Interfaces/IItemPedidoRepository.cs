using gam3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gam3.Interfaces
{
public interface IItemPedidoRepository
{
    ItemPedido CreateItemPedido(ItemPedido itemPedido);
    ItemPedido GetItemPedido(int id);
    ItemPedido UpdateItemPedido(ItemPedido itemPedido);
    void DeleteItemPedido(int id);
    IEnumerable<ItemPedido> GetItemPedidosByPedidoId(int pedidoId);
    
}
}
