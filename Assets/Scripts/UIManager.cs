using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager 
{
    //用来挂在界面的根节点
    private Transform _uiRoot;
    private Dictionary<string, string> pathDict;
    private Dictionary<string, GameObject> perfabDict;//预制件缓存字典
    public Dictionary<string,BasePanel> panelDict;//存储已打开界面字典 
    //单例模式
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
    //找到UI界面的根节点，如果当前根节点为空，就新建一根Canvas组件作为根节点
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

                    // 设置Canvas组件
                    Canvas canvas = canvasGameObject.GetComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;

                    // 设置CanvasScaler组件
                    CanvasScaler scaler = canvasGameObject.GetComponent<CanvasScaler>();
                    scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                    scaler.referenceResolution = new Vector2(1920, 1080);
                    scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                    scaler.matchWidthOrHeight = 0; // 完全匹配宽度

                    // GraphicRaycaster组件通常不需要额外配置
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
        //检测是否已打开
        if(panelDict.TryGetValue(name, out panel))
        {
            return panel;
        }
        return null;
    }
    //打开界面
    public BasePanel OpenPanel(string name)
    {
        BasePanel panel=null;
        //检查是否已打开
        if(panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("界面已打开" + name);
            return null;
        }
        //检查路径是否配置
        string path = "";
        if(!pathDict.TryGetValue(name, out path))
        {
            Debug.Log("界面名称错误，未配置路径" + name);
            return null;  
        }
        //使用缓存的预制件
        GameObject panelPrefab = null;
        if(!perfabDict.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Perfab/Panel/" + path;
            //如果没有放在缓存字典继续使用
            panelPrefab=Resources.Load<GameObject>(realPath) as GameObject;
            if (panelPrefab == null)
            {
                Debug.LogError("从路径加载预制件失败: " + realPath);
                return null;
            }
            perfabDict.Add(name, panelPrefab);
        }
        //打开界面
        GameObject panelObject =GameObject.Instantiate(panelPrefab,UIRoot,false);
        panel=panelObject.GetComponent<BasePanel>();
        panelDict.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }
    //关闭界面
    public bool ClosePanel(string name)
    {
        BasePanel panel=null;
        //检查界面是否在panelDict中 
        if(!panelDict.TryGetValue(name,out panel))
        {
            Debug.Log("界面未打开" + name);
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
