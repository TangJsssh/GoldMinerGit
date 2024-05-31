using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    //��ǰ�����Ƿ��Ѿ��ر�
    protected bool isRemove = false;
    //�����Ƿ��Ѿ��ر�
    protected new string name;
    protected virtual void Awake()
    {

    }
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
    //�������棺���ݴ���������ƽ������������ΪTrue
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
        //�Ƴ����� ��ʾ����û��
        if (UIManager.Instance.panelDict.ContainsKey(name))
        {
            UIManager.Instance.panelDict.Remove(name);
        }
    }
}
