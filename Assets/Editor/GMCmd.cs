using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMCmd 
{
    //������Ա�֤unity�����˵���������һ����ť��������ȥִ����Ӧ���߼�,,,�÷����Ƕ�����UnityEditor�µ�Ӧ���������ռ�using UnityEditor
    [MenuItem("CMCmd/��ȡ���")]
    public static void ReadTable()
    {
        packageTable packagetable = Resources.Load<packageTable>("DataTable/packageTable");
        if (packagetable == null)
        {
            Debug.LogError("Failed to load packageTable from resources.");
            return;
        }
        foreach (packageTableItem packageitem in packagetable.DataList)
        {
            Debug.Log(packageitem.id + "���֣�" + packageitem.name + " ��������" + packageitem.description + "�����ܣ�" + packageitem.skillDescription);
        }
    }
    [MenuItem("CMCmd/��������")]
    public static void CreateLocalPackageData()
    {
        //��������
        packageLocalData.Instance.items = new List<packageLocalItem>();
        for (int i = 1; i < 9; i++)
        {
            //packageLocalItem packageLocalItems = new()
            //{
            //    uid = Guid.NewGuid().ToString(),//����һ��Ψһ�ַ�
            //    id = i,
            //    num = i,
            //    level = i,
            //    isNew = i % 2 == 1
            //};
            packageLocalItem packageLocalItems = new packageLocalItem(Guid.NewGuid().ToString(), i, i, i, i % 2 == 1);
            packageLocalData.Instance.items.Add(packageLocalItems);
        }

        packageLocalData.Instance.savePackage();
    }
    [MenuItem("CMCmd/��ȡ")]
    public static void ReadLocalPackageData()
    {
        List<packageLocalItem> readItems=packageLocalData.Instance.loadPackage();
        foreach (packageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("CMCmd/�򿪱���������")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel); 
    }
}
