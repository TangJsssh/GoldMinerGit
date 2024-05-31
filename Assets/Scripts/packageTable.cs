using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ʹ���Ҽ�������unity�༭���д���һ�������ļ���������������һ���ǲ˵����ƣ�һ���Ǵ������ļ�������
[CreateAssetMenu(menuName="Goldminer/packageTable",fileName ="packageTable")]
//�������ļ��е�ÿһ��Ǳ����е�һ������
public class packageTable : ScriptableObject
{
    //ʹ���б����洢��������
  public List<packageTableItem> DataList = new List<packageTableItem>();
}
//ʹ������unity�б༭���б���
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
