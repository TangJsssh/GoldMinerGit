using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    //�������ڽ���ĸ��ڵ�
    private Transform _uiRoot;
    private Dictionary<string, string> pathDict;
    private Dictionary<string, GameObject> perfabDict;//Ԥ�Ƽ������ֵ�
    public Dictionary<string,BasePanel> panelDict;//�洢�Ѵ򿪽����ֵ� 
    //����ģʽ
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    //�ҵ�UI����ĸ��ڵ㣬�����ǰ���ڵ�Ϊ�գ����½�һ��Canvas�����Ϊ���ڵ�
    public Transform UIRoot
    {
        get
        {
            if(_uiRoot == null)
            {
                //if (GameObject.Find("Canvas"))
                //{
                //    _uiRoot = GameObject.Find("Canvas").transform;
                //}
                //else
                //{
                //    _uiRoot = new GameObject("Canvas").transform;

                //}
                GameObject canvasGameObject;
                if (GameObject.Find("Canvas"))
                {
                    canvasGameObject = GameObject.Find("Canvas");
                }
                else
                {
                    canvasGameObject = new GameObject("Canvas");
                    canvasGameObject.AddComponent<Canvas>();
                    canvasGameObject.AddComponent<CanvasScaler>();
                    canvasGameObject.AddComponent<GraphicRaycaster>();

                    // ����Canvas���
                    Canvas canvas = canvasGameObject.GetComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                    // ����CanvasScaler���
                    CanvasScaler scaler = canvasGameObject.GetComponent<CanvasScaler>();
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1920, 1080);
                    scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = 0; // ��ȫƥ����

                    // GraphicRaycaster���ͨ������Ҫ��������
                }

                _uiRoot = canvasGameObject.transform;
            };
            return _uiRoot;
        }
    }
    private UIManager()
    {
        InitDicts();
    }
    private void InitDicts()
    {
        perfabDict=new Dictionary<string, GameObject> ();
        panelDict = new Dictionary<string, BasePanel> ();
        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PackagePanel,"package/PackagePanel" }, 
        };
    }
    public BasePanel GetPanel(string name)
    {
        BasePanel panel=null;
        //����Ƿ��Ѵ�
        if(panelDict.TryGetValue(name, out panel))
        {
            return panel;
        }
        return null;
    }
    //�򿪽���
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel=null;
        //����Ƿ��Ѵ�
        if(panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("�����Ѵ�" + name);
            return null;
        }
        //���·���Ƿ�����
        string path = "";
        if(!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("�������ƴ���δ����·��" + name);
            return null;  
        }
        //ʹ�û����Ԥ�Ƽ�
        GameObject panelPrefab = null;
        if(!perfabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Perfab/Panel/" + path;
            //���û�з��ڻ����ֵ����ʹ��
            panelPrefab=Resources.Load<GameObject>(realPath) as GameObject;
            if (panelPrefab == null)
            {
                Debug.LogError("��·������Ԥ�Ƽ�ʧ��: " + realPath);
                return null;
            }
            perfabDict.Add(name, panelPrefab);
        }
        //�򿪽���
        GameObject panelObject =GameObject.Instantiate(panelPrefab,UIRoot,false);
        panel=panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }
    //�رս���
    public bool ClosePanel(string name)
    {
        BasePanel panel=null;
        //�������Ƿ���panelDict�� 
        if(!panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("����δ��" + name);
            return false;
        }
        panel.ClosePanel();
        return true;
    }
 

}
public class UIConst
{
    public const string PackagePanel = "PackagePanel";
}
