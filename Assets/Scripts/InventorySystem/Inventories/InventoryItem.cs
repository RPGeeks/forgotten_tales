using RPGeeks.HUDS;
using RPGeeks.Items;
using UnityEngine;

namespace RPGeeks.Inventories
{
    [System.Serializable]
    public abstract class InventoryItem : HUDItem
    {
        [Header("Item Data")]
        [SerializeField] private ItemRarity rarity = null;
        [SerializeField] [Min(0)] private int sellPrice = 1;
        [SerializeField] [Min(1)] private int maxStack = 1;

        public override string Rarity { get => RarityText(rarity.Name); }
        public int SellPrice { get => sellPrice; }
        public int MaxStack { get => maxStack; }
        public ItemRarity ItemRarity { get => rarity; }

        public virtual string RarityText(string text)
        {
            string hexColour = rarity.HexColorCode;
            return $"<color=#{hexColour}>{text}</color>";
        }
    }
}