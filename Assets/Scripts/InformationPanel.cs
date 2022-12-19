using UnityEngine;
using UnityEngine.UI;

public class InformationPanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton = null;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseButtonClicked);
    }

    private void CloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
