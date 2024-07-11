using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Linq;
using TMPro;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;
using UnityEditor.Experimental.GraphView;

// TODO 見てるサイトとは違って、”"で囲んだ値が取得できない←非常に困る
// SpreadSheet使うものに変更することを考える。
public class CSVReader : MonoBehaviour {
    // CSVファイル
    private TextAsset csvFile;
    // CSVファイルの中身を入れるリスト
    // private List<string[]> csvData = new List<string[]>();

    // インデックスカウンター
    // private int i = 0;
    // [SerializeField]
    // private TextMeshProUGUI englishText;
    // [SerializeField]
    // private TextMeshProUGUI japaneseText;

    private void Start() {
        string _csvName = "ScienceExplosionChat";
        GetCSVData(_csvName);

        // // ResourcesにあるCSVファイルを格納する
        // csvFile = Resources.Load("ScienceExplosionChat") as TextAsset;
        // // TextAssetをStringReaderに変換
        // StringReader reader = new StringReader(csvFile.text);
        // string line = reader.ReadLine();
        // // Debug.Log(line);

        // // while (reader.Peek() != -1)
        // // {
        // //     // 1行ずつ読み込む
        // //     string line = reader.ReadLine();
        // //     // リストに追加する
        // //     csvData.Add(line.Split(','));
        // // }

        // // // csvDataリストの条件を満たす値の数(全て)
        // // for (int i = 0; i < csvData.Count; i++)
        // // {
        // //     // データの表示
        // //     Debug.Log(csvData[i][2]);
        // // }
    }

    public List<StoryData> GetCSVData(string name) {
        // CSVファイルの中身を入れるリスト.
        List<string[]> cells = new List<string[]>();
        // ResourcesにあるCSVファイルを格納する.
        csvFile = Resources.Load(name) as TextAsset;
        // TextAssetをStringReaderに変換.
        StringReader reader = new StringReader(csvFile.text);
        // 1行目はラベルなので外す.
        reader.ReadLine();

        while (reader.Peek() != -1) {
            // 1行ずつ読み込む.
            string line = reader.ReadLine();
            // Debug.Log(line);

            // 行のセルは,で区切られる。セルごとに分けて配列化.
            string[] elements = line.Split(',');
            // foreach( var element in elements) {
            //     Debug.Log(element);
            // }
            cells.Add(elements);
        }

        // デバッグ用
        string log = "";
        List<StoryData> stories = new List<StoryData>();
        foreach (var line in cells) {
            var data = new StoryData();

            var replace2 = line[2].Replace("<br>", "\n");

            // データの値を入れる.
            data.Place = line[0];
            data.Name = line[1];
            data.Talk = replace2;
            data.LeftCharacter = line[3];
            data.CenterCharacter = line[4];
            data.RightCharacter = line[5];

            log += $"<場所> {data.Place}, <キャラNo> {data.Name}, <内容> {data.Talk}, "
                + $"<左> {data.LeftCharacter}, <中> {data.CenterCharacter}, <右> {data.RightCharacter}\n";
            
            stories.Add(data);
        }

        Debug.Log(log);

        return stories;
    }

    // private void Update() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         japaneseText.text = csvData[i][0];
    //         englishText.text = csvData[i][1];

    //         if (i < csvData.Count - 1) {
    //             i++;
    //         }
    //         Debug.Log(csvData[i][0] + csvData[i][1]);
    //     }
    // }
}
