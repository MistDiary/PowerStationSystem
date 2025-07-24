using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDong : MonoBehaviour
{
    public MeshRenderer meshRenderer;//��ͷ3D����Quad Ԥ����
    public List<Transform> points = new List<Transform>();//·����
    private List<MeshRenderer> lines = new List<MeshRenderer>();//��ʾ��·��

    public float xscale = 6f;//���ű���
    public float yscale = 0.5f;


    public bool shouldDrawButtons = false; // ���ư�ť�Ƿ����

    void Start()
    {
        //��ͷ�������ֵ
        xscale = meshRenderer.transform.localScale.x;
        //��ͷ��������ֵ
        yscale = meshRenderer.transform.localScale.y;
       
    }

    //��·��
    public void DrawPath()
    {
        if (points == null || points.Count <= 1)
            return;
        for (int i = 0; i < points.Count - 1; i++)
        {
            DrawLine(points[i].position, points[i + 1].position, i);
        }
    }

    //��·�� ����Ϊ·��������
    public void DrawPath(Vector3[] points)
    {
        if (points == null || points.Length <= 1)
            return;
        for (int i = 0; i < points.Length - 1; i++)
        {
            DrawLine(points[i], points[i + 1], i);
        }
    }

    //����·��
    public void HidePath()
    {
        for (int i = 0; i < lines.Count; i++)
            lines[i].gameObject.SetActive(false);
    }

    //��·��
    private void DrawLine(Vector3 start, Vector3 end, int index)
    {
        Debug.Log(transform.gameObject.name);
        MeshRenderer mr;
        if (index >= lines.Count)
        {
            mr = Instantiate(meshRenderer);
            lines.Add(mr);
        }
        else
        {
            mr = lines[index];
        }

        var tran = mr.transform;
        var length = Vector3.Distance(start, end);
        tran.localScale = new Vector3(xscale, length, 1);
        tran.position = (start + end) / 2;
        //ָ��end
        tran.LookAt(end);
        //��תƫ��
        tran.Rotate(90, 0, 0);
        mr.material.mainTextureScale = new Vector2(1, length * yscale);
        mr.gameObject.SetActive(true);
    }

    void OnGUI()
    {
        // ֻ�е�shouldDrawButtonsΪtrueʱ�Ż��ư�ť
        if (shouldDrawButtons)
        {
            if (GUI.Button(new Rect(20, 40, 80, 20), "��ʾ·��"))
            {
                DrawPath();
            }
            if (GUI.Button(new Rect(20, 80, 80, 20), "����·��"))
            {
                HidePath();
            }
        }
    }
}
