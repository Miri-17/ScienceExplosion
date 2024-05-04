using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes {
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        public void PushImage() {
            panel.SetActive(true);
        }
    }
}
