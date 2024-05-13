using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameObject _tapEffectsPrefab;
    [SerializeField] private Camera _tapEffectsCamera;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 position = _tapEffectsCamera.ScreenToWorldPoint(Input.mousePosition + _tapEffectsCamera.transform.forward * 10);
            GameObject prefab = Instantiate(_tapEffectsPrefab);
            prefab.transform.position = position;
        }
    }
}
