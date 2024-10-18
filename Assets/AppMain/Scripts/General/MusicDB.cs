using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicDB", menuName = "ScriptableObjects/Music Database")]
public class MusicDB : ScriptableObject {
    // TODO BGM (GameObject (Dont Destroy))にも活用できると尚よし
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private AudioClip _audioClip;
    // TODO total時間なども取得できたら良いかも
}
