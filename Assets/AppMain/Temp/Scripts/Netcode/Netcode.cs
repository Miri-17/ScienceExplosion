using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Netcode : MonoBehaviour {
    // public void StartHost() {
    //     // 接続承認コールバック
    //     NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
    //     //ホスト開始
    //     NetworkManager.Singleton.StartHost();
    //     //シーンを切り替え
    //     NetworkManager.Singleton.SceneManager.LoadScene("CharacterSelection", LoadSceneMode.Single);
    // }

    // public void StartClient() {
    //     //ホストに接続
    //     bool result = NetworkManager.Singleton.StartClient();
    // }

    // // 接続承認関数
    // private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response) {
    //     // 追加の承認手順が必要な場合は、追加の手順が完了するまでtrueに設定
    //     // trueからfalseに遷移すると、接続承認応答が処理されます。
    //     response.Pending = true;

    //     // 最大人数をチェック
    //     // 2人プレイなので、クライアントは最大1人まで
    //     if (NetworkManager.Singleton.ConnectedClients.Count >= 2) {
    //         response.Approved = false;
    //         response.Pending = false;
    //         return;
    //     }

    //     //ここからは接続成功クライアントに向けた処理
    //     response.Approved = true;//接続を許可

    //     //PlayerObjectを生成するかどうか
    //     response.CreatePlayerObject = true;

    //     //生成するPrefabハッシュ値。nullの場合NetworkManagerに登録したプレハブが使用される
    //     response.PlayerPrefabHash = null;
        
    //     // Debug.Log(response);

    //     //PlayerObjectをスポーンする位置(nullの場合Vector3.zero)
    //     var position = new Vector3(0, 0, 80.0f);
    //     Debug.Log(NetworkManager.Singleton.ConnectedClients.Count);
    //     if (NetworkManager.Singleton.ConnectedClients.Count == 0) {
    //         position.x = -25.0f;
    //     } else {
    //         position.x = 25.0f;
    //     }
    //     response.Position = position;

    //     //PlayerObjectをスポーン時の回転 (nullの場合Quaternion.identity)
    //     response.Rotation = Quaternion.identity;

    //     response.Pending = false;
    // }
}
