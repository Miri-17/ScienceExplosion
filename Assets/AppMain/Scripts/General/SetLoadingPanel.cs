using UnityEngine;
using UnityEngine.UI;

public class SetLoadingPanel : MonoBehaviour {
    [SerializeField] private LoadingDB _loadingDB;
    [SerializeField] private Image _background;

    private void Start() {
        this.gameObject.SetActive(false);
        int index = Random.Range(0, _loadingDB.LoadingBackgrounds.Count);
        _background.sprite = _loadingDB.LoadingBackgrounds[index];
    }
}
