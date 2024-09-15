using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    [SerializeField] private string _name = "";
    [SerializeField] private string _profession = "";
    [SerializeField] private string _skill = "";
    [SerializeField] private string _explosion = "";
    [SerializeField] private string _place = "";
    [SerializeField] private string _affiliationEnglish = "";
    [SerializeField] private string _affiliationJapanese = "";
    // 0 ... StandingCharacter, 1 ... GameCharacter, 2 ... Character
    [SerializeField] private List<Sprite> _characterSprites = null;
    [SerializeField] private Sprite _nationMark = null;
    // 0 ... Home, 1 ... Game
    [SerializeField] private List<Sprite> _placeSprites = null;
    [SerializeField] private Color _uniqueColor = Color.white;
    [SerializeField] private AudioClip _uniqueAudioClip = null;

    public string Name { get => _name; set => _name = value; }
    public string Profession { get => _profession; set => _profession = value; }
    public string Skill { get => _skill; set => _skill = value; }
    public string Explosion { get => _explosion; set => _explosion = value; }
    public string Place { get => _place; set => _place = value;}
    public string AffiliationEnglish { get => _affiliationEnglish; set => _affiliationEnglish = value; }
    public string AffiliationJapanese { get => _affiliationJapanese; set => _affiliationJapanese = value; }
    public List<Sprite> CharacterSprites { get => _characterSprites; set => _characterSprites = value; }
    public Sprite NationMark { get => _nationMark; set => _nationMark = value; }
    public List<Sprite> PlaceSprites { get => _placeSprites; set => _placeSprites = value; }
    public Color UniqueColor { get => _uniqueColor; set => _uniqueColor = value; }
    public AudioClip UniqueAudioClip { get => _uniqueAudioClip; set => _uniqueAudioClip = value; }
}
