using UnityEngine;

/// <summary>
/// �v�Z�Ɋւ���Utility�N���X
/// </summary>
public class MathUtils
{
    /// <summary>
    /// �~�Ɖ~�̓����蔻��
    /// </summary>
    /// <param name="target">���肷����W</param>
    /// <param name="radius1">��̔��a</param>
    /// <param name="radius2">�~�̔��a</param>
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
    /// �~�Ɖ~�̓����蔻��
    /// </summary>
    /// <param name="target">���肷����W</param>
    /// <param name="center">��̒��S���W</param>
    /// <param name="radius1">��̔���</param>
    /// <param name="radius2">�~�̔���</param>
    /// <returns></returns>
    public static bool IsInsideOfCircle(Vector2 target, Vector2 center, float radius1, float radius2)
    {
        var diff = target - center;
        return IsInsideOfCircle(diff, radius1, radius2);
    }

    /// <summary>
    /// �~�Ɛ�`�̓����蔻����s��
    /// </summary>
    /// <param name="target">���肷����W</param>
    /// <param name="center">��̒��S���W</param>
    /// <param name="startDeg">��`�̊J�n�p(�x���@)</param>
    /// <param name="endDeg">��`�̏I���p(�x���@)</param>
    /// <param name="radius1">��`�̔��a</param>
    /// <param name="radius2">�~�̔��a</param>
    /// <returns></returns>
    public static bool IsInsideOfSector(Vector2 target, Vector2 center, float startDeg, float endDeg, float radius1, float radius2)
    {
        var diff = target - center;
        var startRad = startDeg * Mathf.Deg2Rad;
        var endRad = endDeg * Mathf.Deg2Rad;
        var startVec = new Vector2(Mathf.Cos(startRad), Mathf.Sin(startRad));
        var endVec = new Vector2(Mathf.Cos(endRad), Mathf.Sin(endRad));

        // �~�̓��O����

        if (!IsInsideOfCircle(diff, radius1, radius2))
        {
            return false;
        }

        // ��^�̊p�x��180�x�����̏ꍇ
        if (GetCross2d(startVec, endVec) > 0)
        {
            // diff �� startVec ��荶�� *����* diff �� endVec ���E���̎�
            if (GetCross2d(startVec, diff) >= -radius2 && GetCross2d(endVec, diff) <= radius2)
            {
                return true;
            }
            return false;
        }
        // ��^�̊p�x��180�x�ȏ�̏ꍇ
        else
        {
            // diff �� startVec ��荶�� *�܂���* diff �� endVec ���E���̎�
            if (GetCross2d(startVec, diff) >= -radius2 || GetCross2d(endVec, diff) <= radius2)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Vector2�̊O��
    /// </summary>
    /// <param name="a">�Ώ�a</param>
    /// <param name="b">�Ώ�b</param>
    /// <returns>�O�ς̌���</returns>
    static float GetCross2d(Vector2 a, Vector2 b)
    {
        return GetCross2d(a.x, a.y, b.x, b.y);
    }

    /// <summary>
    /// float�̊O��
    /// </summary>
    /// <param name="ax">�Ώ�a��x</param>
    /// <param name="ay">�Ώ�a��y</param>
    /// <param name="bx">�Ώ�b��x</param>
    /// <param name="by">�Ώ�b��y</param>
    /// <returns>�O�ς̌���</returns>
    static float GetCross2d(float ax, float ay, float bx, float by)
    {
        return ax * by - bx * ay;
    }
}