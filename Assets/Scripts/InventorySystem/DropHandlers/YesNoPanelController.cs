using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YesNoPanelController : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text;
    public TextMeshProUGUI Text { get => text; }

    [Header("Buttons")]
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    public Button YesButton { get => yesButton; }
    public Button NoButton { get => noButton; }

    private void Start()
    {
        text = transform.Find("SanityCheck").GetComponent<TextMeshProUGUI>();

        yesButton = transform.Find("Yes").GetComponent<Button>();
        noButton = transform.Find("No").GetComponent<Button>();
    }
}
