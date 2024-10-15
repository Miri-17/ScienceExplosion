using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneDB", menuName = "ScriptableObjects/Cutscene Database")]
public class CutsceneDB : ScriptableObject {
    // TODO 読むボタン識別のためのインデックス
    // 場合によっては、渡すcsvファイルを格納しても良い気がする
    // [SerializeField] private int _index = 0;
    [SerializeField] private Sprite _titleSprite = null;
    [SerializeField] private List<Sprite> _relatedCharacterSprites = new List<Sprite>();

    // public int Index { get => _index; set => _index = value; }
    public Sprite TitleSprite { get => _titleSprite; set => _titleSprite = value; }
    public List<Sprite> RelatedCharacterSprites { get => _relatedCharacterSprites; set => _relatedCharacterSprites = value; }
}
