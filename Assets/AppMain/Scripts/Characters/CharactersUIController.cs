using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class CharactersUIController : MonoBehaviour {
    #region
    private CancellationToken _token = default;
    private AudioSource _audioSource_SE = null;
    private AudioClip _audioClip_SE = null;
    private bool _isChangingScene = false;
    private List<CharacterButton> _characterButtonScripts = null;
    private int _previousCharacterIndex = 0;

    private List<string> _characterDescriptions = new List<string>() {
        "赤の政府のマッドサイエンティスト。本名はピエリス・ポドストロマ。\n"
        + "エルフの証であるとんがったお耳と、とっても目立つ耐薬品性コーティングを施したお洋服がチャームポイント。"
        + "心優しい性格でとても研究熱心だけど、いつもガスマスクで顔を覆っているのでみんなから怖がられちゃう。\n"
        + "じいじ達の夢を受け継いで、国中の皆に効く薬を開発し、治せない病気のない世界を目指す。",

        "橙の政府のマッドサイエンティスト。当時7歳という史上最年少でこの座に就いた天才。\n"
        + "身体にあるケモノの特徴と独特な縫合痕は、自分自身の身体で人体実験を行なったことに起因する。"
        + "また、その後遺症により、時々人格が別の何かと入れ替わってしまう。\n"
        + "身体改造は薬よりも安価で永続的な効果をもたらすと考えており、合法的に身体能力や治癒能力を高められる制度の実現を目指す。",
        
        "黄の政府のマッドサイエンティスト。宙空都市テルージアのアイドルのような存在。\n"
        + "自身よりも美しい星々に強い憧れを抱いており彼らの元へいつか辿り着きたいと考えている。"
        + "星を読んで未来を予測したり、小さな衛星の軌道を操作できるけど、実は本人もその仕組みはよくわかっていない。\n"
        + "一刻も早く平和を取り戻し、スペースシップに乗り、美しい星雲を臨める新天地を目指す。",

        "緑の政府のマッドサイエンティスト。40年前にテュレーネンから嫁いできた。\n"
        + "世界一美味しい野菜を育てるためならどんな手段も厭わず、雑草と見なしたものを根絶やしにする。"
        + "他者の草木をも枯らす農薬を扱い、たとえ自治区の植物学者たちから疎まれようとも、彼女の歩みが止まることはない。\n"
        + "愛した夫の遺志を継ぎ、美味しい食材が当たり前に行き渡る世界を目指す。",

        "水の政府のマッドサイエンティスト。5年前に突如人前に現れた、元引きこもりのオタク。\n"
        + "非常に物腰の柔らかい（笑）青年で、その気にさえなれば、どんなネットワークもハックできる。"
        + "人の5億倍プライドが高いので、高いヒールを履き、超高層ビルの最上階に住んで常に人を見下ろして過ごしている。\n"
        + "全ての人間を見返し、全ての自治区を支配下に置くことを目指す。",

        "青の政府のマッドサイエンティスト。なぜか歴史書に度々登場する、謎多き人物。\n"
        + "のんびりとした性格で、口数は少ない。顔にある傷は昔受けた呪いの跡らしい。"
        + "リアンフリーデンの過去について、何か重大な真実を抱えているようだが......？\n"
        + "悲涙エネルギーを研究するものとして、誰よりも悲しみを理解しており、人々の悲しみを和らげることを目指す。",

        "紫の政府のマッドサイエンティスト。実は抜け忍で、10年の雲隠れを経て今に至る。\n"
        + "くたびれたおじさんといった風貌だが情には厚い。国の未来よりも自分の自治区の未来を第一に考えている。"
        + "霊や魂と話す能力を持っており死者と会話する姿はまるでオカルト信者だ、と噂されるが、本人は生粋のリアリスト。\n"
        + "統治の混乱した自治区を鎮め、平和な世の中を取り戻すことを目指す。",

        "桃の政府のマッドサイエンティスト。地下探索が大好きなおてんばガール。\n"
        + "マッドサイエンティストとしてはまだまだ未熟だが、彼女を慕う親衛隊に支えられ「ゆめ」に向かって一直線に進む。"
        + "騒音被害で怒られてもへっちゃら！今日もお気に入りのもぐら隊長に乗って、もぐら隊と元気にしゅっぱーつ！\n"
        + "国中の地面を掘り返して更地にし、地下にある未知のエネルギーを発見することを目指す。",

        "中央中立政府の第88代目総統。999年続くこの国の均衡を維持する役目のはずだが、突如侵攻を宣言。他の自治区を争いに巻き込む。\n"
        + "非常に冷徹で真意の見えない行動を取る一方、ハルキやシモンら人工生命体達を家族のように慕っている。趣味は人形。\n"
        + "科学を信仰する国で唯一、科学研究が許されず国の統括を任されている黒の政府。そこで生まれた彼女は、旧体制からの解放を目指す。",
    };
    private int _informatioinIndex = 0;
    #endregion

    #region
    [SerializeField] private CharactersController _charactersController = null;
    [Header("そのシーンにローディングパネルが存在しないときはnullでOK")]
    [SerializeField] private GameObject _loadingPanel = null;
    [SerializeField] private Button _backButton = null;
    [SerializeField] private List<Button> _characterButtons = null;
    [SerializeField] private Button _zoomButton = null;
    [SerializeField] private Button _callButton = null;
    
    [SerializeField] private Image _nameImage = null;
    [SerializeField] private Image _majorImage = null;

    [SerializeField] private TextMeshProUGUI _characterDescriptionText = null;
    [SerializeField] private Button _triangleLeftButton = null;
    [SerializeField] private Button _triangleRightButton = null;
    [Header("0...Lab, 1...Skill, 2...Level")]
    [SerializeField] private List<GameObject> _informationPanels = null;

    // TODO 完全に実装したら消す
    [SerializeField] private GameObject _notYetInstalledPanel = null;
    [SerializeField] private Button _closeButton = null;
    #endregion

    private void Start() {
        _token = this.GetCancellationTokenOnDestroy();

        _audioSource_SE = SE.Instance.GetComponent<AudioSource>();

        #region // ボタンの設定
        ChangeAlphaHitThreshold(_backButton, 1.0f);
        _backButton.onClick.AddListener(() => OnBackButtonClicked());

        _characterButtonScripts = new List<CharacterButton>(_characterButtons.Count);
        for (var i = 0; i < _characterButtons.Count; i++) {
            var index = i;
            ChangeAlphaHitThreshold(_characterButtons[i], 1.0f);
            _characterButtons[i].onClick.AddListener(() => OnCharacterButtonClicked(index));
            // FIXME これ、リストへの追加の仕方おかしいかも
            _characterButtonScripts.Add(_characterButtons[i].GetComponent<CharacterButton>());
        }

        ChangeAlphaHitThreshold(_zoomButton, 1.0f);
        _zoomButton.onClick.AddListener(() => OnZoomButtonClicked());
        ChangeAlphaHitThreshold(_callButton, 1.0f);
        _callButton.onClick.AddListener(() => OnCallButtonClicked());

        _triangleLeftButton.onClick.AddListener(() => OnTriangleButtonClicked(-1));
        _triangleRightButton.onClick.AddListener(() => OnTriangleButtonClicked(1));
        // TODO informationPanelsの初期化処理 (LINQ?)
        // _informationPanels[_informatioinIndex].SetActive(true);
        // _informationPanels[_informatioinIndex].SetActive(false);
        // _informationPanels[_informatioinIndex].SetActive(false);
        #endregion

        _previousCharacterIndex = GameDirector.Instance.SelectedCharacterIndex;
        _characterButtonScripts[_previousCharacterIndex].SetSelection(true);

        UpdateUI(_previousCharacterIndex);

        // TODO 完全に実装したら消す
        _notYetInstalledPanel.SetActive(false);
        _closeButton.onClick.AddListener(() => OnCloseButtonClicked());
    }

    private void ChangeAlphaHitThreshold(Button button, float alpha) {
        Image image = button.GetComponent<Image>();
        image.alphaHitTestMinimumThreshold = alpha;
    }

    // TODO 完全に実装したら消す
    private void OnCloseButtonClicked() {
        if (!_notYetInstalledPanel.activeSelf) return;

        _notYetInstalledPanel.SetActive(false);
    }

    private void OnBackButtonClicked() {
        if (_isChangingScene) return;

        _isChangingScene = true;
        _audioClip_SE = SE.Instance.audioClips[0];
        _audioSource_SE.PlayOneShot(_audioClip_SE);
        // TODO durationの変更
        GoNextSceneAsync(0, "Menu", false).Forget();
    }

    private void OnCharacterButtonClicked(int index) {
        _characterButtonScripts[_previousCharacterIndex].SetSelection(false);

        _charactersController.UpdatePlayerCharacter(index);

        UpdateUI(index);

        _characterButtonScripts[index].SetSelection(true);
        _previousCharacterIndex = index;
    }

    private void UpdateUI(int index) {
        _nameImage.sprite = _charactersController.Character.NameSprite;
        _majorImage.sprite = _charactersController.Character.MajorSprite;

        _characterDescriptionText.text = _characterDescriptions[index];
    }

    private void OnTriangleButtonClicked(int number) {
        Debug.Log(number);
        _informationPanels[_informatioinIndex].SetActive(false);

        _informatioinIndex += number;
        if (_informatioinIndex < 0)
            _informatioinIndex = _informationPanels.Count - 1;
        else if (_informatioinIndex >= _informationPanels.Count)
            _informatioinIndex = 0;
        _informationPanels[_informatioinIndex].SetActive(true);
    }

    private void OnZoomButtonClicked() {
        if (_isChangingScene) return;

        // UIを非表示にする処理
        // キャラクターを大きくする処理

        // TODO 完全に実装したら消す
        if (_notYetInstalledPanel.activeSelf) return;
        // _warningSentence.text = _warningTexts[0];
        _notYetInstalledPanel.SetActive(true);
    }

    private void OnCallButtonClicked() {
        GameDirector.Instance.SelectedCharacterIndex = _previousCharacterIndex;
    }

    private async UniTaskVoid GoNextSceneAsync(float duration, string nextSceneName, bool isShowLoadingPanel) {
        // ローディングパネルが出る前にすること

        await UniTask.Delay((int)(duration * 1000), cancellationToken: _token);

        // Debug.Log("Go to " + nextSceneName);
        
        // ローディングパネルがある時
        if (isShowLoadingPanel && _loadingPanel != null) {
            _loadingPanel.SetActive(true);
            AsyncOperation async = SceneManager.LoadSceneAsync(nextSceneName);
            await UniTask.WaitUntil(() => async.isDone, cancellationToken: _token);
        // ローディングパネルがない時
        } else {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
