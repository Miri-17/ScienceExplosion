using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterUIController : MonoBehaviour {
    #region
    [SerializeField] private CharacterDatabase _characterDatabase;

    [SerializeField] private SpriteRenderer _characterSpriteRenderer;
    [SerializeField] private TextMeshProUGUI _professionText;
    [SerializeField] private TextMeshProUGUI _nameText;
    // [SerializeField] private Image _nameImage;
    [SerializeField] private TextMeshProUGUI _skillText;
    [SerializeField] private TextMeshProUGUI _explosionText;
    [SerializeField] private TextMeshProUGUI _placeText;
    [SerializeField] private TextMeshProUGUI _affiliationText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    
    [SerializeField] private List<Button> _characterButtons;
    [SerializeField] private Button _characterSettingButton;
    [SerializeField] private Button _backgroundSettingButton;
    // 0 ... キャラ設定前, 1 ... 背景設定前, 2 ... 設定後
    [SerializeField] private List<Sprite> _settingButtonSprites;
    [SerializeField] private Button _backButton;

    // [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime = 1.0f;
    #endregion

    private int _currentIndex = 0;
    private Image _characterSettingButtonImage = null;
    private Image _backgroundSettingButtonImage = null;
    private bool _isChangeScene = false;
    private List<string> _description = new List<string>() {
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

    private void Start() {
        // UpdateCharacter()より下にするとエラーになるので注意してください
        _characterSettingButtonImage = _characterSettingButton.GetComponent<Image>();
        _backgroundSettingButtonImage = _backgroundSettingButton.GetComponent<Image>();
        UpdateCharacter(GameDirector.Instance.CharactersFirstIndex);

        for (var i = 0; i < _characterButtons.Count; i++) {
            var index = i;
            _characterButtons[i].onClick.AddListener(() => UpdateCharacter(index));
        }
        _characterSettingButton.onClick.AddListener(() => SetCharacter());
        _backgroundSettingButton.onClick.AddListener(() => SetBackground());
        _backButton.onClick.AddListener(() => OnBackButtonClicked());
    }
    
    private void UpdateCharacter(int index) {
        var character = _characterDatabase.GetCharacter(index);
        
        // 設定されていたキャラのボタンを押せなくする
        // if (_currentIndex == 0)
        //     _characterButtons[GameDirector.Instance].interactable = true;
        // // 設定されたキャラのボタンを押せなくする
        // _characterButtons[index].interactable = false;
        _currentIndex = index;
        
        _professionText.text = character.Profession;
        _nameText.text = character.Name;
        // _nameText.color = character.UniqueColor;
        // _nameImage.color = character.UniqueColor;
        // Dr. Pだけフォントサイズを上げる
        if (index == 0)
            _nameText.fontSize = 160;
        else
            _nameText.fontSize = 120;
        _skillText.text = character.Skill;
        _explosionText.text = character.Explosion;
        _placeText.text = character.Place;
        _affiliationText.text = character.AffiliationJapanese;
        _descriptionText.text = _description[index];
        _characterSpriteRenderer.sprite = character.CharacterSprites[2];

        if (index == GameDirector.Instance.SelectedCharacterIndex) {
            _characterSettingButtonImage.sprite = _settingButtonSprites[2];
            _characterSettingButton.enabled = false;
        } else {
            _characterSettingButtonImage.sprite = _settingButtonSprites[0];
            _characterSettingButton.enabled = true;
        }
        if (index == GameDirector.Instance.SelectedPlaceIndex) {
            _backgroundSettingButtonImage.sprite = _settingButtonSprites[2];
            _backgroundSettingButton.enabled = false;
        } else {
            _backgroundSettingButtonImage.sprite = _settingButtonSprites[1];
            _backgroundSettingButton.enabled = true;
        }
    }

    private void SetCharacter() {
        GameDirector.Instance.SelectedCharacterIndex = _currentIndex;
        _characterSettingButtonImage.sprite = _settingButtonSprites[2];
        _characterSettingButton.enabled = false;
    }

    private void SetBackground() {
        GameDirector.Instance.SelectedPlaceIndex = _currentIndex;
        _backgroundSettingButtonImage.sprite = _settingButtonSprites[2];
        _backgroundSettingButton.enabled = false;
    }

    private void OnBackButtonClicked() {
            if (_isChangeScene) return;

        _isChangeScene = true;
        StartCoroutine(GoBackToScene());
    }

    private IEnumerator GoBackToScene() {
        // _transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene("Characters");
    }
}
