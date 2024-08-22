using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler {
    public bool IsSelected { get; private set; }

    [SerializeField] private SpriteRenderer _selectedSprite;
    
    public void OnPointerDown(PointerEventData eventData) {
        // print($"オブジェクト {name} がマウスダウンされたよ！");
        GameController.Instance.OnPuzzleDown(this);
    }
    public void OnPointerEnter(PointerEventData eventData) {
        // print($"オブジェクト {name} がマウスエンターされたよ！");
        GameController.Instance.OnPuzzleEnter(this);
    }
    public void OnPointerUp(PointerEventData eventData) {
        // print($"オブジェクト {name} がマウスアップされたよ！");
        GameController.Instance.OnPuzzleUp(this);
    }

    public void SetSelection(bool isSelected) {
        IsSelected = isSelected;
        _selectedSprite.enabled = IsSelected;
    }
}
