using System.Collections.Generic;
using Live2D.Cubism.Framework.Raycasting;
using UnityEngine;

public class ModeSelectionController : MonoBehaviour {
    private List<Animator> _flaskAnimators = null;
    private bool _isLeftSelected = true;

    [SerializeField] private ModeSelectionUIController _modeSelectionUIController = null;
    [SerializeField] private List<CubismRaycaster> _flaskRayCasters = null;

    private void Start() {
        _flaskAnimators = new List<Animator>() {
            _flaskRayCasters[0].GetComponent<Animator>(),
            _flaskRayCasters[1].GetComponent<Animator>()
        };

        _flaskAnimators[1].enabled = false;
        // _flaskAnimators[1].Play("Flask_R_Fadein");
        _flaskAnimators[0].Play("Flask_L_Select");
    }
    
    private void Update() {
        if (!_modeSelectionUIController.IsChangingScene && Input.GetMouseButtonDown(0)) {
            // 接触判定情報を取得する.
            var results = new CubismRaycastHit[2];

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitCount0 = _flaskRayCasters[0].Raycast(ray, results);
            var hitCount1 = _flaskRayCasters[1].Raycast(ray, results);
            // Debug.Log("hitCount0: " + hitCount0);
            // Debug.Log("hitCount1: " + hitCount1);

            if (!_isLeftSelected && hitCount0 == 1) {
                _isLeftSelected = true;

                _flaskAnimators[0].enabled = true;
                _flaskAnimators[0].Play("Flask_L_Select");
                _modeSelectionUIController.OnPlayAloneButtonClicked();

                // _flaskAnimators[0].Play("Flask_L_Select");
                // _flaskAnimators[1].Play("Flask_R_Fadein");
                _flaskAnimators[1].enabled = false;
            } else if (_isLeftSelected && hitCount1 == 1) {
                _isLeftSelected = false;

                _flaskAnimators[1].enabled = true;
                _flaskAnimators[1].Play("Flask_R_Select");
                _modeSelectionUIController.OnPlayTwoButtonClicked();

                // _flaskAnimators[1].Play("Flask_R_Select");
                // _flaskAnimators[0].Play("Flask_L_Fadein");
                _flaskAnimators[0].enabled = false;
            }
        }
    }
}
