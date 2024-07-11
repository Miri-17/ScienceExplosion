using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ScienceExplosion.Menu {
    public class UIController : MonoBehaviour {
        [SerializeField] private RectTransform _image;

        private void Start() {
            _image.DOAnchorPos(new Vector2(-10.1f, 38.0f), 1.0f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(_image.gameObject);
        }
    }
}
