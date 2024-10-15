using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSelectionDB", menuName = "ScriptableObjects/CharacterSelection Database")]
public class CharacterSelectionDB : ScriptableObject {
    [SerializeField, TextArea(1, 9)]
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

    [SerializeField] private List<string> _governmentNameTexts = null;
    [SerializeField] private List<Sprite> _governmentNameImages = null;
    [SerializeField, Multiline(2)] private List<string> _governmentDescriptions = null;

    [SerializeField] private List<string> _labNameTexts = null;
    [SerializeField] private List<Sprite> _labNameImages = null;
    [SerializeField, Multiline(2)] private List<string> _labDescriptions = null;

    [SerializeField] private List<Sprite> _uniquePuzzleNames = null;
    [SerializeField] private List<string> _uniquePuzzleDescriptions = null;

    [SerializeField] private List<Sprite> _explosionNames = null;
    [SerializeField] private List<string> _explosionDescriptions = null;

    public List<string> CharacterDescriptions { get => _characterDescriptions; set => _characterDescriptions = value; }
    
    public List<string> GovernmentNameTexts { get => _governmentNameTexts; set => _governmentNameTexts = value; }
    public List<Sprite> GovernmentNameImages { get => _governmentNameImages; set => _governmentNameImages = value; }
    public List<string> GovernmentDescriptions { get => _governmentDescriptions; set => _governmentDescriptions = value; }

    public List<string> LabNameTexts { get => _labNameTexts; set => _labNameTexts = value; }
    public List<Sprite> LabNameImages { get => _labNameImages; set => _labNameImages = value; }
    public List<string> LabDescriptions { get => _labDescriptions; set => _labDescriptions = value; }

    public List<Sprite> UniquePuzzleNames { get => _uniquePuzzleNames; set => _uniquePuzzleNames = value; }
    public List<string> UniquePuzzleDescriptions { get => _uniquePuzzleDescriptions; set => _uniquePuzzleDescriptions = value; }

    public List<Sprite> ExplosionNames { get => _explosionNames; set => _explosionNames = value; }
    public List<string> ExplosionDescriptions { get => _explosionDescriptions; set => _explosionDescriptions = value; }
}
