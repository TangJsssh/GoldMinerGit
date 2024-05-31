using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//使用右键可以在unity编辑器中创建一个配置文件，传入两个参数一个是菜单名称，一个是创建后文件的名称
[CreateAssetMenu(menuName="Goldminer/packageTable",fileName ="packageTable")]
//该配置文件中的每一项都是背包中的一件物体
public class packageTable : ScriptableObject
{
    //使用列表来存储所有数据
  public List<packageTableItem> DataList = new List<packageTableItem>();
}
//使可以在unity中编辑下列变量
[System.Serializable]
public class packageTableItem
{
    public int id;
    public int type;
    public int star;
    public string name;
    public string description;
    public string skillDescription;
    public string imagePath;
    public int Num;
}
