using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Linq;

public class CSVReader : MonoBehaviour {
    // // CSVファイル
    // private TextAsset csvFile;
    // // CSVファイルの中身を入れるリスト
    // private List<string[]> csvData = new List<string[]>();

    // private void Start() {
    //     // ResourcesにあるCSVファイルを格納する
    //     csvFile = Resources.Load("ScienceExplosionChat") as TextAsset;
    //     // TextAssetをStringReaderに変換
    //     StringReader reader = new StringReader(csvFile.text);

    //     while (reader.Peek() != -1)
    //     {
    //         // 1行ずつ読み込む
    //         string line = reader.ReadLine();
    //         // リストに追加する
    //         csvData.Add(line.Split(','));
    //     }

    //     // csvDataリストの条件を満たす値の数(全て)
    //     for (int i = 0; i < csvData.Count; i++)
    //     {
    //         // データの表示
    //         Debug.Log("日本語: " + csvData[i][0] + ", English: " + csvData[i][1]);
    //     }
    // }
}
