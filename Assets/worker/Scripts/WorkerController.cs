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
    public GameObject ToolPanel1;//����ѡ������
    public GameObject ToolPanel2;//����ѡ������
    public GameObject ToolPanel3;//���ȵ绰�����
    public GameObject VideoUI;//������Ƶ�ۿ�
    public GameObject toolbar;
    public GameObject toolglove;

    public Animator PowerAnimator; // ���ص�Animator���
    public Animator WireAnimator; // ���ߵ�Animator���


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
        if (Input.GetButtonDown("Jump"))
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



        if (Input.GetKeyDown(KeyCode.F))//����Ƿ���F���򿪹�����1
        {
            ToolPanel3.SetActive(false);
            ToolPanel2.SetActive(false); // ����Pane2��Active����Ϊfalse���ر���
            ToolPanel1.SetActive(true); // ����Panel��Active����Ϊtrue����ʾ��
        }


        if (Input.GetKeyDown(KeyCode.X))//����Ƿ���X���򿪹�����2
        {
            ToolPanel3.SetActive(false);
            ToolPanel1.SetActive(false); // ����Panel1��Active����Ϊfalse���ر���
            ToolPanel2.SetActive(true); // ����Panel��Active����Ϊtrue����ʾ��
        }

        if (Input.GetKeyDown(KeyCode.Y))//����Ƿ���Y��ѡ�񼱾ȵ绰���
        {
            ToolPanel2.SetActive(false);
            ToolPanel1.SetActive(false); // ����Panel1��Active����Ϊfalse���ر���
            ToolPanel3.SetActive(true); // ����Panel��Active����Ϊtrue����ʾ��
        }


        if (Input.GetKeyDown(KeyCode.C))//�жϵ�Դ�����Ĳ��ź���
        {
            if (PowerAnimator != null)
            {

                // ���������Trigger���ͣ�ʹ�����´���
                PowerAnimator.SetTrigger("off");
                toolglove.SetActive(false);

            }
        }

        if (Input.GetKeyDown(KeyCode.Z))//�жϵ�Դʱ������ֵĶ�������
        {
            if (animator != null)
            {
                // ���������Trigger���ͣ�ʹ�����´���
                animator.SetTrigger("IsRaiseHand");


                if (WireAnimator != null)
                {
                    WireAnimator.SetTrigger("move");
                }

                toolbar.SetActive(false);

            }
        }



        if (Input.GetKeyDown(KeyCode.P))//����Ƿ���P��ѡ��ۿ�������Ƶ
        {
            VideoUI.SetActive(true);
            
        }
       



    }


    private void OnDisable()
    {
        // ���������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

  
}