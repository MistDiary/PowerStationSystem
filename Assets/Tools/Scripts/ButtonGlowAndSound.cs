using UnityEngine;

public class ButtonGlowAndSound : MonoBehaviour
{
    public Material buttonMaterial; // ��ť�Ĳ���
    public Color glowColor; // ������ɫ
  public AudioClip soundEffect1; // ��Ч
    public AudioSource audioSource1; // ��ƵԴ
    private Animator characterAnimator; // ��ɫ��Animator���
   

    void Start()
    {
        // ȷ���Ѿ������AudioSource���
        audioSource1 = GetComponent<AudioSource>();
        if (audioSource1 == null)
        {
            audioSource1 = gameObject.AddComponent<AudioSource>();
        }
        audioSource1.clip = soundEffect1;
        audioSource1.loop = true; // ������Чѭ������

        // ��ȡ��ɫ��Animator���
        characterAnimator = GetComponent<Animator>();
        if (characterAnimator == null)
        {
            Debug.LogError("Animator component not found on the object.");
        }

        // ���ð�ť���ʣ�ʹ�䲻����
         ResetButtonMaterial();
    }

    void Update()
    {
        // ����Ƿ�����E��
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("����E");
            // ���������ֱ۵Ķ���
            if (characterAnimator != null)
             {
                 // ���������Trigger���ͣ�ʹ�����´���
                  characterAnimator.SetTrigger("IsRaiseHand");
                
                
             }
           
               // ���Ĳ��ʵ�Emission��ɫʹ�䷢��
               buttonMaterial.EnableKeyword("_EMISSION");
               buttonMaterial.SetColor("_EmissionColor", glowColor);

               // ������Ч
               if (!audioSource1.isPlaying)
               {
                   audioSource1.Play();
               }
        }
        





    }

        // ���ð�ť���ʵķ���
         private void ResetButtonMaterial()
         {
             // ����Emission�ؼ���
             buttonMaterial.DisableKeyword("_EMISSION");
             // ����Emission��ɫΪĬ��ֵ��ͨ���Ǻ�ɫ����ʾ�����⣩
             buttonMaterial.SetColor("_EmissionColor", Color.black);
         }

    
}
