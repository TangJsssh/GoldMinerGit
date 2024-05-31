using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这个脚本的作用是将背包的动态数据以json的格式存储在本地
//并且在使用中从本地读取到内存中
public class packageLocalData
{
    //使用单例模式
    private static packageLocalData _instance;
    public static packageLocalData Instance{
         get{
                if(_instance ==null){
                    _instance=new packageLocalData();
                 }
                return _instance;
         }
    }
    //缓存当前所有物品动态信息
    public List<packageLocalItem> items;
    //存储
    public void savePackage()
    {
        //把表格信息序列化为字符串
        string inventoryJson=JsonUtility.ToJson(this);
        //把文本数据存储到本地文件中
        PlayerPrefs.SetString("packageLocalData", inventoryJson);
        PlayerPrefs.Save();

    }
    //读取
    public List<packageLocalItem> loadPackage()
    {
        //先判断数据是否被读取过如果读取过直接返回items
        if (items != null)
        {
            return items;
        }
        //否则到本地文件中读取
        if (PlayerPrefs.HasKey("packageLocalData"))
        {
            //使读取到内存中变成字符串
            string inventoryJson = PlayerPrefs.GetString("packageLocalData");
            //反序列化
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
    public string uid;//唯一标识符
    public int id;
    public int num;
    public int level;
    public bool isNew;

    public override string ToString()
    {
        return string.Format("{0},{1}", id, num);
    }
    //使用工厂方法进行初始化
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
