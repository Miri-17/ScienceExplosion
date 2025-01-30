using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler {
    #region Serialized Fields
    [SerializeField] private SpriteRenderer _selectedSprite = null;
    [SerializeField] private string _id = "";
    [SerializeField] private int _row = 0;
    [SerializeField] private int _column = 0;
    #endregion

    #region Public Properties
    public bool IsSelected { get; private set; }
    public string ID { get => _id; set => _id = value; }
    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }
    #endregion

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

    /// <summary>
    /// パズルの選択状態を変更する.
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelection(bool isSelected) {
        IsSelected = isSelected;
        _selectedSprite.enabled = IsSelected;
    }
}
