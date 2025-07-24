using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float rotateSpeed = 5.0f; // 鼠标转向速度
    public float maxPitchAngle = 40.0f; // 最大俯仰角度
    private float yaw;
    private float pitch;
    public float moveSpeed = 5.0f; // 移动速度
    public float jumpSpeed = 8.0f; // 跳跃速度
    public float gravity = -9.81f; // 重力加速度
    public float fallMultiplier = 2.5f; // 下落时重力加速度的倍数
    public float lowJumpMultiplier = 2.0f; // 短跳跃时重力加速度的倍数

    private CharacterController characterController;
    private Vector3 moveDirection;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
        // 锁定鼠标光标到游戏窗口中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 视角转向
        yaw += Input.GetAxis("Mouse X") * rotateSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotateSpeed;
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle); // 限制俯仰角度
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        // 移动逻辑
        float moveForward = Input.GetAxis("Vertical") * moveSpeed;
        float moveRight = Input.GetAxis("Horizontal") * moveSpeed;
        moveDirection = transform.forward * moveForward + transform.right * moveRight;

        // 应用重力
        if (characterController.isGrounded)
        {
            moveDirection.y = 0f; // 当角色在地面上时，y轴速度设为0

            // 跳跃逻辑
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed; // 角色跳跃
            }
        }
        else
        {
            // 当角色不在地面上时，应用重力
            moveDirection.y += gravity * Time.deltaTime;

            // 如果角色正在下落，增加下落速度
            if (moveDirection.y < 0)
            {
                moveDirection.y += gravity * (fallMultiplier * Time.deltaTime);
            }
            // 如果角色正在上升并且松开了跳跃键，减少上升速度
            else if (Input.GetButtonUp("Jump"))
            {
                moveDirection.y += gravity * (lowJumpMultiplier * Time.deltaTime);
            }
        }

        // 移动角色
        characterController.Move(moveDirection * Time.deltaTime);
        // 解锁光标
        void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
