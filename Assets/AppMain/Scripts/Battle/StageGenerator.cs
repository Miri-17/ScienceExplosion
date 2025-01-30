using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageGenerator : MonoBehaviour {
    #region Serialized Fields
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private BattleDB _battleDB = null;

    [SerializeField] private SpriteRenderer _player = null;
    [SerializeField] private SpriteRenderer _enemy = null;
    [SerializeField] private Image _bgImage = null;

    [SerializeField] private List<TextMeshProUGUI> _exTexts = null;
    [SerializeField] private GameObject _characterInfosOriginal;
    [Header("0...player, 1...enemy")]
    [SerializeField] private List<Image> _decorationImages = null;
    [SerializeField] private List<Image> _hpImages = null;
    [SerializeField] private List<Image> _playerExImages = null;
    [SerializeField] private List<Image> _enemyExImages = null;
    #endregion

    private void Start() {
        if (GameDirector.Instance == null) {
            Debug.Log("GameDirecotr is missing!");
            Destroy(this);
            return;
        }

        var playerIndex = GameDirector.Instance.PlayerCharacterIndex;
        var enemyIndex = GameDirector.Instance.EnemyCharacterIndex;

        var playerCharacter = _charactersDB.GetCharacter(playerIndex);
        var enemyCharacter = _charactersDB.GetCharacter(enemyIndex);
        _player.sprite = playerCharacter.CharacterSprites[1];
        _enemy.sprite = enemyCharacter.CharacterSprites[1];
        _bgImage.sprite = enemyCharacter.PlaceSprites[1];

        _exTexts[0].color = playerCharacter.UniqueColor;
        _exTexts[1].color = enemyCharacter.UniqueColor;
        // TODO 良くないコードなので書き直すこと.
        GameObject playerInfo = Instantiate(_battleDB.CharacterInfos[playerIndex], new Vector3(0, 0, 0), Quaternion.identity);
        playerInfo.transform.SetParent(_characterInfosOriginal.transform, false);
        playerInfo.transform.localPosition = new Vector3(-459, 800, 0);
        playerInfo.GetComponent<CharacterInfo>().SetAsPlayer();
        GameObject enemyInfo = Instantiate(_battleDB.CharacterInfos[enemyIndex], new Vector3(0, 0, 0), Quaternion.identity);
        enemyInfo.transform.SetParent(_characterInfosOriginal.transform, false);
        enemyInfo.transform.localPosition = new Vector3(459, 800, 0);
        enemyInfo.GetComponent<CharacterInfo>().SetAsEnemy();
        _decorationImages[0].sprite = _battleDB.DecorationSprites[playerIndex];
        _decorationImages[1].sprite = _battleDB.DecorationSprites[enemyIndex];
        _hpImages[0].sprite = _battleDB.HPSprites[playerIndex];
        _hpImages[1].sprite = _battleDB.HPSprites[enemyIndex];
        foreach (var playerExImage in _playerExImages)
            playerExImage.sprite = _battleDB.ExSprites[playerIndex];
        foreach (var enemyExImage in _enemyExImages)
            enemyExImage.sprite = _battleDB.ExSprites[enemyIndex];
    }
}
