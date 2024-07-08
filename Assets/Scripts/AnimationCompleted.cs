using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCompleted : MonoBehaviour
{
    public void OnCompleteAnimation() {
        Destroy(this.gameObject);
    }
}
