using TMPro;
using UnityEngine;

public class CharacterInfo : MonoBehaviour {
    [SerializeField] private RectTransform _bg = null;
    [SerializeField] private RectTransform _texts = null;
    [SerializeField] private TextMeshProUGUI _infoText = null;

    public void SetAsPlayer() {
        _bg.rotation = Quaternion.Euler(0, 0, 0);
        _texts.anchoredPosition = new Vector2(-227.5f, 0);
        _infoText.text = "Lv. Max";
    }
    
    public void SetAsEnemy() {
        _bg.rotation = Quaternion.Euler(0, 180, 0);
        _texts.anchoredPosition = new Vector2(227.5f, 0);
        _infoText.text = "NPC";
    }
}
