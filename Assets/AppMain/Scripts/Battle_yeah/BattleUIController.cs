using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIController : MonoBehaviour {
    private bool _isChangingScene = false;
    private Image _pauseMenuButtonImage = null;
    
    [SerializeField] private GameObject _pauseMenuPanel = null;
    [SerializeField] private Button _pauseMenuButton = null;
    [SerializeField] private List<Sprite> _pauseMenuButtonSprites = new List<Sprite>();

    private void Start() {
        _pauseMenuButtonImage = _pauseMenuButton.GetComponent<Image>();
        _pauseMenuButtonImage.alphaHitTestMinimumThreshold = 1;

        _pauseMenuButton.onClick.AddListener(() => OnPauseMenuButtonClicked());
    }

    private void OnPauseMenuButtonClicked() {
        if (_pauseMenuPanel.activeSelf) {
            _pauseMenuPanel.SetActive(false);
            _pauseMenuButtonImage.sprite = _pauseMenuButtonSprites[0];
        } else {
            _pauseMenuPanel.SetActive(true);
            _pauseMenuButtonImage.sprite = _pauseMenuButtonSprites[1];
        }
    }
}
