using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class TitleL2D : MonoBehaviour {
    private Animator _animator = null;

    [SerializeField] private int id = 0;

    private void Start() {
        _animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// アニメーションをさせる
    /// </summary>
    public void PlayAnimation() {
        switch (id) {
            case 0:
                _animator.Play("Start_Red");
                break;
            case 1:
                _animator.Play("Start_Orange");
                break;
            case 2:
                _animator.Play("Start_Yellow");
                break;
            case 3:
                _animator.Play("Start_Green");
                break;
            case 4:
                _animator.Play("Start_Cyan");
                break;
            case 5:
                _animator.Play("Start_Blue");
                break;
            case 6:
                _animator.Play("Start_Purple");
                break;
            case 7:
                _animator.Play("Start_Pink");
                break;
            case 8:
                _animator.Play("Start_Black");
                break;
            default:
                break;
        }
    }
}
