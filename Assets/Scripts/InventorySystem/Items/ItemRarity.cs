using UnityEngine;

namespace RPGeeks.Items
{
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
    public class ItemRarity : ScriptableObject
    {
        [SerializeField] private new string name = "New Rarity";
        [SerializeField] private Color textColor = new Color(1f, 1f, 1f, 1f);

        public string Name { get => name; }
        public Color TextColor { get => textColor; }
        public string HexColorCode { get => ColorUtility.ToHtmlStringRGB(textColor); }
    }
}


