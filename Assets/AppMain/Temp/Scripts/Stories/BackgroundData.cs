using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "ScriptableObjects/BackgroundData")]
public class BackgroundData : ScriptableObject {
    /// <summary>
    /// 背景のパラメータ.
    /// </summary>
    [System.Serializable]
    public class Parameter {
        // 名前.
        public string Name = "";
        // 画像パラメータのリスト.
        public Sprite Sprite = null;
    }

    // パラメータリスト.
    // public List<Parameter> Parameters = new List<Parameter>();
    [SerializeField] private List<Parameter> Parameters = new List<Parameter>();

    /// <summary>
    /// 画像名から画像を取得する
    /// </summary>
    /// <param name="imageName"></param>
    /// <returns></returns>
    // public Sprite GetSprite(string imageName) {
    public Sprite GetBackgroundSprite(string imageName) {
        foreach (var param in Parameters) {
            if (param.Name == imageName) return param.Sprite;
        }
        return null;
    }
}
