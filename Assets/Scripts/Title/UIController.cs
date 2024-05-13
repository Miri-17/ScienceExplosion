using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ScienceExplosion.Title {
    public class UIController : MonoBehaviour {
        [SerializeField] private Text _text;

        private void Start() {
            _text.DOFade(0.0f, 1.0f)
                // .SetEase(Ease.InQuint)
                .SetEase(Ease.InQuart)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_text.gameObject);
        }
    }
}
