using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GameDirector : MonoBehaviour {
    [HideInInspector] public static GameDirector Instance { get; private set; }

    [SerializeField] private int _selectedCharacterIndex = 0;
    [SerializeField] private int _selectedBackgroundIndex = 0;
    [SerializeField] private int _playerCharacterIndex = 0;
    [SerializeField] private int _enemyCharacterIndex = 1;
    // Charactersで選んだキャラを保持するためだけの変数
    // FIXME 他の渡し方ないか模索すること！
    [SerializeField] private int _charactersFirstIndex = 0;
    [SerializeField] private int _score = 0;

    public int SelectedCharacterIndex { get => _selectedCharacterIndex; set => _selectedCharacterIndex = value; }
    public int SelectedBackgroundIndex { get => _selectedBackgroundIndex; set => _selectedBackgroundIndex = value; }
    public int PlayerCharacterIndex { get => _playerCharacterIndex; set => _playerCharacterIndex = value; }
    public int EnemyCharacterIndex { get => _enemyCharacterIndex; set => _enemyCharacterIndex = value; }
    public int CharactersFirstIndex { get => _charactersFirstIndex; set => _charactersFirstIndex = value; }
    public int Score { get => _score; set => _score = value; }
    
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
