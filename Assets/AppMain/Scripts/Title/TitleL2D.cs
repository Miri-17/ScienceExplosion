using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleL2D : MonoBehaviour {
    private Animator _animator = null;

    private void Start() {
        _animator = this.GetComponent<Animator>();
        _animator.enabled = false;
    }

    /// <summary>
    /// アニメーションをさせる
    /// </summary>
    public void PlayAnimation() {
        _animator.enabled = true;
    }
}
