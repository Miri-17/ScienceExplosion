using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MenuUIController : MonoBehaviour {
    // TODO csvファイルを読み込めるように変更
    private List<string> _speeches = new List<string>() {
        "私はピエリス。みんなからDr. Pって呼ばれてるわ。\n"
        + "毒を持ってる植物を調べるのが好きなの。\n"
        + "...ねぇ、何かいい毒物を知らない？",
        "やっほー！！！ おいらはマイラ・キメラ！\n"
        + "わくわくけんきゅーじょの所長だよ。\n"
        + "あなたの体が何で作られてるか、ちょっとおれに見せてよ！",
        "ボク<sprite=3, color=#61009b>はクラックス・クルー<sprite=3, color=#61009b> 星に愛された男さ<sprite=3, color=#61009b>\n"
        + "キミ<sprite=3, color=#61009b>もボク<sprite=3, color=#61009b>の輝きに導かれてここまできたのだろう？\n"
        + "それなら、ボク<sprite=3, color=#61009b>のブロマイドをあげよう<sprite=3, color=#61009b>",
        "ごきげんよう。アタクシはミセス・グリューン。\n"
        + "プラネッタで植物栄養学の研究をしているの。\n"
        + "雑草さんには縁のない学問でしょうけれど、よろしくね。",
        "遠路はるばるトロイ州までようこそお越しくださいました。\n"
        + "私は電脳交流学者の<sprite=0, color=#61009b>・青雷です。\n"
        + "以後お見知り置きを...。",
        "ナミモロという。悲涙エネルギーの研究者だ。\n"
        + "テュレーネンを気に入ってくれることを願う。\n"
        + "何か困ったことがあったら言ってくれ...力になりたい。",
        "あー、どうも。新村八雲だ。\n"
        + "霊魂統御学を専攻している者だ。\n"
        + "これからよろしく頼む。",
        "はじめまして！ ロジーちゃんだよ！\n"
        + "みてみて！ きれいな化石を見つけたの！\n"
        + "特別にロジーちゃんのコレクションを見せてあげるね！",
        "中央政府総統、ヴィクトリア・J・グラヴィティーナだ。\n"
        + "貴様が新人か？ 期待しているぞ。\n"
        + "リアンフリーデンに栄光あれ！",
    };

    #region
    [SerializeField] private CharactersDB _charactersDB = null;
    [SerializeField] private RectTransform _flaskImage = null;
    [SerializeField] private RectTransform _chargeImage = null;
    [SerializeField] private SpriteRenderer _characterSpriteRenderer = null;
    [SerializeField] private Image _background = null;

    [SerializeField] private Image _nameImage = null;
    [SerializeField] private TextMeshProUGUI _speechText = null;
    [Header("0,1...Level")]
    [SerializeField] private List<Image> _changeColorImages = null;
    #endregion

    private void Start() {
        _flaskImage.DOAnchorPosY(23.0f, 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_flaskImage.gameObject);
        
        _chargeImage.localScale = Vector3.one * 0.8f;
        _chargeImage.DOScale(1f, 0.8f)
            .SetEase(Ease.OutCubic)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(_chargeImage.gameObject);
        
        UpdateCharacter(GameDirector.Instance.SelectedCharacterIndex);
        var character = _charactersDB.GetCharacter(GameDirector.Instance.SelectedPlaceIndex);
        _background.sprite = character.PlaceSprites[0];
    }

    // キャラクターをアップデートするためのクラスを定義し、オーバーライド関数にした方が良いかも
    // CharacterのUIController.csにも使用されている。
    private void UpdateCharacter(int index) {
        var character = _charactersDB.GetCharacter(index);

        _characterSpriteRenderer.sprite = character.CharacterSprites[0];
        _nameImage.sprite = character.NameSpriteSpeech;
        
        _speechText.text = _speeches[index];

        foreach (var changeColorImage in _changeColorImages)
            changeColorImage.color = character.UniqueColor;
    }
}
