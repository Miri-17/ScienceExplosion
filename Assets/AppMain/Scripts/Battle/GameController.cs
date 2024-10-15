using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    #region
    private List<Puzzle> _selectedPuzzles = new List<Puzzle>();
    private GameObject[,] _puzzlesXY;
    // ある六角形の中心から上の六角形の中心までの長さ
    private float _hexagonHeight = 0;
    // ある六角形の中心と上の六角形の中心のずれ
    private float _adjustmentWidth = 0;
    private string _selectID = "";

    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    #endregion

    #region
    [SerializeField] private BattleUIController _battleUIController = null;
    [SerializeField] private GameObject[] _puzzles = null;
    [SerializeField] private GameObject _puzzleBlack = null;
    [SerializeField] private LineRenderer _lineRenderer = null;
    // ある六角形の中心から右の六角形の中心までの長さ
    [SerializeField] private float _hexagonWidth = 0.74f;
    [SerializeField] private int _rowSize = 17;
    [SerializeField] private int _columnSize = 7;
    #endregion

    private void Awake() {
        if (Instance != null) {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        _puzzlesXY = new GameObject[_rowSize, _columnSize];

        _hexagonHeight = _hexagonWidth * Mathf.Sin(60.0f * Mathf.Deg2Rad);
        _adjustmentWidth = -_hexagonWidth * Mathf.Cos(60.0f * Mathf.Deg2Rad);

        // ヴィクトリアの時だけ敵のパズル色を無くし、ヴィクトリアのパズルを配置
        if (GameDirector.Instance.PlayerCharacterIndex == 8)
            _puzzles[GameDirector.Instance.EnemyCharacterIndex] = _puzzleBlack;
        CreatePuzzles();
    }

    private void CreatePuzzles() {
        for (int i = 0; i < _rowSize; i++) {
            for (int j = 0; j < _columnSize; j++) {
                if (_puzzlesXY[i, j] == null) {
                    int r = Random.Range(0, _puzzles.Length);
                    var puzzlePrefab = Instantiate(_puzzles[r]);
                    puzzlePrefab.transform.parent = GameObject.Find("Puzzles").transform;
                    var x = _hexagonWidth * i + _adjustmentWidth * Mathf.Abs(j % 2);
                    var y = _hexagonHeight * j;
                    // puzzlePrefab.transform.position = new Vector2(x, y);
                    puzzlePrefab.transform.position = new Vector3(x, y, 80);
                    _puzzlesXY[i, j] = puzzlePrefab;

                    Puzzle _puzzle = puzzlePrefab.GetComponent<Puzzle>();
                    _puzzle.Row = i;
                    _puzzle.Column = j;
                }
            }
        }
    }

    private void DestroyPuzzles(List<Puzzle> selectedPuzzles) {
        foreach (var selectedPuzzle in selectedPuzzles) {
            Destroy(selectedPuzzle.gameObject);
            _puzzlesXY[selectedPuzzle.Row, selectedPuzzle.Column] = null;
        }
    }

    private void TriggerFallCheck(List<int> affectedRows) {
        foreach (var row in affectedRows) {
           for (int j = 1; j < _columnSize; j++) {
                var temp_j = j;
                while (temp_j > 0) {
                    if (_puzzlesXY[row, temp_j - 1] != null || _puzzlesXY[row, temp_j] == null)
                        break;
                    
                    FallPuzzle(row, temp_j);
                    temp_j--;
                }
            }
        }
    }

    private void FallPuzzle(int row, int column) {
        var puzzle = _puzzlesXY[row, column].GetComponent<Puzzle>();
        _puzzlesXY[row, column] = null;
        puzzle.Column--;
        var x = _hexagonWidth * puzzle.Row + _adjustmentWidth * Mathf.Abs(puzzle.Column % 2);
        var y = _hexagonHeight * puzzle.Column;
        // puzzle.gameObject.transform.position = new Vector2(x, y);
        puzzle.gameObject.transform.position = new Vector3(x, y, 80);
        _puzzlesXY[row, column - 1] = puzzle.gameObject;
    }

    private void UpdateLineRenderer() {
        var puzzleCount = _selectedPuzzles.Count;
        if (puzzleCount >= 2) {
            _lineRenderer.positionCount = puzzleCount;
            for (int i = 0; i < puzzleCount; i++) {
                _lineRenderer.SetPosition(i, _selectedPuzzles[i].transform.position);
            }
            _lineRenderer.gameObject.SetActive(true);
        } else {
            _lineRenderer.gameObject.SetActive(false);
        }
    }

    public void OnPuzzleDown(Puzzle puzzle) {
        Debug.Log("OnPuzzleDown");
        _audioClip_SE = SE.Instance.audioClips[2];
        _audioSource_SE.PlayOneShot(_audioClip_SE);

        _selectedPuzzles.Add(puzzle);
        puzzle.SetSelection(true);
        _selectID = puzzle.ID;

        UpdateLineRenderer();
    }

    public void OnPuzzleEnter(Puzzle puzzle) {
        if (_selectedPuzzles.Count < 1 || _selectID != puzzle.ID)
            return;

        // Debug.Log(_selectedPuzzles.Count);

        if (puzzle.IsSelected) {
            if (_selectedPuzzles.Count >= 2 && _selectedPuzzles[_selectedPuzzles.Count - 2] == puzzle) {
                var removedPuzzle = _selectedPuzzles[_selectedPuzzles.Count - 1];
                removedPuzzle.SetSelection(false);
                _selectedPuzzles.Remove(removedPuzzle);
            }
        } else {
            var length = (_selectedPuzzles[_selectedPuzzles.Count - 1].transform.position - puzzle.transform.position).magnitude;
            // Debug.Log("length: " + length);
            if (length < _hexagonWidth + 0.01f) {
                _selectedPuzzles.Add(puzzle);
                puzzle.SetSelection(true);
            }
        }

        Debug.Log("OnPuzzleEnter");
        Debug.Log(_selectedPuzzles.Count);
        switch(_selectedPuzzles.Count) {
            // TODO case 1の時の音変えた方が良いかも
            case 1:
                _audioClip_SE = SE.Instance.audioClips[2];
                break;
            case 2:
                _audioClip_SE = SE.Instance.audioClips[3];
                break;
            case 3:
                _audioClip_SE = SE.Instance.audioClips[4];
                break;
            default:
                _audioClip_SE = SE.Instance.audioClips[5];
                break;
        }
        _audioSource_SE.PlayOneShot(_audioClip_SE);

        UpdateLineRenderer();
    }
    
    public void OnPuzzleUp(Puzzle puzzle) {
        Debug.Log("OnPuzzleUp");
        _audioClip_SE = SE.Instance.audioClips[6];
        _audioSource_SE.PlayOneShot(_audioClip_SE);

        Debug.Log(puzzle);
        _battleUIController.AddScore(_selectedPuzzles.Count, puzzle.ID);
        DestroyPuzzles(_selectedPuzzles);
        // 消えたパズルがある行
        var affectedRows = _selectedPuzzles.Select(p => p.Row).Distinct().ToList();
        _selectedPuzzles.Clear();
        TriggerFallCheck(affectedRows);
        
        UpdateLineRenderer();

        CreatePuzzles();
    }
}
