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
        // 锁定鼠标光标
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    private void Update()
    {
        // 检测是否在地面上
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // 获取水平和垂直输入
        float moveForward = Input.GetAxis("Vertical");
        float moveRight = Input.GetAxis("Horizontal");

        // 计算移动方向和速度
        Vector3 move = transform.right * moveRight + transform.forward * moveForward;
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // 更新动画状态
        //float speedNormalized = Mathf.InverseLerp(walkSpeed, runSpeed, currentSpeed);
        animator.SetFloat("Speed", move.magnitude);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));
        animator.SetBool("IsGrounded", isGrounded);

        // 跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * Physics.gravity.y);
            animator.SetTrigger("Jump");
        }

        // 应用重力
        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);




        // 头部旋转
        rotationX += Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
        rotationY += Input.GetAxis("Mouse X") * sensitivity;

        // 计算头部和身体的新旋转
        Quaternion headRotation = Quaternion.Euler(rotationX, 0f, 0f);
        Quaternion bodyRotation = Quaternion.Euler(0f, rotationY, 0f);

        // 设置头部和身体的旋转
        head.localRotation = headRotation;
        transform.localRotation = bodyRotation;





        


    }
}