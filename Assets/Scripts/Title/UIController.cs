using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Title {
    public class UIController : MonoBehaviour {
        [SerializeField] private Text text;

        private void Start() {
            text.DOFade(0.0f, 1.0f)
                // .SetEase(Ease.InQuint)
                .SetEase(Ease.InQuart)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(text.gameObject);
        }
    }
}
