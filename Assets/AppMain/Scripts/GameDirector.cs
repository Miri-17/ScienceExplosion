using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GameDirector : MonoBehaviour {
    [HideInInspector] public static GameDirector Instance { get; private set; }

    [SerializeField] private int _selectedCharacterIndex = 0;
    [SerializeField] private int _selectedBackgroundIndex = 0;
    [SerializeField] private int _battleCharacterIndex = 0;
    [SerializeField] private int _enemyCharacterIndex = 0;
    // Charactersで選んだキャラを保持するためだけの変数
    // FIXME 他の渡し方ないか模索すること！
    [SerializeField] private int _charactersFirstIndex = 0;

    public int SelectedCharacterIndex { get => _selectedCharacterIndex; set => _selectedCharacterIndex = value; }
    public int SelectedBackgroundIndex { get => _selectedBackgroundIndex; set => _selectedBackgroundIndex = value; }
    public int BattleCharacterIndex { get => _battleCharacterIndex; set => _battleCharacterIndex = value; }
    public int EnemyCharacterIndex { get => _enemyCharacterIndex; set => _enemyCharacterIndex = value; }
    public int CharactersFirstIndex { get => _charactersFirstIndex; set => _charactersFirstIndex = value; }
    
    private void Awake() {
        // BGMがすでにロードされていたら、自分自身を破棄して終了する.
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
