using UnityEngine;

/// <summary>
/// 計算に関するUtilityクラス
/// </summary>
public class MathUtils
{
    /// <summary>
    /// 円と円の当たり判定
    /// </summary>
    /// <param name="target">判定する座標</param>
    /// <param name="radius1">扇の半径</param>
    /// <param name="radius2">円の半径</param>
    /// <returns></returns>
    public static bool IsInsideOfCircle(Vector2 target, float radius1, float radius2)
    {
        if (target.magnitude <= radius1 + radius2)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 円と円の当たり判定
    /// </summary>
    /// <param name="target">判定する座標</param>
    /// <param name="center">扇の中心座標</param>
    /// <param name="radius1">扇の判定</param>
    /// <param name="radius2">円の判定</param>
    /// <returns></returns>
    public static bool IsInsideOfCircle(Vector2 target, Vector2 center, float radius1, float radius2)
    {
        var diff = target - center;
        return IsInsideOfCircle(diff, radius1, radius2);
    }

    /// <summary>
    /// 円と扇形の当たり判定を行う
    /// </summary>
    /// <param name="target">判定する座標</param>
    /// <param name="center">扇の中心座標</param>
    /// <param name="startDeg">扇形の開始角(度数法)</param>
    /// <param name="endDeg">扇形の終了角(度数法)</param>
    /// <param name="radius1">扇形の半径</param>
    /// <param name="radius2">円の半径</param>
    /// <returns></returns>
    public static bool IsInsideOfSector(Vector2 target, Vector2 center, float startDeg, float endDeg, float radius1, float radius2)
    {
        var diff = target - center;
        var startRad = startDeg * Mathf.Deg2Rad;
        var endRad = endDeg * Mathf.Deg2Rad;
        var startVec = new Vector2(Mathf.Cos(startRad), Mathf.Sin(startRad));
        var endVec = new Vector2(Mathf.Cos(endRad), Mathf.Sin(endRad));

        // 円の内外判定

        if (!IsInsideOfCircle(diff, radius1, radius2))
        {
            return false;
        }

        // 扇型の角度が180度未満の場合
        if (GetCross2d(startVec, endVec) > 0)
        {
            // diff が startVec より左側 *かつ* diff が endVec より右側の時
            if (GetCross2d(startVec, diff) >= -radius2 && GetCross2d(endVec, diff) <= radius2)
            {
                return true;
            }
            return false;
        }
        // 扇型の角度が180度以上の場合
        else
        {
            // diff が startVec より左側 *または* diff が endVec より右側の時
            if (GetCross2d(startVec, diff) >= -radius2 || GetCross2d(endVec, diff) <= radius2)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Vector2の外積
    /// </summary>
    /// <param name="a">対象a</param>
    /// <param name="b">対象b</param>
    /// <returns>外積の結果</returns>
    static float GetCross2d(Vector2 a, Vector2 b)
    {
        return GetCross2d(a.x, a.y, b.x, b.y);
    }

    /// <summary>
    /// floatの外積
    /// </summary>
    /// <param name="ax">対象aのx</param>
    /// <param name="ay">対象aのy</param>
    /// <param name="bx">対象bのx</param>
    /// <param name="by">対象bのy</param>
    /// <returns>外積の結果</returns>
    static float GetCross2d(float ax, float ay, float bx, float by)
    {
        return ax * by - bx * ay;
    }
}