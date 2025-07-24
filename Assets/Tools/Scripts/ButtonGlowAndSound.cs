using UnityEngine;

public class ButtonGlowAndSound : MonoBehaviour
{
    public Material buttonMaterial; // 按钮的材质
    public Color glowColor; // 发光颜色
  public AudioClip soundEffect1; // 音效
    public AudioSource audioSource1; // 音频源
    private Animator characterAnimator; // 角色的Animator组件
   

    void Start()
    {
        // 确保已经添加了AudioSource组件
        audioSource1 = GetComponent<AudioSource>();
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }
        audioSource1.clip = soundEffect1;
        audioSource1.loop = true; // 设置音效循环播放

        // 获取角色的Animator组件
        characterAnimator = GetComponent<Animator>();
        if (characterAnimator == null)
        {
            Debug.LogError("Animator component not found on the object.");
        }

        // 重置按钮材质，使其不发光
         ResetButtonMaterial();
    }

    void Update()
    {
        // 检测是否按下了E键
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("按下E");
            // 触发举起手臂的动画
            if (characterAnimator != null)
             {
                 // 如果参数是Trigger类型，使用以下代码
                  characterAnimator.SetTrigger("IsRaiseHand");
                
                
             }
           
               // 更改材质的Emission颜色使其发光
               buttonMaterial.EnableKeyword("_EMISSION");
               buttonMaterial.SetColor("_EmissionColor", glowColor);

               // 播放音效
               if (!audioSource1.isPlaying)
               {
                   audioSource1.Play();
               }
        }
        





    }

        // 重置按钮材质的方法
         private void ResetButtonMaterial()
         {
             // 禁用Emission关键字
             buttonMaterial.DisableKeyword("_EMISSION");
             // 重置Emission颜色为默认值（通常是黑色，表示不发光）
             buttonMaterial.SetColor("_EmissionColor", Color.black);
         }

    
}
