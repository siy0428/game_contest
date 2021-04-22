using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class RangeObjectGizmosEditor
{
    /// <summary>
    /// ��̉���
    /// </summary>
    /// <param name="_object"></param>
    /// <param name="gizmoType"></param>
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected)]
    private static void DrawPointGizmos(RangeObject _object, GizmoType gizmoType)
    {
        if (_object.Radius <= 0.0f)
        {
            return;
        }

        Gizmos.color = _object.Color;

        Transform transform = _object.transform;
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation * Quaternion.AngleAxis(270.0f, Vector3.right);
        Vector3 scale = Vector3.one * _object.Radius;

        if (_object.Angle > 0.0f)
        {
            Mesh fanMesh = CreateFunMesh(_object.Angle, _object.TriangleCount);

            Gizmos.DrawMesh(fanMesh, pos, rot, scale);
        }
    }

    /// <summary>
    /// ��_�쐬
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="triangleCount">���_��</param>
    /// <returns></returns>
    private static Vector3[] CreateFanVertices(float angle, int triangleCount)
    {
        //�G���[�ݒ�
        if (angle <= 0.0f)
        {
            throw new System.ArgumentException(string.Format("�p�x�����������I angle={0}", angle));
        }
        if (triangleCount <= 0)
        {
            throw new System.ArgumentException(string.Format("�������������I triangleCount={0}", triangleCount));
        }

        angle = Mathf.Min(angle, 360.0f);

        var vertices = new List<Vector3>(triangleCount + 2);

        // �n�_
        vertices.Add(Vector3.zero);

        // Mathf.Sin()��Mathf.Cos()�Ŏg�p����̂͊p�x�ł͂Ȃ����W�A���Ȃ̂ŕϊ����Ă����B
        float radian = angle * Mathf.Deg2Rad;
        float startRad = -radian / 2;
        float incRad = radian / triangleCount;

        for (int i = 0; i < triangleCount + 1; ++i)
        {
            float currentRad = startRad + (incRad * i);

            Vector3 vertex = new Vector3(Mathf.Sin(currentRad), 0.0f, Mathf.Cos(currentRad));
            vertices.Add(vertex);
        }

        return vertices.ToArray();
    }

    /// <summary>
    /// �O�p�`�̍쐬
    /// </summary>
    /// <param name="angle">�p�x</param>
    /// <param name="triangleCount">���_��</param>
    /// <returns></returns>
    private static Mesh CreateFunMesh(float angle, int triangleCount)
    {
        var mesh = new Mesh();

        var vertices = CreateFanVertices(angle, triangleCount);

        var triangleIndexes = new List<int>(triangleCount * 3);

        for (int i = 0; i < triangleCount; ++i)
        {
            triangleIndexes.Add(0);
            triangleIndexes.Add(i + 1);
            triangleIndexes.Add(i + 2);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndexes.ToArray();

        mesh.RecalculateNormals();

        return mesh;
    }
} // class RangeObjectGizmosEditor