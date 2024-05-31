using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    //当前界面是否已经关闭
    protected bool isRemove = false;
    //界面是否已经关闭
    protected new string name;
    protected virtual void Awake()
    {

    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    //开启界面：根据传入界面名称将这个界面设置为True
    public virtual void OpenPanel(string name)
    {
        this.name = name;
        SetActive(true);
    }
    public virtual void ClosePanel()
    {
        isRemove = true;
        SetActive(false);
        Destroy(gameObject);
        //移除缓存 表示界面没打开
        if (UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
    }
}
