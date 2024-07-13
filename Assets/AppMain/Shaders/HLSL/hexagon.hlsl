void Hexagon_float (
    float2 UV,
    float Scale,
    out float Hexagon,
    out float2 HexagonPosition
)

{
    float2 p = UV * Scale;
    // 六角形の横方向の大きさはsqrt(3)/2倍のほうが綺麗なため
    p.x *= 1.1547005838;
    // 画面アスペクト比による歪みの補正
    p.x *= _ScreenParams.x / _ScreenParams.y;

    // 偶数列目なら1.0
    float isTwo = frac(floor(p.x) / 2.0) * 2.0;
    // 奇数列目なら1.0
    float isOne = 1.0 - isTwo;
    // 偶数列目は縦に0.5ずらす
    p.y += isTwo * 0.5;

    // 四角形の番号
    float2 p_index1 = floor(p);
    float2 p_rect = frac(p);
    // 四角形内部の座標
    p = p_rect - 0.5;
    // タイルの右上: (+1, +1), 右上: (+1, +1), 左上: (-1, +1), 右下: (+1, -1), 左下: (-1, -1)
    float2 s = sign(p);
    // 上下左右対称にする
    p = abs(p);
    
    // 六角形の内部にある場合は1.0
    float isInHexagon = step(p.x * 1.5 + p.y, 1.0);
    // 六角形の外部にある場合は1.0
    float isOutHexagon = 1.0 - isInHexagon;

    // 四角形マスのうち、六角形の外部を補正するために使用する
    float2 p_index2 = float2(0, 0);
    p_index2 = lerp(
        float2(s.x, +step(0.0, s.y)), // 奇数列目
        float2(s.x, -step(s.y, 0.0)), // 偶数列目
        isTwo
    ) * isOutHexagon; // 六角形の外部のみ取り出す
    // 六角形の番号
    float2 hexagonIndex = p_index1 + p_index2;
    
    // 六角形グラデーションを出力
    // 六角形の内部ならp_rect、外部なら4つの六角形UVを使用
    Hexagon = lerp(p_rect, p_rect - s * float2(1.0, 0.5), isOutHexagon);

    // 六角形の座標を出力
    HexagonPosition = hexagonIndex / Scale;
}