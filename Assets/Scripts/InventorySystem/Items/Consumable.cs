using RPGeeks.Inventories;
using System.Text;
using UnityEngine;

namespace RPGeeks.Items
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
    public class Consumable: InventoryItem
    {
        [Header("Consumable Data")]
        [SerializeField] private string description = "<Consumable description>";

        public override string ShowInfo()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(Rarity).AppendLine();
            builder.Append(RarityText(description)).AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }
    }
}