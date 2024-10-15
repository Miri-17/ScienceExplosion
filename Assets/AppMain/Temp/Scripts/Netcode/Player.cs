using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour {
    // private void Update()
    // {
    //     //ownerの場合
    //     if (IsOwner)
    //     {
    //         // 移動入力を設定
    //         // SetMoveInputServerRpc(
    //         //         Input.GetAxisRaw("Horizontal"),
    //         //         Input.GetAxisRaw("Vertical"));
    //     }

    //     //サーバー（ホスト）の場合
    //     if (IsServer)
    //     {
    //         ServerUpdate();
    //     }
    // }

    //=================================================================
    //サーバー側で行う処理
    //=================================================================
    // サーバーだけで呼び出すUpdate
    // private void ServerUpdate()
    // {
        //移動
        // var velocity = Vector3.zero;
        // velocity.x = m_moveSpeed * m_moveInput.normalized.x;
        // velocity.z = m_moveSpeed * m_moveInput.normalized.y;
        //移動処理
        // m_rigidBody.AddForce(velocity);
    // }
}
