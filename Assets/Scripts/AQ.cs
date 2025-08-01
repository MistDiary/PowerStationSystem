using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class AQ : MonoBehaviour
    {
        public GameObject SuccessUI; // 成功面板
        public GameObject FailUI; // 失败面板
        public GameObject QuestionUI; // 答题面板
        [Header("是否随机")]
        public bool IsRandom;
        [Header("抽取题目的数量")]
        [Range(1, 100)]
        public int TopicCount;
        /// <summary>
        /// 题库每行的数据
        /// </summary>
        private string[] _perLineData;
        /// <summary>
        /// 创建二维不规则数组，因选项可能3个或4个，所以创建不规则数组
        /// </summary>
        private string[][] _questionsArray;
        /// <summary>
        /// 记录题目是否已经回答
        /// </summary>
        private readonly List<bool> IsAnswer = new List<bool>();
        /// <summary>
        /// 记录选择过的序号
        /// </summary>
        private readonly List<int> SelectAnswerIndex = new List<int>();
        /// <summary>
        /// 记录选择的是否正确
        /// </summary>
        private readonly List<bool> SelectIsRight = new List<bool>();
        /// <summary>
        /// 选择键
        /// </summary>
        public List<Toggle> SelecToggles;
        /// <summary>
        /// 选项
        /// </summary>
        public List<Text> SelecAnswer;
        /// <summary>
        /// 上一题按钮
        /// </summary>
        private Button _beforeTopicBtn;
        /// <summary>
        /// 下一题按钮
        /// </summary>
        private Button _nextTopicBtn;
        /// <summary>
        /// 错误提示按钮
        /// </summary>
        private Button _exitBtn;

        private Button _tipsBtn;
        /// <summary>
        /// 正确答案提示
        /// </summary>
        private Text _tipCorrectText;
        /// <summary>
        /// 题目序号
        /// </summary>
        private Text _quesIndexText;
        /// <summary>
        /// 题目内容
        /// </summary>
        private Text _quesContent;
        /// <summary>
        /// 正确率显示
        /// </summary>
        private Text _accuracyText;
        /// <summary>
        /// 第几题
        /// </summary>
        private int _quesIndex = 0;
        /// <summary>
        /// 题目总数量
        /// </summary>
        private int _quesCount;
        /// <summary>
        /// 回答正确的数量
        /// </summary>
        private int _rightCount;
        /// <summary>
        /// 已经大回答的题目数量
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
        /// 选择判断
        /// </summary>
        /// <param name="selectIndex">第几个选项</param>
        void JudgeSelect(int selectIndex)
        {

            bool isRight = false;
            int selectCount = 0;
            for (int i = 0; i < 4; i++)
            {
                if (i == selectIndex)
                {
                    SelecToggles[i].isOn = true;
                    int index = _questionsArray[_quesIndex].Length - 1;//该二位数组的长度，减1获得最后一位的序号
                    int correctIndex = int.Parse(_questionsArray[_quesIndex][index]) - 1;//ArrayX[topicIndex][idx]获取到最后一位的正确答案标识，答案序号是0123，所以减1

                    if (i == correctIndex)
                    {
                        if (!IsAnswer[_quesIndex])
                            _tipCorrectText.text = "<color=green>恭喜你，回答正确！请继续答题。</color>";
                        isRight = true;
                    }
                    else
                    {
                        if (!IsAnswer[_quesIndex])
                            _tipCorrectText.text = "<color=red>对不起，回答错误！请重新选择或查看提示。</color>";
                        _tipsBtn.gameObject.SetActive(true);
                    }
                    selectCount = i;
                }
                else
                    SelecToggles[i].isOn = false;//单选题
            }
            if (IsAnswer[_quesIndex])
            {
                Debug.Log("已经回答");
            }
            else
            {
                _answeredCount++;
                if (isRight)
                    _rightCount++;
                IsAnswer[_quesIndex] = true;
                SelectAnswerIndex.Add(selectCount + 1);
                SelectIsRight.Add(isRight);
                _accuracyText.text = "正确率：" + ((float)_rightCount / _answeredCount * 100).ToString("f2") + "%";
            }

        }
        /// <summary>
        ///  csv读取Resources内的题库
        /// </summary>
        void CsvReadQuestions()
        {
            //csv二进制读取文件
            TextAsset questions = Resources.Load<TextAsset>("面试题库");

            SaveTopicToArray(questions);
        }
        void SaveTopicToArray(TextAsset questions)
        {
            //读取题库中每行的数据
            _perLineData = questions.text.Split("\r"[0]);

            Read(IsRandom);

            _quesCount = _questionsArray.Length - 1;

            for (int i = 0; i <= _quesCount; i++)
                IsAnswer.Add(false);//根据题目数量添加记录值

            TopicSet();
        }
        /// <summary>
        /// 外部加载读取题库
        /// </summary>
        void StreamingReadQuestions()
        {
            //数据流读取文件
            string questions = File.ReadAllText(Application.streamingAssetsPath + "/面试题库.txt");
            SaveTopicToArray(questions);
        }

        void SaveTopicToArray(string questions)
        {
            //读取题库中每行的数据
            _perLineData = questions.Split("\r"[0]);

            Read(IsRandom);

            _quesCount = _questionsArray.Length - 1;

            for (int i = 0; i <= _quesCount; i++)
                IsAnswer.Add(false);//根据题目数量添加记录值

            TopicSet();
        }
        /// <summary>
        /// 添加、判断数据
        /// </summary>
        /// <param name="isRandom"></param>
        void Read(bool isRandom)
        {
            // 由于不进行随机选择，我们可以直接使用题库的总长度作为二维数组的行数
            _questionsArray = new string[_perLineData.Length][];
            for (int i = 0; i < _perLineData.Length; i++)
            {
                // 直接使用题库中的每行数据来创建二维数组
                _questionsArray[i] = _perLineData[i].Split("*"[0]);
            }
        }

        /// <summary>
        /// 题目设置
        /// </summary>
        void TopicSet()
        {
            _tipsBtn.gameObject.SetActive(false);//初始隐藏提示按钮，错误时显示
            _tipCorrectText.text = "";
            for (int i = 0; i < 4; i++)
                SelecToggles[i].isOn = false;//开始时所有的选择默认为未选
            _quesIndexText.text = "第" + (_quesIndex + 1) + "题：";
            _quesContent.text = _questionsArray[_quesIndex][1];//题目内容  序号0是题目的序号。
            int selectCount = _questionsArray[_quesIndex].Length - 3;//有多少选项  减去3个分别是：序号、题目、正确答案标识
            for (int i = 0; i < selectCount; i++)
            {
                SelecToggles[i].gameObject.SetActive(true);
                SelecAnswer[i].text = _questionsArray[_quesIndex][i + 2];//设置题目的选项。从第三位开始
            }
            if (selectCount < SelecToggles.Count)//判断选项是否有4个，如果没有则隐藏多余的
            {
                for (int i = selectCount; i < SelecToggles.Count; i++)
                    SelecToggles[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 提示正确答案
        /// </summary>
        void CorrectTip()
        {
            _tipCorrectText.text = "正确答案是：<color=green>" + JudgeCorrectAnswer() + "</color>";
        }

        /// <summary>
        /// 正确答案的选项
        /// </summary>
        /// <returns></returns>
        string JudgeCorrectAnswer()
        {
            int index = _questionsArray[_quesIndex].Length - 1;//该二位数组的长度，减1获得最后一位的序号
            int correctIndex = int.Parse(_questionsArray[_quesIndex][index]);//ArrayX[topicIndex][idx]获取到最后一位的正确答案标识
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
        /// 上一题
        /// </summary>
        void BeforeTopic()
        {
            if (_quesIndex > 0)
            {
                _quesIndex--;
                TopicSet();
                if (IsAnswer[_quesIndex])//已经答过题
                {
                    if (SelectIsRight[_quesIndex])//并且回答正确
                    {
                        _tipCorrectText.text = "<color=green>本题已经回答过，且回答正确，请继续答题。</color>";
                        SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                    }
                    else
                    {
                        SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                        _tipCorrectText.text = "本题已经回答过，<color=red>回答错误</color>，正确答案为：<color=green>" + JudgeCorrectAnswer() + "</color>";
                    }

                }
                if (_quesIndex != _quesCount)
                    _nextTopicBtn.gameObject.SetActive(true);
                if (_quesIndex == 0)
                    _beforeTopicBtn.gameObject.SetActive(false);

            }
        }
        /// <summary>
        /// 下一题
        /// </summary>
        void NextTopic()
        {
            if (_quesIndex < _quesCount)
            {
                if (_quesIndex == 0)
                {
                    _quesIndex++;
                    TopicSet();
                    if (!IsAnswer[_quesIndex]) // 确保用户已经回答了当前题目（这里逻辑可能有误，通常应该是已经回答过才检查是否正确）
                    {
                        // 注意：这里的逻辑可能需要根据您的实际需求进行调整
                        // 如果 IsAnswer[_quesIndex] 表示用户是否**未**回答，则下面的条件判断需要反转
                        if (SelectIsRight[_quesIndex]) // 并且回答正确
                        {
                            _tipCorrectText.text = "<color=green>恭喜你，回答正确！请继续答题。</color>";
                            if (_quesIndex > 0 && SelectAnswerIndex[_quesIndex - 1] >= 0 && SelectAnswerIndex[_quesIndex - 1] < SelecToggles.Count)
                            {
                                SelecToggles[SelectAnswerIndex[_quesIndex - 1]].isOn = true;
                            }
                            // 注意：如果 SelectAnswerIndex[_quesIndex - 1] 可能无效，上面的条件检查是必要的
                            // 另外，如果 SelecToggles 的长度与 SelectAnswerIndex 的元素不对应，也需要处理
                        }
                        else
                        {
                            // 注意：这里应该确保 SelectAnswerIndex[_quesIndex] 是有效的，并且不会导致索引越界
                            // 如果 SelectAnswerIndex[_quesIndex] 可能是无效的索引，您应该添加额外的检查
                            if (SelectAnswerIndex[_quesIndex] > 0 && SelectAnswerIndex[_quesIndex] - 1 < SelecToggles.Count)
                            {
                                SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true; // 这行代码可能有误，通常不应该减 1，除非有特定逻辑
                            }
                            // 假设这里的逻辑是显示用户选择的错误答案，并且给出正确答案的提示
                            _tipCorrectText.text = "本题已经回答过，<color=red>回答错误</color>，正确答案为：<color=green>" + JudgeCorrectAnswer() + "</color>";

                            // 注意：如果 SelectAnswerIndex[_quesIndex] 应该直接对应 SelecToggles 的索引，
                            // 则不应该减 1，应该是：SelecToggles[SelectAnswerIndex[_quesIndex]].isOn = false; // 假设要取消错误答案的选中状态
                            // 并且可能需要设置正确答案的选中状态为 true
                        }
                    }
                }
                else
                {
                    if (!IsAnswer[_quesIndex]) // 确保用户已经回答了当前题目
                    {
                        if (SelectIsRight[_quesIndex]) // 并且回答正确
                        {
                            _tipCorrectText.text = "<color=green>本题已经回答过，且回答正确，请继续答题。</color>";
                            SelecToggles[SelectAnswerIndex[_quesIndex - 1]].isOn = true;
                        }
                        else
                        {
                            SelecToggles[SelectAnswerIndex[_quesIndex] - 1].isOn = true;
                            _tipCorrectText.text = "本题已经回答过，<color=red>回答错误</color>，正确答案为：<color=green>" + JudgeCorrectAnswer() + "</color>";
                        }
                    }
                    _quesIndex++; // 增加问题索引
                    TopicSet(); // 设置下一个问题
                }

                if (_quesIndex == _quesCount)
                    _nextTopicBtn.gameObject.SetActive(false);
                if (_quesIndex != 0)
                    _beforeTopicBtn.gameObject.SetActive(true);

                if (IsAnswer.Count == _quesCount + 1 && SelectIsRight.Count == _quesCount + 1 && SelectAnswerIndex.Count == _quesCount + 1) // 确保所有题目都被回答了
                {
                    float accuracy = (_rightCount / (float)_answeredCount) * 100; // 计算正确率
                    if (accuracy >= 80) // 正确率达到80%以上
                    {
                        QuestionUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它
                        SuccessUI.SetActive(true); // 设置Panel的Active属性为false来隐藏它
                    }
                    else // 正确率未达到80%
                    {
                        QuestionUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它
                        FailUI.SetActive(true);
                    }
                }
            }
        }

        public void HandleExitButtonClick()
        {
           // if (_quesIndex == _quesCount) // 确保是在最后一题  
           // {
                float accuracy = (_rightCount / (float)_answeredCount) * 100; // 计算正确率  

                if (accuracy >= 80) // 正确率达到80%以上  
                {
                    QuestionUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它
                    SuccessUI.SetActive(true); // 设置Panel的Active属性为false来隐藏它
                }
                else // 正确率未达到80%  
                {


                    QuestionUI.SetActive(false); // 设置Panel的Active属性为false来隐藏它
                    FailUI.SetActive(true);


                }
           // }
        }
    }
}
