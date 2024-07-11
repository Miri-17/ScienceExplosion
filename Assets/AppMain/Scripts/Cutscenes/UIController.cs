using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace ScienceExplosion.CutScenes {
    public class UIController : MonoBehaviour {
        #region
        [SerializeField] private RectTransform _background;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button[] _images;
        // SelectedImagePanelのBackgroundをタップすると閉じるようにする.
        [SerializeField] private EventTrigger _eventTrigger;
        [SerializeField] private GameObject _selectedImagePanel;
        [SerializeField] private Animator _transition;
        [SerializeField] private float _transitionTime = 1.0f;
        #endregion

        private bool _isChangeScene = false;

        private void Start() {
            _background.DOAnchorPos(new Vector2(-2500f, 2500), 10f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart)
                .SetLink(_background.gameObject);

            _backButton.onClick.AddListener(() => OnBackButtonClicked());
            foreach (Button image in _images) {
                image.onClick.AddListener(() => PushImage());
            }
            EventTrigger.Entry entry = new EventTrigger.Entry();
            // 押した瞬間に実行するようにする.
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((x) => CloseSelectedImagePanel());
            //イベントの設定をEventTriggerに反映
            _eventTrigger.triggers.Add(entry);
            
            _selectedImagePanel.SetActive(false);
        }

        private void OnBackButtonClicked() {
             if (_isChangeScene) return;

            _isChangeScene = true;
            StartCoroutine(GoBackToScene());
        }

        private void PushImage() {
            _selectedImagePanel.SetActive(true);
        }

        private void CloseSelectedImagePanel() {
            if (!_selectedImagePanel.activeSelf) return;

            Debug.Log("Close Panel");
            _selectedImagePanel.SetActive(false);
        }

        private IEnumerator GoBackToScene() {
            _transition.SetTrigger("Start");

            yield return new WaitForSeconds(_transitionTime);

            SceneManager.LoadScene("Menu");
        }
    }
}
