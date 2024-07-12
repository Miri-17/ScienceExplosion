using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    [SerializeField] private Text _text;

    private void Start() {
        _text.DOFade(0.0f, 1.0f)
            .SetEase(Ease.InQuart)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_text.gameObject);
    }
}
