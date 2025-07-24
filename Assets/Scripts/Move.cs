using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpSpeed = 8.0f;
    //重力值
    public float gravity = 0f;
    private Vector3 moveDirection = Vector3.zero;
    // 旋转速度
    public float speed1= 20f;

    // 是否按下鼠标左键
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
            // 如果按下鼠标左键
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
            }
            // 如果释放鼠标左键
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            // 如果正在拖动鼠标左键
            if (isDragging)
            {
                // 获取鼠标移动的距离
                float horizontal = Input.GetAxis("Mouse X") * speed1* Time.deltaTime;
                float vertical = Input.GetAxis("Mouse Y") * speed1 * Time.deltaTime;

                // 绕着Y轴旋转
                transform.Rotate(Vector3.up, -horizontal, Space.World);
                // 绕着X轴旋转
                transform.Rotate(Vector3.right, vertical, Space.World);
            }


        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


    }
}
