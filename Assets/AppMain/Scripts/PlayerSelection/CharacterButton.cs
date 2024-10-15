using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterButton : MonoBehaviour {
    private RectTransform _triangleRectTransform = null;

    [SerializeField] private Image _selectedCharacter = null;
    [SerializeField] private Image _triangle = null;

    public bool IsSelected { get; private set; }

    private void Start() {
        _triangleRectTransform = _triangle.GetComponent<RectTransform>();
        _triangleRectTransform.DOAnchorPos(new Vector2(-73f, 0), 0.5f)
            .SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_triangleRectTransform.gameObject);
    }

    /// <summary>
    /// キャラクターボタンの選択状態を変更する
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelection(bool isSelected) {
        IsSelected = isSelected;
        _selectedCharacter.enabled = IsSelected;
        _triangle.enabled = IsSelected;
    }
}
