using UnityEngine;

public class ViewBase : MonoBehaviour {
    private SceneBase _scene = null;
    public SceneBase Scene {
        get => _scene;
        set => _scene = value;
    }
    
    private UITransition _transition = null;
    public UITransition Transition {
        get {
            if (_transition == null)
                _transition = GetComponent<UITransition>();
            return _transition;
        }
    }

    /// <summary>
    /// ビューオープン時コール
    /// </summary>
    public virtual void OnViewOpened() {
        Debug.Log("View Open");
    }

    /// <summary>
    /// ビュークローズ時コール
    /// </summary>
    public virtual void OnViewClosed() {
        Debug.Log("View Close");
    }
}
