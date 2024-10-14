using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDB", menuName = "ScriptableObjects/Character Database")]
public class CharacterDB : ScriptableObject {
    #region
    [Header("0...Menu, 1...Battle, 2...Character")]
    [SerializeField] private List<Sprite> _characterSprites = null;
    [Header("0...Menu, 1...Battle")]
    [SerializeField] private List<Sprite> _placeSprites = null;

    [Header("PlayerSelectionで使っているが、他でも使う可能性が高い情報")]
    [Header("min: 1, max: 5")]
    [SerializeField] private int _hp = 1;
    [Header("min: 1, max: 5")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private Sprite _puzzleSprite = null;
    [SerializeField] private Sprite _exSprite = null;
    [SerializeField] private Color _uniqueColor = Color.white;

    // [SerializeField] private string _name = "";
    // [SerializeField] private string _profession = "";
    // [SerializeField] private string _skill = "";
    // [SerializeField] private string _explosion = "";
    // [SerializeField] private string _place = "";
    // [SerializeField] private string _affiliationEnglish = "";
    // [SerializeField] private string _affiliationJapanese = "";
    // [SerializeField] private Sprite _nationMark = null;
    // [SerializeField] private AudioClip _uniqueAudioClip = null;
    #endregion

    #region
    public List<Sprite> CharacterSprites { get => _characterSprites; set => _characterSprites = value; }
    public List<Sprite> PlaceSprites { get => _placeSprites; set => _placeSprites = value; }

    public int HP { get => _hp; set => _hp = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public Sprite PuzzleSprite { get => _puzzleSprite; set => _puzzleSprite = value; }
    public Sprite ExSprite { get => _exSprite; set => _exSprite = value; }
    public Color UniqueColor { get => _uniqueColor; set => _uniqueColor = value; }

    // public string Name { get => _name; set => _name = value; }
    // public string Profession { get => _profession; set => _profession = value; }
    // public string Skill { get => _skill; set => _skill = value; }
    // public string Explosion { get => _explosion; set => _explosion = value; }
    // public string Place { get => _place; set => _place = value;}
    // public string AffiliationEnglish { get => _affiliationEnglish; set => _affiliationEnglish = value; }
    // public string AffiliationJapanese { get => _affiliationJapanese; set => _affiliationJapanese = value; }
    // public Sprite NationMark { get => _nationMark; set => _nationMark = value; }
    // public AudioClip UniqueAudioClip { get => _ uniqueAudioClip; set => _uniqueAudioClip = value; }
    #endregion
}
