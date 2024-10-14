using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutscenesDB", menuName = "ScriptableObjects/Cutscenes Database")]
public class CutscenesDB : ScriptableObject {
    [SerializeField] private List<CutsceneDB> _cutsceneDatabases = null;

    public List<CutsceneDB> CutsceneDatabases { get => _cutsceneDatabases; set => _cutsceneDatabases = value; }

    public CutsceneDB GetCutscene(int index) {
        return _cutsceneDatabases[index];
    }
}
