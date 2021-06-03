using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private RangeObject ro;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�I�u�W�F�N�g�ɍ��킹�ĉ�]
        if (ro.FunCollision())
        {
            //��͈͓̔��ɂ����ԋ߂��I�u�W�F�N�g�̍��W�擾
            var vec1 = ro.FunCollision().transform.position;   
            
            //�v���C���[�������璆�S���W�ɍ��킹���������W�����Z����
            if(ro.FunCollision().tag == "Player")
            {
                var player = ro.FunCollision().GetComponent<Player>();
                vec1 += player.Offset;
            }

            var vec2 = transform.position;
            var angle = GetAim(vec1, vec2);
            angle = (angle + 360) % 360;
            //�L�����̌����ɍ��킹�Ĕ��]
            if (ro.Direction.x == 1.0f)
            {
                angle += 180.0f;
            }
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.right);
        }
    }

    /// <summary>
    /// 2�_�Ԃ̊p�x�̎擾
    /// </summary>
    /// <param name="p1">�I�u�W�F�N�g�̍��W</param>
    /// <param name="p2">�I�u�W�F�N�g�̍��W</param>
    /// <returns>�p�x</returns>
    private float GetAim(Vector2 p1, Vector2 p2)
    {
        float dx = p2.x - p1.x;
        float dy = p2.y - p1.y;
        float rad = Mathf.Atan2(dy, dx);

        return rad * Mathf.Rad2Deg;
    }
}
