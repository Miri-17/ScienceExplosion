using UnityEngine;

// TODO BGM (GameObject (Dont Destroy))にも活用できると尚よし.
// TODO total時間なども取得できたら良いかも.

[CreateAssetMenu(fileName = "MusicDB", menuName = "ScriptableObjects/Music Database")]
public class MusicDB : ScriptableObject {
    #region Serialized Fields
    [SerializeField] private int _index;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private AudioClip _audioClip;
    #endregion
}
