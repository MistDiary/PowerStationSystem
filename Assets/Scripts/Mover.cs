using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float rotateSpeed = 5.0f; // ���ת���ٶ�
    public float maxPitchAngle = 40.0f; // ������Ƕ�
    private float yaw;
    private float pitch;
    public float moveSpeed = 5.0f; // �ƶ��ٶ�
    public float jumpSpeed = 8.0f; // ��Ծ�ٶ�
    public float gravity = -9.81f; // �������ٶ�
    public float fallMultiplier = 2.5f; // ����ʱ�������ٶȵı���
    public float lowJumpMultiplier = 2.0f; // ����Ծʱ�������ٶȵı���

    private CharacterController characterController;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        // ��������굽��Ϸ��������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �ӽ�ת��
        yaw += Input.GetAxis("Mouse X") * rotateSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle); // ���Ƹ����Ƕ�
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // �ƶ��߼�
        float moveForward = Input.GetAxis("Vertical") * moveSpeed;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed;
        moveDirection = transform.forward * moveForward + transform.right * moveRight;

        // Ӧ������
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f; // ����ɫ�ڵ�����ʱ��y���ٶ���Ϊ0

            // ��Ծ�߼�
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed; // ��ɫ��Ծ
            }
        }
        else
        {
            // ����ɫ���ڵ�����ʱ��Ӧ������
            moveDirection.y += gravity * Time.deltaTime;

            // �����ɫ�������䣬���������ٶ�
            if (moveDirection.y < 0)
            {
                moveDirection.y += gravity * (fallMultiplier * Time.deltaTime);
            }
            // �����ɫ�������������ɿ�����Ծ�������������ٶ�
            else if (Input.GetButtonUp("Jump"))
            {
                moveDirection.y += gravity * (lowJumpMultiplier * Time.deltaTime);
            }
        }

        // �ƶ���ɫ
        characterController.Move(moveDirection * Time.deltaTime);
        // �������
        void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
