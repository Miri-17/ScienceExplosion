using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageScroller : ScrollRect {
    // TODO うまいことpublicを使わない、またはpublicをプロパティにしてもエラーになる方法がないか模索すること
    // [SerializeField] private int _pageCount = 0;
    // [SerializeField] private float _threshold = 0;
    // public int PageCount { get => _pageCount; set => _pageCount = value; }
    // public float Threshold { get => _threshold; set => _threshold = value; }
    public int PageCount = 0;
    // 移動量が20%以下なら移動せず、それより大きければ移動する
    public float Threshold = 0.2f;
    public List<Image> PageCircles = null;

    private float _beforePosition = 0;

    public override void OnBeginDrag(PointerEventData eventData) {
        base.OnBeginDrag(eventData);
        _beforePosition = horizontalNormalizedPosition;
    }

    public override void OnEndDrag(PointerEventData eventData) {
        base.OnEndDrag(eventData);
        var nextPageNumber = GetNextPageNumber(_beforePosition, horizontalNormalizedPosition);
        horizontalNormalizedPosition = nextPageNumber / (PageCount - 1);
        Debug.Log(nextPageNumber);
        // PageCircles[(int)nextPageNumber].color = new Color(163, 24, 25);
        // PageCircles[Mathf.Abs((int)nextPageNumber - 1)].color = new Color(91, 16, 16);
    }

    private float GetNextPageNumber(float beforePosition, float afterPosition) {
        // 移動量(%)を求める
        var movement = (afterPosition - beforePosition) * (PageCount - 1);
        // 移動量が閾値以下なら移動しない
        if (Mathf.Abs(movement) <= Threshold)
            return beforePosition * (PageCount - 1);
        
        // 次ページへ移動
        if (movement > 0) {
            // PageCircles[1].color = new Color(163, 24, 25);
            // PageCircles[0].color = new Color(91, 16, 16);
            return Mathf.Ceil(afterPosition * (PageCount - 1));
        }
        
        // 前ページへ移動
        // PageCircles[0].color = new Color(163, 24, 25);
        // PageCircles[1].color = new Color(91, 16, 16);
        return Mathf.Floor(afterPosition * (PageCount - 1));
    }
}
