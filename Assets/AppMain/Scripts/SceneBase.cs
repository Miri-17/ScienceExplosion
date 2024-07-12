using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour {
    [SerializeField] protected int _initialViewIndex = 0;
    [SerializeField] protected bool _isInitialTransition = true;
    [SerializeField] protected List<ViewBase> _views = new List<ViewBase>();

    protected ViewBase _currentView = null;

    protected virtual void Start() {
        if (_initialViewIndex < 0)
            return;
        
        foreach(var view in _views) {
            // SceneにSceneBaseを継承したオブジェクト（TitleSceneなど）自身を設定
            view.Scene = this;

            if (_views.IndexOf(view) == _initialViewIndex) {
                // 初期インデックスのビューに対する処理
                if (view.Transition != null && _isInitialTransition) {
                    view.Transition.CanvasGroup.alpha = 0;
                    view.OnViewOpened();
                    view.gameObject.SetActive(true);
                    view.Transition.TransitionIn();
                } else {
                    view.OnViewOpened();
                    view.gameObject.SetActive(true);
                }

                _currentView = view;
            } else {
                view.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 現在のビューを変更する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual async UniTask ChangeView(int index) {
        // 現在のビューが設定されている場合、Close処理を行う
        if (_currentView != null) {
            _currentView.OnViewClosed();
            if (_currentView.Transition != null)
                await _currentView.Transition.TransitionOutWait();
        }

        // 指定されたインデックスのビューを検索し、処理を行う
        foreach(var view in _views) {
            if (_views.IndexOf(view) == index) {
                view.gameObject.SetActive(true);
                view.OnViewOpened();
                if (view.Transition != null)
                    await view.Transition.TransitionInWait();
                
                _currentView = view;
            } else {
                view.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// シーン遷移を行う
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public virtual async UniTask ChangeScene(string sceneName) {
        // 現在のビューが設定されている場合Close処理を行う
        if (_currentView != null) {
            _currentView.OnViewClosed();
            if (_currentView.Transition != null)
                await _currentView.Transition.TransitionOutWait();
        }

        await SceneManager.LoadSceneAsync(sceneName);
    }
}
