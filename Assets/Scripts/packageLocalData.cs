using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����ű��������ǽ������Ķ�̬������json�ĸ�ʽ�洢�ڱ���
//������ʹ���дӱ��ض�ȡ���ڴ���
public class packageLocalData
{
    //ʹ�õ���ģʽ
    private static packageLocalData _instance;
    public static packageLocalData Instance{
         get{
                if(_instance ==null){
                    _instance=new packageLocalData();
                 }
                return _instance;
         }
    }
    //���浱ǰ������Ʒ��̬��Ϣ
    public List<packageLocalItem> items;
    //�洢
    public void savePackage()
    {
        //�ѱ����Ϣ���л�Ϊ�ַ���
        string inventoryJson=JsonUtility.ToJson(this);
        //���ı����ݴ洢�������ļ���
        PlayerPrefs.SetString("packageLocalData", inventoryJson);
        PlayerPrefs.Save();

    }
    //��ȡ
    public List<packageLocalItem> loadPackage()
    {
        //���ж������Ƿ񱻶�ȡ�������ȡ��ֱ�ӷ���items
        if (items != null)
        {
            return items;
        }
        //���򵽱����ļ��ж�ȡ
        if (PlayerPrefs.HasKey("packageLocalData"))
        {
            //ʹ��ȡ���ڴ��б���ַ���
            string inventoryJson = PlayerPrefs.GetString("packageLocalData");
            //�����л�
            packageLocalData packagelocalData=JsonUtility.FromJson<packageLocalData>(inventoryJson);
            items = packagelocalData.items;
            return items;

        }
        else
        {
            items = new List<packageLocalItem>();
            return items;
        }
    }
}
[System.Serializable]
public class packageLocalItem
{
    public string uid;//Ψһ��ʶ��
    public int id;
    public int num;
    public int level;
    public bool isNew;

    public override string ToString()
    {
        return string.Format("{0},{1}", id, num);
    }
    //ʹ�ù����������г�ʼ��
    //public static packageLocalItem CreateNewItem(int id)
    //{
    //    return new packageLocalItem
    //    {
    //        uid = Guid.NewGuid().ToString(),
    //        id = id,
    //        num = id,
    //        level = id,
    //        isNew = id % 2 == 1
    //    };
    //}
    public packageLocalItem(string uid, int id, int num, int level, bool isNew)
    {
        this.uid = uid;
        this.id = id;
        this.num = num;
        this.level = level;
        this.isNew = isNew;
    }
}
