using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScienceExplosion
{
    public class TapEffectsController : MonoBehaviour
    {
        #region
        [SerializeField] private Camera _tapEffectsCamera;
        [SerializeField] private GameObject _tapEffectsPrefab;
        #endregion

        public void SpawnEffects() {
            Vector3 position = _tapEffectsCamera.ScreenToWorldPoint(Input.mousePosition + _tapEffectsCamera.transform.forward * 40);
            GameObject prefab = Instantiate(_tapEffectsPrefab);
            prefab.transform.SetParent(this.transform, false);
            prefab.transform.position = position;
        }
    }    
}
