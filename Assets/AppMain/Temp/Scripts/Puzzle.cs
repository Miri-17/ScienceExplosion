using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler {
    public bool IsSelected { get; private set; }

    // add
    private GameController _gameController;
    [SerializeField] private int _row = 0;
    [SerializeField] private int _column = 0;
    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }

    [SerializeField] private SpriteRenderer _selectedSprite;

    // add
    private void Start() {
        _gameController = FindObjectOfType<GameController>();
    }
    private void Update() {
        if (_column > 0 && _gameController.puzzlesXY[_row, _column - 1] == null)
            FallPuzzle();
    }

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

    private void FallPuzzle() {
        _gameController.puzzlesXY[_row, _column] = null;
        _column--;

        var x = _gameController._hexagonWidth * _row + _gameController.adjustmentWidth * Mathf.Abs(_column % 2);
        var y = _gameController.hexagonHeight * _column;
        this.transform.position = new Vector2(x, y);
        _gameController.puzzlesXY[_row, _column] = this.gameObject;
    }
}
