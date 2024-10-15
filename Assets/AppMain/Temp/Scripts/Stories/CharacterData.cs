using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

/// <summary>
/// キャラクター名、画像関連付データ.
/// </summary>
[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject {
    /// <summary>
    /// キャラクター定義.
    /// </summary>
    public enum Type {
        None = 0,
        Red = 1,        // Dr.P.
        Orange = 2,     // マイラ・キメラ.
        Yellow = 3,     // クラックス・クルー.
        Green = 4,      // ミセス・グリューン.
        Cyan = 5,       // ロン・チンレイ.
        Blue = 6,       // ナミモロ.
        Purple = 7,     // 新村八雲.
        Pink = 8,       // ロジー・バックパック.
        Black = 9,      // ヴィクトリア.
        Shimon = 10,    // シモン.
        Haruki = 11,    // ハルキ.
    }

    // TODO デザイナー班と要確認.
    /// <summary>
    /// 表情の定義.
    /// </summary>
    public enum EmotionType {
        None,
        Normal,
        Happy,
        Sad,
        Angry,
    }

    /// <summary>
    /// 画像と表情の関連付けパラメータ.
    /// </summary>
    [System.Serializable]
    public class ImageParam {
        // 表情タイプ.
        public EmotionType Emotion = EmotionType.None;
        // 画像.
        public Sprite Sprite = null;
    }

    /// <summary>
    /// キャラクターのパラメータ.
    /// </summary>
    [System.Serializable]
    public class Parameter {
        // キャラクター表示名.
        public string DisplayName = "";
        // キャラクタータイプ.
        public Type Character = Type.None;
        // 画像パラメータのリスト.
        public List<ImageParam> ImageParams = new List<ImageParam>();
        
        // 表情タイプから画像を取得する.
        public Sprite GetEmotionSprite(EmotionType emotion) {
            foreach (var img in ImageParams) {
                if (img.Emotion == emotion) return img.Sprite;
            }
            return null;
        }
    }

    // メモ.
    // [SerializeField] private string Memo = "";
    // パラメータリスト.
    [SerializeField] private List<Parameter> Parameters = new List<Parameter>();

    /// <summary>
    /// キャラクター番号からキャラクター表示名を取得する.
    /// </summary>
    /// <param name="characterNumber"></param>
    /// <returns>キャラクター表示名</returns>
    // public string GetCharacterName(string characterNumber) {
    public string GetCharacterName(int characterNumber) {
        // キャラクター番号が0やif文指定範囲以外の時は何もなしで返す.
        if (characterNumber < 0 && characterNumber > 11) return "";
        
        Parameter param = GetParameterFromNumber(characterNumber);
        return param.DisplayName;
        // if (characterNumber == "1" || characterNumber == "2" || characterNumber == "3") {
        //     var param = GetParameterFromNumber(characterNumber);
        //     return param.DisplayName;
        // }

        // // キャラクター番号が0やif文指定範囲以外の時は何もなしで返す.
        // return "";
    }

    // キャラクター番号からパラメータを取得する.
    // private Parameter GetParameterFromNumber(string characterNumber) {
    private Parameter GetParameterFromNumber(int characterNumber) {
        foreach (Parameter param in Parameters) {
            // TODO stringじゃなくて、int型に揃えたほうがいいかも？
            int typeInt = (int)param.Character;
            // var typeString = typeInt.ToString();
            if (typeInt == characterNumber) return param;
        }
        return null;
    }

    /// <summary>
    /// 文字列のデータからキャラクター画像を取得する.
    /// </summary>
    /// <param name="dataString"></param>
    /// <returns>キャラクター画像</returns>
    public Sprite GetCharacterSprite(string dataString) {
        // 先頭の2文字はキャラクター定義.
        // string num = dataString.Substring(0, 2);
        string characterNumberString = dataString.Substring(0, 2);
        // 3文字目から先は表情定義.
        // string emo = dataString.Substring(2);
        string emotionString = dataString.Substring(2);

        // 追加
        int characterNumber = int.Parse(characterNumberString);
        // if (num != "0" && num != "1" && num != "2" && num != "3") {
        if (characterNumber < 0 && characterNumber > 11) {
            Debug.Log("入力データが正しくありません: " + dataString);
            return null;
        }

        // TODO なんで取得してる？
        Parameter param = GetParameterFromNumber(characterNumber);

        EmotionType emotionType = GetEmotionType(emotionString);
        Sprite emotionSprite = param.GetEmotionSprite(emotionType);

        return emotionSprite;
    }

    // 表情部分の文字列からEmotionTypeを取得する.
    private EmotionType GetEmotionType(string emotionString) {
        switch(emotionString) {
            case "Normal": return EmotionType.Normal;
            case "Happy": return EmotionType.Happy;
            case "Sad": return EmotionType.Sad;
            case "Angry": return EmotionType.Angry;
            default: return EmotionType.None;
        }
    }
}
