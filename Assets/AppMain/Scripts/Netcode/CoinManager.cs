using Unity.Netcode;
using UnityEngine;

public class CoinManager : NetworkBehaviour {
    // [SerializeField] private NetworkObject _characterPrefab;
    // [SerializeField] private CharacterDatabase _characterDatabase;

    // private SpriteRenderer _spriteRenderer;
    // private int _playerCount = 0;
    // private float posX = 0;

    // public override void OnNetworkSpawn() {
    //     _spriteRenderer = _characterPrefab.GetComponent<SpriteRenderer>();
    //     _playerCount++;
    //     // if (IsHost) {
    //     //     GenerateCharacter();
    //     // } else {
    //     //     GenerateCharacter();
    //     // }
    //     GenerateCharacter();
    // }


    // public void GenerateCharacter() {
    //     Debug.Log("GenerateCharacter");
    //     NetworkObject coin = Instantiate(_characterPrefab);
    //     if (_playerCount == 1) {
    //         Debug.Log("generate host image");
    //         posX = -25.0f;
    //         Debug.Log(_characterDatabase.GetCharacter(0).CharacterSprites[2]);
    //         _spriteRenderer.sprite = _characterDatabase.GetCharacter(0).CharacterSprites[2];
    //     } else if (_playerCount == 2) {
    //         Debug.Log("generate client image");
    //         Debug.Log(_characterDatabase.GetCharacter(1).CharacterSprites[2]);
    //         _spriteRenderer.sprite = _characterDatabase.GetCharacter(1).CharacterSprites[2];
    //         posX = 25.0f;
    //     }
    //     coin.transform.position = new Vector3(posX, 0, 80);
    //     coin.Spawn();
    // }
}
