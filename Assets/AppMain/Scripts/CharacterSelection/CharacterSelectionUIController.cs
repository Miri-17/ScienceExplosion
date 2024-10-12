using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class CharacterSelectionUIController : MonoBehaviour {
    #region 
    private bool _isSceneTransitioning = false;
    private readonly float SCALE_DURATION = 0.5f;
    #endregion

    #region
    [SerializeField] private List<Image> _characterButtonImages = null;
    [SerializeField] private Button _readyButton = null;
    [SerializeField] private Button _noButton = null;
    [SerializeField] private Button _yesButton = null;
    [SerializeField] private GameObject _warningPanel = null;
    [SerializeField] private RectTransform _unmask = null;
    [SerializeField] private Image _screen = null;
    #endregion

    private void Start() {
        // ボタンの透過部分の反応をなくす
        foreach (var characterButtonImage in _characterButtonImages)
            characterButtonImage.alphaHitTestMinimumThreshold = 1;

        _screen.enabled = false;
        _warningPanel.SetActive(false);

        _readyButton.onClick.AddListener(() => OnReadyButtonClicked());
        _noButton.onClick.AddListener(() => OnNoButtonClicked());
        _yesButton.onClick.AddListener(() => OnYesButtonClicked());
    }

    private void OnReadyButtonClicked() {
        _warningPanel.SetActive(true);
    }
    private void OnNoButtonClicked() {
        _warningPanel.SetActive(false);
    }
    private void OnYesButtonClicked() {
        if (_isSceneTransitioning) return;

        _isSceneTransitioning = true;
        StartCoroutine(GoToBattle());
    }

    private IEnumerator GoToBattle() {
        _warningPanel.SetActive(false);
        IrisOut();

        yield return new WaitForSeconds(0.5f);
        
        // Debug.Log("Go to Battle");
        SceneManager.LoadScene("Battle");
    }

    private void IrisOut() {
        _screen.enabled = true;
        _unmask.DOScale(new Vector3(0, 0, 0), SCALE_DURATION)
            .SetEase(Ease.InBack)
            .SetLink(_unmask.gameObject);
    }
}
