using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingDB", menuName = "Create Loading Database")]
public class LoadingDB : ScriptableObject {
    [SerializeField] private List<Sprite> _loadingBackgrounds = null;

    public List<Sprite> LoadingBackgrounds { get => _loadingBackgrounds; set => _loadingBackgrounds = value; }
}
