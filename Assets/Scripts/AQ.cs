using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class AQ : MonoBehaviour
    {
        public GameObject SuccessUI; // �ɹ����
        public GameObject FailUI; // ʧ�����
        public GameObject QuestionUI; // �������
        [Header("�Ƿ����")]
        public bool IsRandom;
        [Header("��ȡ��Ŀ������")]
        [Range(1, 100)]
        public int TopicCount;
        /// <summary>
        /// ���ÿ�е�����
        /// </summary>
        private string[] _perLineData;
        /// <summary>
        /// ������ά���������飬��ѡ�����3����4�������Դ�������������
        /// </summary>
        private string[][] _questionsArray;
        /// <summary>
        /// ��¼��Ŀ�Ƿ��Ѿ��ش�
        /// </summary>
        private readonly List<bool> IsAnswer = new List<bool>();
        /// <summary>
        /// ��¼ѡ��������
        /// </summary>
        private readonly List<int> SelectAnswerIndex = new List<int>();
        /// <summary>
        /// ��¼ѡ����Ƿ���ȷ
        /// </summary>
        private readonly List<bool> SelectIsRight = new List<bool>();
        /// <summary>
        /// ѡ���
        /// </summary>
        public List<Toggle> SelecToggles;
        /// <summary>
        /// ѡ��
        /// </summary>
        public List<Text> SelecAnswer;
        /// <summary>
        /// ��һ�ⰴť
        /// </summary>
        private Button _beforeTopicBtn;
        /// <summary>
        /// ��һ�ⰴť
        /// </summary>
        private Button _nextTopicBtn;
        /// <summary>
        /// ������ʾ��ť
        /// </summary>
        private Button _exitBtn;

        private Button _tipsBtn;
        /// <summary>
        /// ��ȷ����ʾ
        /// </summary>
        private Text _tipCorrectText;
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private Text _quesIndexText;
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        private Text _quesContent;
        /// <summary>
        /// ��ȷ����ʾ
        /// </summary>
        private Text _accuracyText;
        /// <summary>
        /// �ڼ���
        /// </summary>
        private int _quesIndex = 0;
        /// <summary>
        /// ��Ŀ������
        /// </summary>
        private int _quesCount;
        /// <summary>
        /// �ش���ȷ������
        /// </summary>
        private int _rightCount;
        /// <summary>
        /// �Ѿ���ش����Ŀ����
        /// </summary>
        private int _answeredCount;
        void Awake()
        {
            _beforeTopicBtn = GameObject.Find("Canvas/BgPanel/Buttons/BeforeTopic").GetComponent<Button>();
            _nextTopicBtn = GameObject.Find("Canvas/BgPanel/Buttons/NextTopic").GetComponent<Button>();
            _tipsBtn = GameObject.Find("Canvas/BgPanel/Buttons/TipsButton").GetComponent<Button>();
            _exitBtn = GameObject.Find("Canvas/BgPanel/Buttons/ExitButton").GetComponent<Button>();
            _tipCorrectText = GameObject.Find("Canvas/BgPanel/TipCorrectText").GetComponent<Text>();
            _quesIndexText = GameObject.Find("Canvas/BgPanel/QuesIndexText").GetComponent<Text>();
            _quesContent = GameObject.Find("Canvas/BgPanel/QuesContent").GetComponent<Text>();
            _accuracyText = GameObject.Find("Canvas/BgPanel/AccuracyText").GetComponent<Text>();
            for (int i = 0; i < 4; i++)
            {
                SelecToggles.Add(GameObject.Find("Canvas/BgPanel/SelecToggles").transform.GetChild(i).GetComponent<Toggle>());
                SelecAnswer.Add(SelecToggles[i].transform.GetChild(1).GetComponent<Text>());
            }
            _beforeTopicBtn.gameObject.SetActive(false);
            if (TopicCount == 1)
                _nextTopicBtn.gameObject.SetActive(false);
            if (_quesIndex != _quesCount)
                _exitBtn.gameObject.SetActive(false);
        }

        void Start()
        {
            //CsvReadQuestions();
            StreamingReadQuestions();
            _beforeTopicBtn.onClick.AddListener(BeforeTopic);
            _nextTopicBtn.onClick.AddListener(NextTopic);
            _tipsBtn.onClick.AddListener(CorrectTip);
            _exitBtn.onClick.AddListener(HandleExitButtonClick);
            for (int i = 0; i < 4; i++)
            {
                int count = i;
                SelecToggles[i].onValueChanged.AddListener(delegate (bool isOn)
                {
                    if (isOn)
                        JudgeSelect(count);
                });
            }
        }
        /// <summary>
        /// ѡ���ж�
        /// </summary>
        /// <param name="selectIndex">�ڼ���ѡ��</param>
        void JudgeSelect(int selectIndex)
        {

            bool isRight = false;
            int selectCount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i == selectIndex)
                {
                    SelecToggles[i].isOn = true;
                    int index = _questionsArray[_quesIndex].Length - 1;//�ö�λ����ĳ��ȣ���1������һλ�����
                    int correctIndex = int.Parse(_questionsArray[_quesIndex][index]) - 1;//ArrayX[topicIndex][idx]��ȡ�����һλ����ȷ�𰸱�ʶ���������0123�����Լ�1

                    if (i == correctIndex)
                    {
                        if (!IsAnswer[_quesIndex])
                            _tipCorrectText.text = "<color=green>��ϲ�㣬�ش���ȷ����������⡣</color>";
                        isRight = true;
                    }
                    else
                    {
                        if (!IsAnswer[_quesIndex])
                            _tipCorrectText.text = "<color=red>�Բ��𣬻ش����������ѡ���鿴��ʾ��</color>";
                        _tipsBtn.gameObject.SetActive(true);
                    }
                    selectCount = i;
                }
                else
                    SelecToggles[i].isOn = false;//��ѡ��
            }
            if (IsAnswer[_quesIndex])
            {
                Debug.Log("�Ѿ��ش�");
            }
            else
            {
                _answeredCount++;
                if (isRight)
                    _rightCount++;
                IsAnswer[_quesIndex] = true;
                SelectAnswerIndex.Add(selectCount + 1);
                SelectIsRight.Add(isRight);
                _accuracyText.text = "��ȷ�ʣ�" + ((float)_rightCount / _answeredCount * 100).ToString("f2") + "%";
            }

        }
        /// <summary>
        ///  csv��ȡResources�ڵ����
        /// </summary>
        void CsvReadQuestions()
        {
            //csv�����ƶ�ȡ�ļ�
            TextAsset questions = Resources.Load<TextAsset>("�������");

            SaveTopicToArray(questions);
        }
        void SaveTopicToArray(TextAsset questions)
        {
            //��ȡ�����ÿ�е�����
            _perLineData = questions.text.Split("\r"[0]);

            Read(IsRandom);

            _quesCount = _questionsArray.Length - 1;

            for (int i = 0; i <= _quesCount; i++)
                IsAnswer.Add(false);//������Ŀ������Ӽ�¼ֵ

            TopicSet();
        }
        /// <summary>
        /// �ⲿ���ض�ȡ���
        /// </summary>
        void StreamingReadQuestions()
        {
            //��������ȡ�ļ�
            string questions = File.ReadAllText(Application.streamingAssetsPath + "/�������.txt");
            SaveTopicToArray(questions);
        }

        void SaveTopicToArray(string questions)
        {
            //��ȡ�����ÿ�е�����
            _perLineData = questions.Split("\r"[0]);

            Read(IsRandom);

            _quesCount = _questionsArray.Length - 1;

            for (int i = 0; i <= _quesCount; i++)
                IsAnswer.Add(false);//������Ŀ������Ӽ�¼ֵ

            TopicSet();
        }
        /// <summary>
        /// ��ӡ��ж�����
        /// </summary>
        /// <param name="isRandom"></param>
        void Read(bool isRandom)
        {
            // ���ڲ��������ѡ�����ǿ���ֱ��ʹ�������ܳ�����Ϊ��ά���������
            _questionsArray = new string[_perLineData.Length][];
            for (int i = 0; i < _perLineData.Length; i++)
            {
                // ֱ��ʹ������е�ÿ��������������ά����
                _questionsArray[i] = _perLineData[i].Split("*"[0]);
            }
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        void TopicSet()
        {
            _tipsBtn.gameObject.SetActive(false);//��ʼ������ʾ��ť������ʱ��ʾ
            _tipCorrectText.text = "";
            for (int i = 0; i < 4; i++)
                SelecToggles[i].isOn = false;//��ʼʱ���е�ѡ��Ĭ��Ϊδѡ
            _quesIndexText.text = "��" + (_quesIndex + 1) + "�⣺";
            _quesContent.text = _questionsArray[_quesIndex][1];//��Ŀ����  ���0����Ŀ����š�
            int selectCount = _questionsArray[_quesIndex].Length - 3;//�ж���ѡ��  ��ȥ3���ֱ��ǣ���š���Ŀ����ȷ�𰸱�ʶ
            for (int i = 0; i < selectCount; i++)
            {
                SelecToggles[i].gameObject.SetActive(true);
                SelecAnswer[i].text = _questionsArray[_quesIndex][i + 2];//������Ŀ��ѡ��ӵ���λ��ʼ
            }
            if (selectCount < SelecToggles.Count)//�ж�ѡ���Ƿ���4�������û�������ض����
            {
                for (int i = selectCount; i < SelecToggles.Count; i++)
                    SelecToggles[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// ��ʾ��ȷ��
        /// </summary>
        void CorrectTip()
        {
            _tipCorrectText.text = "��ȷ���ǣ�<color=green>" + JudgeCorrectAnswer() + "</color>";
        }

        /// <summary>
        /// ��ȷ�𰸵�ѡ��
        /// </summary>
        /// <returns></returns>
        string JudgeCorrectAnswer()
        {
            int index = _questionsArray[_quesIndex].Length - 1;//�ö�λ����ĳ��ȣ���1������һλ�����
            int correctIndex = int.Parse(_questionsArray[_quesIndex][index]);//ArrayX[topicIndex][idx]��ȡ�����һλ����ȷ�𰸱�ʶ
            string correctNum = "";
            switch (correctIndex)
            {
                case 1:
                    correctNum = "A";
                    break;
                case 2:
                    correctNum = "B";
                    break;
                case 3:
                    correctNum = "C";
                    break;
                case 4:
                    correctNum = "D";
                    break;
            }

            return correctNum;
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        void BeforeTopic()
        {
            if (_quesIndex > 0)
            {
                _quesIndex--;
                TopicSet();
                if (IsAnswer[_quesIndex])//�Ѿ������
                {
                    if (SelectIsRight[_quesIndex])//���һش���ȷ
                    {
                        _tipCorrectText.text = "<color=green>�����Ѿ��ش�����һش���ȷ����������⡣</color>";
                        SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                    }
                    else
                    {
                        SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                        _tipCorrectText.text = "�����Ѿ��ش����<color=red>�ش����</color>����ȷ��Ϊ��<color=green>" + JudgeCorrectAnswer() + "</color>";
                    }

                }
                if (_quesIndex != _quesCount)
                    _nextTopicBtn.gameObject.SetActive(true);
                if (_quesIndex == 0)
                    _beforeTopicBtn.gameObject.SetActive(false);

            }
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        void NextTopic()
        {
            if (_quesIndex < _quesCount)
            {
                if (_quesIndex == 0)
                {
                    _quesIndex++;
                    TopicSet();
                    if (!IsAnswer[_quesIndex]) // ȷ���û��Ѿ��ش��˵�ǰ��Ŀ�������߼���������ͨ��Ӧ�����Ѿ��ش���ż���Ƿ���ȷ��
                    {
                        // ע�⣺������߼�������Ҫ��������ʵ��������е���
                        // ��� IsAnswer[_quesIndex] ��ʾ�û��Ƿ�**δ**�ش�������������ж���Ҫ��ת
                        if (SelectIsRight[_quesIndex]) // ���һش���ȷ
                        {
                            _tipCorrectText.text = "<color=green>��ϲ�㣬�ش���ȷ����������⡣</color>";
                            if (_quesIndex > 0 && SelectAnswerIndex[_quesIndex - 1] >= 0 && SelectAnswerIndex[_quesIndex - 1] < SelecToggles.Count)
                            {
                                SelecToggles[SelectAnswerIndex[_quesIndex - 1]].isOn = true;
                            }
                            // ע�⣺��� SelectAnswerIndex[_quesIndex - 1] ������Ч���������������Ǳ�Ҫ��
                            // ���⣬��� SelecToggles �ĳ����� SelectAnswerIndex ��Ԫ�ز���Ӧ��Ҳ��Ҫ����
                        }
                        else
                        {
                            // ע�⣺����Ӧ��ȷ�� SelectAnswerIndex[_quesIndex] ����Ч�ģ����Ҳ��ᵼ������Խ��
                            // ��� SelectAnswerIndex[_quesIndex] ��������Ч����������Ӧ����Ӷ���ļ��
                            if (SelectAnswerIndex[_quesIndex] > 0 && SelectAnswerIndex[_quesIndex] - 1 < SelecToggles.Count)
                            {
                                SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true; // ���д����������ͨ����Ӧ�ü� 1���������ض��߼�
                            }
                            // ����������߼�����ʾ�û�ѡ��Ĵ���𰸣����Ҹ�����ȷ�𰸵���ʾ
                            _tipCorrectText.text = "�����Ѿ��ش����<color=red>�ش����</color>����ȷ��Ϊ��<color=green>" + JudgeCorrectAnswer() + "</color>";

                            // ע�⣺��� SelectAnswerIndex[_quesIndex] Ӧ��ֱ�Ӷ�Ӧ SelecToggles ��������
                            // ��Ӧ�ü� 1��Ӧ���ǣ�SelecToggles[SelectAnswerIndex[_quesIndex]].isOn = false; // ����Ҫȡ������𰸵�ѡ��״̬
                            // ���ҿ�����Ҫ������ȷ�𰸵�ѡ��״̬Ϊ true
                        }
                    }
                }
                else
                {
                    if (!IsAnswer[_quesIndex]) // ȷ���û��Ѿ��ش��˵�ǰ��Ŀ
                    {
                        if (SelectIsRight[_quesIndex]) // ���һش���ȷ
                        {
                            _tipCorrectText.text = "<color=green>�����Ѿ��ش�����һش���ȷ����������⡣</color>";
                            SelecToggles[SelectAnswerIndex[_quesIndex - 1]].isOn = true;
                        }
                        else
                        {
                            SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                            _tipCorrectText.text = "�����Ѿ��ش����<color=red>�ش����</color>����ȷ��Ϊ��<color=green>" + JudgeCorrectAnswer() + "</color>";
                        }
                    }
                    _quesIndex++; // ������������
                    TopicSet(); // ������һ������
                }

                if (_quesIndex == _quesCount)
                    _nextTopicBtn.gameObject.SetActive(false);
                if (_quesIndex != 0)
                    _beforeTopicBtn.gameObject.SetActive(true);

                if (IsAnswer.Count == _quesCount + 1 && SelectIsRight.Count == _quesCount + 1 && SelectAnswerIndex.Count == _quesCount + 1) // ȷ��������Ŀ�����ش���
                {
                    float accuracy = (_rightCount / (float)_answeredCount) * 100; // ������ȷ��
                    if (accuracy >= 80) // ��ȷ�ʴﵽ80%����
                    {
                        QuestionUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
                        SuccessUI.SetActive(true); // ����Panel��Active����Ϊfalse��������
                    }
                    else // ��ȷ��δ�ﵽ80%
                    {
                        QuestionUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
                        FailUI.SetActive(true);
                    }
                }
            }
        }

        public void HandleExitButtonClick()
        {
           // if (_quesIndex == _quesCount) // ȷ���������һ��  
           // {
                float accuracy = (_rightCount / (float)_answeredCount) * 100; // ������ȷ��  

                if (accuracy >= 80) // ��ȷ�ʴﵽ80%����  
                {
                    QuestionUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
                    SuccessUI.SetActive(true); // ����Panel��Active����Ϊfalse��������
                }
                else // ��ȷ��δ�ﵽ80%  
                {


                    QuestionUI.SetActive(false); // ����Panel��Active����Ϊfalse��������
                    FailUI.SetActive(true);


                }
           // }
        }
    }
}
