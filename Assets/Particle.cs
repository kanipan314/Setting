using UnityEngine;

public class Particle : MonoBehaviour
{
    //���ł���܂ł̎���
    private float lifeTime;
    //���ł���܂ł̎c�莞��
    private float leftlifeTime;
    //�ړ���
    private Vector3 velocity;
    //����Scale
    private Vector3 defaultScale;
    // �����ʒu
    public Vector3 initialPosition;

    public void PlayerPosition(Vector3 MoveToPosition)
    {
        initialPosition.x = MoveToPosition.x;
        initialPosition.y = MoveToPosition.y;
    }

    // Start is called before the first frame update
    void Start()
    {
        //���ł���܂ł̎��Ԃ�0.3�b�Ƃ���
        lifeTime = 0.3f;
        //�c�莞�Ԃ�������
        leftlifeTime = lifeTime;
        //���݂�Scale���L�^
        defaultScale = transform.localScale;
        //�����_���Ō��܂�ړ��ʂ̍ő�l
        float maxVelocity = 5;
        //�e�����փ����_���Ŕ�΂�
        velocity = new Vector3(
            Random.Range(-maxVelocity, maxVelocity),
            Random.Range(-maxVelocity, maxVelocity),
            0
            );

    }

    // Update is called once per frame
    void Update()
    {
        //�c�莞�Ԃ��J�E���g�_�E��
        leftlifeTime -= Time.deltaTime;
        // ���g�̍��W���ړ�
        transform.position += velocity * Time.deltaTime;
        //�c�莞�ԂŃh���h��������
        transform.localScale = Vector3.Lerp(new Vector3(0,0,0),defaultScale, leftlifeTime / lifeTime);

        //�c�莞�Ԃ��O�ȉ��ɂȂ����玩�g�̃Q�[���I�u�W�F�N�g������
        if (leftlifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
