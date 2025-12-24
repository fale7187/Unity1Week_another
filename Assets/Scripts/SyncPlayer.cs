using UnityEngine;

public class SyncPlayer : MonoBehaviour
{
    [Header("���삷��L�����N�^�[��Rigidbody")]
    public Rigidbody2D playerRb; // ��l��
    public Rigidbody2D shadowRb; // �e�i������l�j

    [Header("�ړ����x")]
    public float moveSpeed = 5f;

    void Update()
    {
        // 1. ���͂��󂯎�� (WASD �܂��� ���L�[)
        // GetAxisRaw���g���ƁA�������Ȃ��p�L�b�Ɠ����܂�
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // 2. �ړ��x�N�g������� (�΂߈ړ��̑��x���߂�h�����߂�normalized)
        Vector2 movement = new Vector2(x, y).normalized * moveSpeed;

        // 3. 2�l�ɑ��x��K�p����
        // �ǂ��炩��Rigidbody���ݒ肳��Ă��Ȃ��Ă��G���[�ɂȂ�Ȃ��悤�Ƀ`�F�b�N
        if (playerRb != null)
        {
            playerRb.linearVelocity = movement;
        }

        if (shadowRb != null)
        {
            shadowRb.linearVelocity = movement;
        }
    }
}