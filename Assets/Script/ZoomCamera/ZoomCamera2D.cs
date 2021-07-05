using UnityEngine;
using System.Collections;

public class ZoomCamera2D : MonoBehaviour
{

    [SerializeField]
    private Vector2 offset = new Vector2(1, 1);
    [SerializeField]
    private float DistanceSize = 0.0f;

    private float screenAspect = 0;
    private Camera _camera = null;
    public Transform LeftBottom;
    public Transform RightTop;

    void Awake()
    {
        screenAspect = (float)Screen.height / Screen.width;
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        UpdateCameraPosition();
        UpdateOrthographicSize();
    }

    void UpdateCameraPosition()
    {
        // 2�_�Ԃ̒��S�_����J�����̈ʒu���X�V
        Vector3 center = Vector3.Lerp(LeftBottom.position, RightTop.position, 0.5f);
        transform.position = center + Vector3.forward * -10;
    }

    void UpdateOrthographicSize()
    {
        // �Q�_�Ԃ̃x�N�g�����擾
        Vector3 targetsVector = AbsPositionDiff(LeftBottom, RightTop) + (Vector3)offset;

        // �A�X�y�N�g�䂪�c���Ȃ�y�̔����A�����Ȃ�x�ƃA�X�y�N�g��ŃJ�����̃T�C�Y���X�V
        float targetsAspect = targetsVector.y / targetsVector.x;
        float targetOrthographicSize = 0;
        if (screenAspect < targetsAspect)
        {
            targetOrthographicSize = targetsVector.y * 0.5f;
        }
        else
        {
            targetOrthographicSize = targetsVector.x * (1 / _camera.aspect) * 0.5f;
        }

        //�߂������ꍇ������ۂ�
        targetOrthographicSize = Mathf.Max(targetOrthographicSize, DistanceSize);

        _camera.orthographicSize = targetOrthographicSize;
    }

    Vector3 AbsPositionDiff(Transform target1, Transform target2)
    {
        Vector3 targetsDiff = target1.position - target2.position;
        return new Vector3(Mathf.Abs(targetsDiff.x), Mathf.Abs(targetsDiff.y));
    }
}