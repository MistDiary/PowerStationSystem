using UnityEngine;

public class WorkerController : MonoBehaviour
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
    public GameObject ToolPanel1;//工具选择的面板
    public GameObject ToolPanel2;//工具选择的面板
    public GameObject ToolPanel3;//急救电话的面板
    public GameObject VideoUI;//急救视频观看
    public GameObject toolbar;
    public GameObject toolglove;

    public Animator PowerAnimator; // 开关的Animator组件
    public Animator WireAnimator; // 电线的Animator组件


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
        if (Input.GetButtonDown("Jump"))
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



        if (Input.GetKeyDown(KeyCode.F))//检测是否按下F键打开工具箱1
        {
            ToolPanel3.SetActive(false);
            ToolPanel2.SetActive(false); // 设置Pane2的Active属性为false来关闭它
            ToolPanel1.SetActive(true); // 设置Panel的Active属性为true来显示它
        }


        if (Input.GetKeyDown(KeyCode.X))//检测是否按下X键打开工具箱2
        {
            ToolPanel3.SetActive(false);
            ToolPanel1.SetActive(false); // 设置Panel1的Active属性为false来关闭它
            ToolPanel2.SetActive(true); // 设置Panel的Active属性为true来显示它
        }

        if (Input.GetKeyDown(KeyCode.Y))//检测是否按下Y键选择急救电话面板
        {
            ToolPanel2.SetActive(false);
            ToolPanel1.SetActive(false); // 设置Panel1的Active属性为false来关闭它
            ToolPanel3.SetActive(true); // 设置Panel的Active属性为true来显示它
        }


        if (Input.GetKeyDown(KeyCode.C))//切断电源动画的播放函数
        {
            if (PowerAnimator != null)
            {

                // 如果参数是Trigger类型，使用以下代码
                PowerAnimator.SetTrigger("off");
                toolglove.SetActive(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.Z))//切断电源时人物举手的动画播放
        {
            if (animator != null)
            {
                // 如果参数是Trigger类型，使用以下代码
                animator.SetTrigger("IsRaiseHand");


                if (WireAnimator != null)
                {
                    WireAnimator.SetTrigger("move");
                }

                toolbar.SetActive(false);

            }
        }



        if (Input.GetKeyDown(KeyCode.P))//检测是否按下P键选择观看急救视频
        {
            VideoUI.SetActive(true);
            
        }
       



    }


    private void OnDisable()
    {
        // 解锁鼠标光标
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

  
}