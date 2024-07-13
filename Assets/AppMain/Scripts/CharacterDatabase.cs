using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDatabase", menuName = "ScriptableObjects/CharacterDatabase")]
public class CharacterDatabase : ScriptableObject {
    [SerializeField] private List<Character> _characters = new List<Character>();

    /// <summary>
    /// 渡されたインデックスのキャラクター情報を返す
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Character GetCharacter(int index) {
        return _characters[index];
    }
}
