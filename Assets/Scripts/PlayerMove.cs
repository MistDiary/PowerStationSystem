using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    //�����
    public GameObject playerView;

    //�ٶȣ�ÿ���ƶ�5����λ����
    public float moveSpeed = 6;
    //���ٶȣ�ÿ����ת135��
    public float angularSpeed = 135;
    //��Ծ����
    public float jumpForce = 200f;

    //ˮƽ�ӽ�������
    public float horizontalRotateSensitivity = 10;
    //��ֱ�ӽ�������
    public float verticalRotateSensitivity = 5;

    //��󸩽�
    public float maxDepressionAngle = 90;

    //�������
    public float maxElevationAngle = 25;

    //��ɫ�ĸ���
    private Rigidbody rigidbody;
    //��ɫ�ƶ�����
    private Vector3 moveDirection = Vector3.zero;
    //��Ծ�ٶ�
    public float jumpSpeed = 8.0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        View();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        //ͨ�����̻�ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //����ʸ���ƶ�һ�ξ���
        Vector3 targetPosition = transform.position + Vector3.forward * v * Time.deltaTime * moveSpeed +
                                  Vector3.right * h * Time.deltaTime * moveSpeed;
        rigidbody.MovePosition(targetPosition);
    }

    void View()
    {
        //������굽��Ļ����
        SetCursorToCentre();

        //��ǰ��ֱ�Ƕ�
        double VerticalAngle = playerView.transform.eulerAngles.x;

        //ͨ������ȡ��ֱ��ˮƽ���ֵ����Χ��-1��1
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y") * -1;

        //��ɫˮƽ��ת
        transform.Rotate(Vector3.up * h * Time.deltaTime * angularSpeed * horizontalRotateSensitivity);

        //���㱾����ת����ֱ�����ϵ�ŷ����
        double targetAngle = VerticalAngle + v * Time.deltaTime * angularSpeed * verticalRotateSensitivity;

        //��ֱ�����ӽ�����
        if (targetAngle > maxDepressionAngle && targetAngle < 360 - maxElevationAngle) return;

        //�������ֱ��������ת
        playerView.transform.Rotate(Vector3.right * v * Time.deltaTime * angularSpeed * verticalRotateSensitivity);
    }

    void SetCursorToCentre()
    { // �������δ����ʱ�������
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Jump()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        //moveDirection *= speed;
        /* if (Input.GetKeyDown(KeyCode.Space))
         {
             rigidbody.AddForce(Vector3.up * jumpForce);
         }*/
        // ʹ��Rigidbody��AddForce��������Ծ
        if (Input.GetKeyDown(KeyCode.Space) && rigidbody.velocity.y == 0)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}