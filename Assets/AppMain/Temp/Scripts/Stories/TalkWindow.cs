using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Unity.VisualScripting;
using DG.Tweening;
using System.Data.Common;

// TODO if (... == true)
public class TalkWindow : MonoBehaviour {
    // 会話パラメータ.
    // [System.Serializable]
    // public class StoryData {
    //     // 表示名.
    //     public string Name = "";
    //     // 会話内容.
    //     [Multiline(3)] public string Talk = "";
    // }

    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private TextMeshProUGUI talkText = null;
    // 次ページへ表示画像.
    [SerializeField] private Image nextArrow = null;
    // キャラクターデータ.
    // [SerializeField] CharacterData data = null;
    [SerializeField] private CharacterData characterData = null;
    // 背景データ.
    // [SerializeField] BackgroundData bgData = null;
    [SerializeField] private BackgroundData backgroundData = null;
    // 左キャラクター画像
    [SerializeField] private Image leftCharacterImage = null;
    // 真ん中キャラクター画像
    [SerializeField] private Image centerCharacterImage = null;
    // 右キャラクター画像
    [SerializeField] private Image rightCharacterImage = null;
    // 背景画像.
    // [SerializeField] Image bgImage = null;
    [SerializeField] private Image backgroundImage = null;
    // // 背景トランジション.
    // [SerializeField] private UITransition bgTransition = null;
    // 会話パラメータリスト.
    // [SerializeField] private List<StoryData> talks = new List<StoryData>();
    // CSV Reader
    [SerializeField] CSVReader csvReader = null;
    // CSVFile名
    [SerializeField] private string csvName = "";
    
    // 次へフラグ.
    private bool goToNextPage = false;
    // 次へ行けるフラグ.
    private bool currentPageCompleted = false;
    // スキップフラグ.
    // private bool isSkip = false;
    private bool isSkiped = false;
    // 現在の左キャラクター情報
    private string currentLeft = "";
    // 現在の真ん中キャラクター情報
    private string currentCenter = "";
    // 現在の右キャラクター情報
    private string currentRight = "";

    private List<StoryData> talks = new List<StoryData>();
    // タグ判定用
    private bool isInTag = false;
    string tagStrings = "";

    // private void Awake() {
    //     SetCharacter(null).Forget();
    //     // TalkWindowTransition.gameObject.SetActive(false);
    // }
    
    private void Start() {
        // 後で消す
        StartTalk();
    }

    private async void StartTalk() {
        // await Open();
        Open();
        talks = csvReader.GetCSVData(csvName);
        await TalkStart(talks);
        // await Close();
        Debug.Log("テスト終了");
    }

    // 会話の開始.
    // public async UniTask TalkStart(List<StoryData> talkList, float wordInterval = 0.1f) {
    private async UniTask TalkStart(List<StoryData> talkList, float wordInterval = 0.1f) {
        currentLeft = "";
        currentCenter = "";
        currentRight = "";

        foreach (var talk in talkList) {
            if (string.IsNullOrEmpty(talk.Place) == false) {
                // await SetBackground(talk.Place, false);
                SetBackground(talk.Place);
            }
            // nameText.text = talk.Name;
            // nameText.text = characterData.GetCharacterName(talk.Name);
            nameText.text = characterData.GetCharacterName(int.Parse(talk.Name));
            talkText.text = "";
            goToNextPage = false;
            currentPageCompleted = false;
            isSkiped = false;
            // SetActive以外に変更
            // nextArrow.gameObject.SetActive(false);
            nextArrow.enabled = false;
            await SetCharacter(talk);

            await UniTask.Delay((int)(0.5f * 1000f));

            foreach (char word in talk.Talk) {
                // タグ判定用
                bool isCloseTag = false;
                if (word.ToString() == "<") {
                    Debug.Log("<です。");
                    isInTag = true;
                } else if (word.ToString() == ">") {
                    Debug.Log(">です。");
                    isInTag = false;
                    isCloseTag = true;
                }

                if (isInTag == false && isCloseTag == false && string.IsNullOrEmpty(tagStrings) == false) {
                    var _word = tagStrings + word;
                    talkText.text += _word;
                    tagStrings = "";
                } else if (isInTag == true || isCloseTag == true) {
                    tagStrings += word;
                    Debug.Log("Tab内です。");
                    continue;
                } else {
                    talkText.text += word;
                }
                // talkText.text += word;
                await UniTask.Delay((int)(wordInterval * 1000f));

                if (isSkiped == true) {
                    talkText.text = talk.Talk;
                    break;
                }
            }
            
            // print("Hello");

            currentPageCompleted = true;
            // SetActive以外に変更
            // nextArrow.gameObject.SetActive(true);
            nextArrow.enabled = true;
            await UniTask.WaitUntil(() => goToNextPage == true);
        }
    }

    // 次へボタンクリックコールバック.
    public void OnNextButtonClicked() {
        if (currentPageCompleted == true) goToNextPage = true;
        else isSkiped = true;
    }

    /// <summary>
    /// ウィンドウを開く
    /// </summary>
    /// <param name="initName"></param>
    /// <param name="initText"></param>
    /// <returns></returns>
    // public async UniTask Open(string initName = "", string initText = "") {
    private void Open(string initName = "", string initText = "") {
        SetCharacter(null).Forget();
        nameText.text = initName;
        talkText.text = initText;
        // nextArrow.gameObject.SetActive(false);
        nextArrow.enabled = false;
        // talkWindowTransition.gameObject.SetActive(true);
        // await talkWindowTransition.TransitionInWait();
    }

    // ウィンドウを閉じる
    // // public async UniTask Close()
    // private void Close() {
    //     // await talkWindowTransition.TransitionOutWait();
    //     // talkWindowTransition.gameObject.SetActive(false);
    // }

    // キャラクター画像の設定
    private async UniTask SetCharacter(StoryData storyData) {
        // nullなら全て消す
        if (storyData == null) {
            // leftCharacterImage.gameObject.SetActive(false);
            // centerCharacterImage.gameObject.SetActive(false);
            // rightCharacterImage.gameObject.SetActive(false);
            leftCharacterImage.enabled = false;
            centerCharacterImage.enabled = false;
            rightCharacterImage.enabled = false;
            return;
        }

        var tasks = new List<UniTask>();
        bool hideLeft = false;
        bool hideCenter = false;
        bool hideRight = false;

        // 値がない場合は非表示にする.
        // 値が前回と違う場合は画像を変更して表示.
        // 値が同じ場合は変化なし.
        
        Debug.Log("currentLeft: " + currentLeft);
        Debug.Log("currentCenter: " + currentCenter);
        Debug.Log("currentRight: " + currentRight);
        Debug.Log("storyData.LeftCharacter: " + storyData.LeftCharacter);
        Debug.Log("storyData.CenterCharacter: " + storyData.CenterCharacter);
        Debug.Log("storyData.RightCharacter: " + storyData.RightCharacter);

        // 左キャラクター設定
        // if (string.IsNullOrEmpty(storyData.Left) == true) {
        if (string.IsNullOrEmpty(storyData.LeftCharacter) == true) {
            Debug.Log("左: 非表示");

            // tasks.Add(leftCharacterImageTransition.TransitionOutWait());
            hideLeft = true;
        } else if (currentLeft != storyData.LeftCharacter) {
            Debug.Log("左: 画像変更");
            
            // var img = characterData.GetCharacterSprite(storyData.LeftCharacter);
            Sprite characterSprite = characterData.GetCharacterSprite(storyData.LeftCharacter);
            leftCharacterImage.sprite = characterSprite;
            // leftCharacterImage.gameObject.SetActive(true);
            leftCharacterImage.enabled = true;
            // tasks.Add(leftCharacterImageTransition.TransitionOutWait());

            currentLeft = storyData.LeftCharacter;
        } else {
            Debug.Log("左: キャラクター画像の変化なし");
        }

        // 真ん中キャラクター設定
        // if (string.IsNullOrEmpty(storyData.Center) == true) {
        if (string.IsNullOrEmpty(storyData.CenterCharacter) == true) {
            Debug.Log("真ん中: 非表示");

            // tasks.Add(centerCharacterImageTransition.TransitionOutWait());
            hideCenter = true;
        } else if (currentCenter != storyData.CenterCharacter) {
            Debug.Log("真ん中: 画像変更");

            // var img = characterData.GetCharacterSprite(storyData.CenterCharacter);
            Sprite characterSprite = characterData.GetCharacterSprite(storyData.CenterCharacter);
            centerCharacterImage.sprite = characterSprite;
            // centerCharacterImage.gameObject.SetActive(true);
            centerCharacterImage.enabled = true;
            // tasks.Add(centerCharacterImageTransition.TransitionOutWait());

            currentCenter = storyData.CenterCharacter;
        } else {
            Debug.Log("真ん中: キャラクター画像の変化なし");
        }
        
        // 右キャラクター設定
        // if (string.IsNullOrEmpty(storyData.Right) == true) {
        if (string.IsNullOrEmpty(storyData.RightCharacter) == true) {
            Debug.Log("右: 非表示");

            // tasks.Add(rightCharacterImageTransition.TransitionOutWait());
            hideRight = true;
        } else if (currentRight != storyData.RightCharacter) {
            Debug.Log("右: 画像変更");

            // var img = characterData.GetCharacterSprite(storyData.RightCharacter);
            Sprite characterSprite = characterData.GetCharacterSprite(storyData.RightCharacter);
            rightCharacterImage.sprite = characterSprite;
            // rightCharacterImage.gameObject.SetActive(true);
            rightCharacterImage.enabled = true;
            // tasks.Add(rightCharacterImageTransition.TransitionOutWait());

            currentRight = storyData.RightCharacter;
        } else {
            Debug.Log("右: キャラクター画像の変化なし");
        }

        // 待機.
        await UniTask.WhenAll(tasks);

        // 消したいキャラクターを消す.
        // if (hideLeft == true) leftCharacterImage.gameObject.SetActive(false);
        // if (hideCenter == true) centerCharacterImage.gameObject.SetActive(false);
        // if (hideRight == true) rightCharacterImage.gameObject.SetActive(false);
        if (hideLeft == true) leftCharacterImage.enabled = false;
        if (hideCenter == true) centerCharacterImage.enabled = false;
        if (hideRight == true) rightCharacterImage.enabled = false;
    }

    // 背景の設定
    // public async UniTask SetBackground(string place, bool isImmediate) {
    // private async UniTask SetBackground(string place, bool isImmediate) {
    private void SetBackground(string place) {
        // var sp = backgroundData.GetSprite(place);
        Sprite backgroundSprite = backgroundData.GetBackgroundSprite(place);
        // bgTransition.gameObject.SetActive(true);

        // var currentBg = bgImage.sprite.name;
        string currentBackground = backgroundImage.sprite.name;
        // if (currentBg == sp.name)
        if (currentBackground == backgroundSprite.name) {
            Debug.Log("同じ背景なので変更をスキップします");
            return;
        }

        // if (isImmediate == false) {
        //     await bgTransition.TransitionOutWait();
        //     backgroundImage.sprite = backgroundSprite;
        //     await bgTransition.TransitionInWait();
        // } else {
        //     backgroundImage.sprite = backgroundSprite;
        // }
        backgroundImage.sprite = backgroundSprite;
    }
}
