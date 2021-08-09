using RPGeeks.Items;
using RPGeeks.HUDS;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace RPGeeks.Items
{
    public class HoverInfoPopup : MonoBehaviour
    {
        [SerializeField] private RectTransform popupObject = null;
        [SerializeField] private TextMeshProUGUI infoText = null;
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, 0f);
        [SerializeField] private float padding = 25f;

        [SerializeField] private Canvas canvas;

        private void Start()
        {
            popupObject = transform.Find("InfoBox").GetComponent<RectTransform>();
            infoText = popupObject.Find("Text").GetComponent<TextMeshProUGUI>();

            canvas = FindObjectOfType<Canvas>();

            offset.y += popupObject.rect.height * canvas.scaleFactor / 2;
        }

        private void OnEnable()
        {
            ItemDragHandler.onMouseStartHoverItem += DisplayInfo;
            ItemDragHandler.onMouseEndHoverItem += HideInfo;
        }

        private void OnDisable()
        {
            ItemDragHandler.onMouseStartHoverItem -= DisplayInfo;
            ItemDragHandler.onMouseEndHoverItem -= HideInfo;
        }

        private void Update()
        {
            FollowCursor();
        }

        public void HideInfo()
        {
            popupObject.gameObject.SetActive(false);
        }

        private void FollowCursor()
        {
            if (!gameObject.activeSelf) { return; }

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;

            //float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width) - padding;
            //if (rightEdgeToScreenEdgeDistance < 0)
            //{
            //    newPos.x += rightEdgeToScreenEdgeDistance;
            //}
            //float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width) + padding;
            //if (leftEdgeToScreenEdgeDistance > 0)
            //{
            //    newPos.x += leftEdgeToScreenEdgeDistance;
            //}
            //float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height) - padding;
            //if (topEdgeToScreenEdgeDistance < 0)
            //{
            //    newPos.y += topEdgeToScreenEdgeDistance;
            //}
            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(HUDItem infoItem)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<size=35>").Append(infoItem.Name).Append("</size>\n");
            builder.Append(infoItem.ShowInfo());

            infoText.text = builder.ToString();

            popupObject.gameObject.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }
    }
}