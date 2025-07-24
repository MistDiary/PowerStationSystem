using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float runSpeed = 6.0f;
    public float jumpHeight = 7.0f;
    public float turnSmoothTime = 0.2f;
    public Transform head;

    private float turnSmoothVelocity;
    private float currentSpeed;
    private bool isGrounded;
    private Vector3 playerVelocity;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private float sensitivity = 2f;
    private float minimumX = -50f;
    private float maximumX = 50f;

    private CharacterController controller;
    public Animator animator;
    private Rigidbody rb;




    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        // ���������
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        // ����Ƿ��ڵ�����
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // ��ȡˮƽ�ʹ�ֱ����
        float moveForward = Input.GetAxis("Vertical");
        float moveRight = Input.GetAxis("Horizontal");

        // �����ƶ�������ٶ�
        Vector3 move = transform.right * moveRight + transform.forward * moveForward;
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // ���¶���״̬
        //float speedNormalized = Mathf.InverseLerp(walkSpeed, runSpeed, currentSpeed);
        animator.SetFloat("Speed", move.magnitude);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("IsGrounded", isGrounded);

        // ��Ծ
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            animator.SetTrigger("Jump");
        }

        // Ӧ������
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);




        // ͷ����ת
        rotationX += Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
        rotationY += Input.GetAxis("Mouse X") * sensitivity;

        // ����ͷ�������������ת
        Quaternion headRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Quaternion bodyRotation = Quaternion.Euler(0f, rotationY, 0f);

        // ����ͷ�����������ת
        head.localRotation = headRotation;
        transform.localRotation = bodyRotation;





        


    }
}