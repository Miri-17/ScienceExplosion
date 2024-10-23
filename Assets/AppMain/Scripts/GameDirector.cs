using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GameDirector : MonoBehaviour {
    [HideInInspector] public static GameDirector Instance { get; private set; }

    [SerializeField] private int _selectedCharacterIndex = 0;
    [SerializeField] private int _selectedPlaceIndex = 0;
    [SerializeField] private int _playerCharacterIndex = 0;
    [SerializeField] private int _enemyCharacterIndex = 1;
    // Charactersで選んだキャラを保持するためだけの変数
    // FIXME 他の渡し方ないか模索すること！
    [SerializeField] private int _charactersFirstIndex = 0;
    [SerializeField] private int _score = 0;
    [SerializeField] private string _rank = "C";
    [SerializeField] private float _masterSliderValue = 10.0f;
    [SerializeField] private float _bgmSliderValue = 10.0f;
    [SerializeField] private float _seSliderValue = 10.0f;

    public int SelectedCharacterIndex { get => _selectedCharacterIndex; set => _selectedCharacterIndex = value; }
    public int SelectedPlaceIndex { get => _selectedPlaceIndex; set => _selectedPlaceIndex = value; }
    public int PlayerCharacterIndex { get => _playerCharacterIndex; set => _playerCharacterIndex = value; }
    public int EnemyCharacterIndex { get => _enemyCharacterIndex; set => _enemyCharacterIndex = value; }
    public int CharactersFirstIndex { get => _charactersFirstIndex; set => _charactersFirstIndex = value; }
    public int Score { get => _score; set => _score = value; }
    public string Rank { get => _rank; set => _rank = value;}
    public float MasterSliderValue { get => _masterSliderValue; set => _masterSliderValue = value; }
    public float BGMSliderValue { get => _bgmSliderValue; set => _bgmSliderValue = value; }
    public float SESliderValue { get => _seSliderValue; set => _seSliderValue = value; }
    
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
