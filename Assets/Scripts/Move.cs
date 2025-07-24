using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    //����ֵ
    public float gravity = 0f;
    private Vector3 moveDirection = Vector3.zero;
    // ��ת�ٶ�
    public float speed1= 20f;

    // �Ƿ���������
    private bool isDragging = false;


    private void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
            // �������������
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }
            // ����ͷ�������
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            // ��������϶�������
            if (isDragging)
            {
                // ��ȡ����ƶ��ľ���
                float horizontal = Input.GetAxis("Mouse X") * speed1* Time.deltaTime;
                float vertical = Input.GetAxis("Mouse Y") * speed1 * Time.deltaTime;

                // ����Y����ת
                transform.Rotate(Vector3.up, -horizontal, Space.World);
                // ����X����ת
                transform.Rotate(Vector3.right, vertical, Space.World);
            }


        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }
}
