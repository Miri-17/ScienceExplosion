using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "ScriptableObjects/Character Database")]
public class CharacterDB : ScriptableObject {
    #region Serialized Fields
    [Header("0...Menu, 1...Battle, 2...Character")]
    [SerializeField] private List<Sprite> _characterSprites = null;
    [Header("0...Menu, 1...Battle")]
    [SerializeField] private List<Sprite> _placeSprites = null;

    // CharactersとPlayerSelectionで使用している情報.
    [SerializeField] private Sprite _nameSprite = null;
    [SerializeField] private Sprite _majorSprite = null;

    // MapとEnemySelectionで使用している情報.
    [SerializeField] private Sprite _placeNameSprite = null;
    [SerializeField] private Sprite _iconSprite = null;

    // PlayerSelectionとEnemySelectionで使用している情報.
    // 場合によっては private List<Color> _uniqueColors = null; にする.
    [SerializeField] private Color _uniqueColor = Color.white;
    
    // EnemySelectionで使用している情報.
    [SerializeField] private Sprite _nationMarkSprite = null;

    // 今の所Menuでしか使用していないが、Storyでも使われると考えられる情報.
    [SerializeField] private Sprite _nameSpriteSpeech = null;
    #endregion

    #region Public Properties
    public List<Sprite> CharacterSprites { get => _characterSprites; set => _characterSprites = value; }
    public List<Sprite> PlaceSprites { get => _placeSprites; set => _placeSprites = value; }

    public Sprite NameSprite { get => _nameSprite; set => _nameSprite = value; }
    public Sprite MajorSprite { get => _majorSprite; set => _majorSprite = value; }

    public Sprite PlaceNameSprite { get => _placeNameSprite; set => _placeNameSprite = value; }
    public Sprite IconSprite { get => _iconSprite; set => _iconSprite = value; }

    public Color UniqueColor { get => _uniqueColor; set => _uniqueColor = value; }

    public Sprite NationMarkSprite { get => _nationMarkSprite; set => _nationMarkSprite = value; }

    public Sprite NameSpriteSpeech { get => _nameSpriteSpeech; set => _nameSpriteSpeech = value; }
    #endregion
}
