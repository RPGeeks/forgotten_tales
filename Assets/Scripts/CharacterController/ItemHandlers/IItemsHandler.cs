using RPGeeks.Inventories;
using RPGeeks.Items;

namespace RPGeeks.ItemHandlers
{
    public interface IItemsHandler
    {
        public Inventory Inventory { get; }
    }
}
