using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;
using Unity.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    [SerializeField] private GameObject[] _puzzles = null;
    [SerializeField] private GameObject _puzzleBlack = null;
    [SerializeField] private int _rowSize = 17;
    [SerializeField] private int _columnSize = 7;
    // ある六角形の中心から右の六角形の中心までの長さ
    [SerializeField] private float _hexagonWidth = 0.74f;
    // 4つまでしか選択できなくする
    [SerializeField] private int _puzzleDestroyLimit = 4;
    [SerializeField] private LineRenderer _lineRenderer = null;
    // public LineRenderer _lineRenderer = null;

    // 選択中のパズルを保持
    private List<Puzzle> _selectedPuzzles = new List<Puzzle>();
    private GameObject[,] _puzzlesXY = null;

    private void Start() {
        Instance = this;

        _puzzlesXY = new GameObject[_rowSize, _columnSize];
        // ヴィクトリアの時だけ敵のパズル色を無くし、ヴィクトリアのパズルを配置
        if (GameDirector.Instance.BattleCharacterIndex == 8)
            _puzzles[GameDirector.Instance.EnemyCharacterIndex] = _puzzleBlack;
        CreatePuzzles();
    }

    private void Update() {
        UpdateLineRenderer();
    }

    private void CreatePuzzles() {
        for (int i = 0; i < _rowSize; i++) {
            for (int j = 0; j < _columnSize; j++) {
                int r = Random.Range(0, _puzzles.Length);
                var puzzlePrefab = Instantiate(_puzzles[r]);

                // ある六角形の中心から上の六角形の中心までの長さ
                var hexagonHeight = _hexagonWidth * Mathf.Sin(60.0f * Mathf.Deg2Rad);
                 // ある六角形の中心と上の六角形の中心のずれ
                var adjustmentWidth = -_hexagonWidth * Mathf.Cos(60.0f * Mathf.Deg2Rad);
                var x = _hexagonWidth * i + adjustmentWidth * Mathf.Abs(j % 2);
                var y = hexagonHeight * j;
                puzzlePrefab.transform.position = new Vector2(x, y);
                _puzzlesXY[i, j] = puzzlePrefab;
            }
        }
    }

    public void OnPuzzleDown(Puzzle puzzle) {
        _selectedPuzzles.Add(puzzle);
        puzzle.SetSelection(true);
    }

    public void OnPuzzleEnter(Puzzle puzzle) {
        if (_selectedPuzzles.Count < 1) return;
        Debug.Log(_selectedPuzzles.Count);

        if (puzzle.IsSelected) {
            if (_selectedPuzzles.Count >= 2 && _selectedPuzzles[_selectedPuzzles.Count - 2] == puzzle) {
                Debug.Log("Hello!");
                var removedPuzzle = _selectedPuzzles[_selectedPuzzles.Count - 1];
                removedPuzzle.SetSelection(false);
                _selectedPuzzles.Remove(removedPuzzle);
            }
        } else {
            if (_selectedPuzzles.Count >= _puzzleDestroyLimit) return;
            var length = (_selectedPuzzles[_selectedPuzzles.Count - 1].transform.position - puzzle.transform.position).magnitude;
            Debug.Log("length: " + length);
            if (length < _hexagonWidth + 0.01f) {
                _selectedPuzzles.Add(puzzle);
                puzzle.SetSelection(true);
            }
        }
    }
    
    public void OnPuzzleUp(Puzzle puzzle) {
        DestroyPuzzles(_selectedPuzzles);
        _selectedPuzzles.Clear();
    }

    private void DestroyPuzzles(List<Puzzle> puzzles) {
        foreach (var puzzle in puzzles) {
            Destroy(puzzle.gameObject);
        }
    }

    private void UpdateLineRenderer() {
        if (_selectedPuzzles.Count >= 2) {
            _lineRenderer.positionCount = _selectedPuzzles.Count;
            _lineRenderer.SetPositions(_selectedPuzzles.Select(puzzle => puzzle.transform.position).ToArray());
            _lineRenderer.gameObject.SetActive(true);
        } else {
            _lineRenderer.gameObject.SetActive(false);
        }
    }
}
