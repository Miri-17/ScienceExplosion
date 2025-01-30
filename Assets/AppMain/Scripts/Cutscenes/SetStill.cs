using UnityEngine;
using UnityEngine.UI;

public class SetStill : MonoBehaviour {
    // 背景とびっくりマークもこのクラスで変更できるようにすること.
    [SerializeField] private Image _selectedStill = null;

    public bool IsSelected { get; private set; }

    /// <summary>
    /// スチルの選択状態を変更する.
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelection(bool isSelected) {
        IsSelected = isSelected;
        _selectedStill.enabled = IsSelected;
    }
}
