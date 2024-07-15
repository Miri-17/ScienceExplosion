using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialLayout : UIBehaviour, ILayoutGroup {
    [SerializeField] private float _radius = 100;
    [SerializeField] private float _offsetAngle = 0;
    protected override void OnValidate() {
        base.OnValidate();
        ArrangeRadially();
    }

    #region // 要素数が変わると自動的に呼ばれるコールバック
    public void SetLayoutHorizontal() {}
    public void SetLayoutVertical() { ArrangeRadially(); }
    #endregion

    private void ArrangeRadially() {
        if (transform.childCount == 0)
            return;
        var spliteAngle = 360 / transform.childCount;
        var rect = transform as RectTransform;

        for (var i = 0; i < transform.childCount; i++) {
            var child = transform.GetChild(i) as RectTransform;
            var currentAngle = spliteAngle * i + _offsetAngle;
            child.anchoredPosition = new Vector2(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad),
                Mathf.Sin(currentAngle * Mathf.Deg2Rad)
            ) * _radius;
        }
    }
}
