using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(PageScroller))]
public class PageScrollerEditor : ScrollRectEditor {
    public override void OnInspectorGUI() {
        // 内部キャッシュから値をロードする
        serializedObject.Update();

        // 元々のインスペクタ内容を記述する
        base.OnInspectorGUI();

        // プロパティを取得する
        var pageCount = serializedObject.FindProperty("PageCount");
        var threshold = serializedObject.FindProperty("Threshold");
        var pageCircles = serializedObject.FindProperty("PageCircles");

        // プロパティをインスペクタから編集できるように設定する
        EditorGUILayout.PropertyField(pageCount);
        EditorGUILayout.PropertyField(threshold);
        EditorGUILayout.PropertyField(pageCircles);

        // 内部キャッシュに値を保存する
        serializedObject.ApplyModifiedProperties();
    }
}
