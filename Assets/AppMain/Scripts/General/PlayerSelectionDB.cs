using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSelectionDB", menuName = "ScriptableObjects/PlayerSelection Database")]
public class PlayerSelectionDB : ScriptableObject {
    #region Serialized Fields
    [SerializeField] private List<Sprite> _typeSprites = null;
    [SerializeField] private List<string> _descriptions = null;

    // FIXME PlayerSelection以外でも使う可能性が高い -> 他でも使用する場合CharacterDB保持に変更.
    [Header("min: 1, max: 5")]
    [SerializeField] private List<int> _hps = null;
    [Header("min: 1, max: 5")]
    [SerializeField] private List<int> _damages = null;
    [SerializeField] private List<Sprite> _testTubeSprites = null;
    [Header("min: 1, max: 10")]
    [SerializeField] private List<int> _requiredNumberPuzzle = null;
    [Header("min: 1, max: 10")]
    [SerializeField] private List<int> _requiredNumberEx = null;
    [SerializeField] private List<Sprite> _puzzleSprites = null;
    [SerializeField] private List<Sprite> _exSprites = null;
    #endregion

    #region Public Properties
    public List<Sprite> TypeSprites { get => _typeSprites; set => _typeSprites = value; }
    public List<string> Descriptions { get => _descriptions; set => _descriptions = value; }

    public List<int> HPs { get => _hps; set => _hps = value; }
    public List<int> Damages { get => _damages; set => _damages = value; }
    public List<Sprite> TestTubeSprites { get => _testTubeSprites; set => _testTubeSprites = value; }
    public List<int> RequiredNumberPuzzle { get => _requiredNumberPuzzle; set => _requiredNumberPuzzle = value; }
    public List<int> RequiredNumberEx { get => _requiredNumberEx; set => _requiredNumberEx = value; }
    public List<Sprite> PuzzleSprites { get => _puzzleSprites; set => _puzzleSprites = value; }
    public List<Sprite> ExSprites { get => _exSprites; set => _exSprites = value; }
    #endregion
}
