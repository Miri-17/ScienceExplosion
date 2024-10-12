using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private TapEffectsController _tapEffectsController;

    // FIXME 以下、InputSystemに対応させること.
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            _tapEffectsController.SpawnEffects();
        }
    }
}
