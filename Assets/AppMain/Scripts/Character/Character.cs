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
    [SerializeField] private Sprite _characterSprite = null;
    [SerializeField] private Sprite _nationMark = null;
    [SerializeField] private Sprite _placeSprite = null;

    public string Name { get => _name; set => _name = value; }
    public string Profession { get => _profession; set => _profession = value; }
    public string Skill { get => _skill; set => _skill = value; }
    public string Explosion { get => _explosion; set => _explosion = value; }
    public string Place { get => _place; set => _place = value; }
    public Sprite CharacterSprite { get => _characterSprite; set => _characterSprite = value; }
    public Sprite NationMark { get => _nationMark; set => _nationMark = value; }
    public Sprite PlaceSprite { get => _placeSprite; set => _placeSprite = value; }
}
