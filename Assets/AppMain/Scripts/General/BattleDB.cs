using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleDB", menuName = "ScriptableObjects/Battle Database")]
public class BattleDB : ScriptableObject {
    #region Serialized Fields
    [SerializeField] private List<GameObject> _characterInfos = null;
    [SerializeField] private List<Sprite> _decorationSprites = null;

    // TODO 多分、スライダーごとプレハブにして、それをデータベース化した方が良い.
    [SerializeField] private List<Sprite> _hpSprites = null;
    // TODO 多分、Exごとプレハブにして、それをデータベース化した方が良い.
    [SerializeField] private List<Sprite> _exSprites = null;
    #endregion

    #region Public Properties
    public List<GameObject> CharacterInfos { get => _characterInfos; set => _characterInfos = value; }
    public List<Sprite> DecorationSprites { get => _decorationSprites; set => _decorationSprites = value; }
    public List<Sprite> HPSprites { get => _hpSprites; set => _hpSprites = value; }
    public List<Sprite> ExSprites { get => _exSprites; set => _exSprites = value; }
    #endregion
}
