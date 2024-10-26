using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance { get; private set; }

    #region
    // 同時押し防ぐ
    private bool _isAlreadyPuzzleDown = false;
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
    [SerializeField] private List<EachPuzzle> _eachPuzzle = new List<EachPuzzle>();
    [SerializeField] private LineRenderer _lineRenderer = null;
    // ある六角形の中心から右の六角形の中心までの長さ
    [SerializeField] private float _hexagonWidth = 0.74f;
    [SerializeField] private int _rowSize = 17;
    [SerializeField] private int _columnSize = 7;
    [SerializeField, Header("パズルが何個以上で消えるか")] private int _puzzleLowerLimit = 3;
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

        CreatePuzzles();
    }

    private void CreatePuzzles() {
        for (int i = 0; i < _rowSize; i++) {
            for (int j = 0; j < _columnSize; j++) {
                if (_puzzlesXY[i, j] == null) {
                    var puzzles = _eachPuzzle[GameDirector.Instance.PlayerCharacterIndex].Puzzles;
                    int r = Random.Range(0, puzzles.Count);
                    var puzzlePrefab = Instantiate(puzzles[r]);
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
        if (_isAlreadyPuzzleDown) return;

        Debug.Log("OnPuzzleDown");
        _isAlreadyPuzzleDown = true;
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

        Debug.Log("important: " + _selectedPuzzles.Count);
        switch(_selectedPuzzles.Count) {
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

        if (puzzle.IsSelected) {
            if (_selectedPuzzles.Count >= 2 && _selectedPuzzles[_selectedPuzzles.Count - 2] == puzzle) {
                var removedPuzzle = _selectedPuzzles[_selectedPuzzles.Count - 1];
                removedPuzzle.SetSelection(false);
                _selectedPuzzles.Remove(removedPuzzle);
                _audioSource_SE.PlayOneShot(_audioClip_SE);
            }
        } else {
            var length = (_selectedPuzzles[_selectedPuzzles.Count - 1].transform.position - puzzle.transform.position).magnitude;
            // Debug.Log("length: " + length);
            if (length < _hexagonWidth + 0.01f) {
                _selectedPuzzles.Add(puzzle);
                puzzle.SetSelection(true);
                _audioSource_SE.PlayOneShot(_audioClip_SE);
            }
        }

        UpdateLineRenderer();
    }
    
    public void OnPuzzleUp(Puzzle puzzle) {
        Debug.Log("OnPuzzleUp");
        Debug.Log(puzzle);
        _isAlreadyPuzzleDown = false;

        if (_selectedPuzzles.Count >= _puzzleLowerLimit) {
            _audioClip_SE = SE.Instance.audioClips[6];
            _audioSource_SE.PlayOneShot(_audioClip_SE);
            _battleUIController.AddScore(_selectedPuzzles.Count, puzzle.ID);
            DestroyPuzzles(_selectedPuzzles);
            // 消えたパズルがある行
            var affectedRows = _selectedPuzzles.Select(p => p.Row).Distinct().ToList();
            TriggerFallCheck(affectedRows);
            CreatePuzzles();
        } else {
            foreach (var selectedPuzzle in _selectedPuzzles)
                selectedPuzzle.SetSelection(false);
        }

        _selectedPuzzles.Clear();
        
        UpdateLineRenderer();
    }
}

[System.Serializable]
public class EachPuzzle {
    public List<GameObject> Puzzles = new List<GameObject>();
}
