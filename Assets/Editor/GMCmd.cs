using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMCmd 
{
    //这个特性保证unity顶部菜单栏中生成一个按钮，方便点击去执行相应的逻辑,,,该方法是定义在UnityEditor下的应引用命名空间using UnityEditor
    [MenuItem("CMCmd/读取表格")]
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
            Debug.Log(packageitem.id + "名字：" + packageitem.name + " ，描述：" + packageitem.description + "，技能：" + packageitem.skillDescription);
        }
    }
    [MenuItem("CMCmd/创建背包")]
    public static void CreateLocalPackageData()
    {
        //保存数据
        packageLocalData.Instance.items = new List<packageLocalItem>();
        for (int i = 1; i < 9; i++)
        {
            //packageLocalItem packageLocalItems = new()
            //{
            //    uid = Guid.NewGuid().ToString(),//生成一个唯一字符
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
    [MenuItem("CMCmd/读取")]
    public static void ReadLocalPackageData()
    {
        List<packageLocalItem> readItems=packageLocalData.Instance.loadPackage();
        foreach (packageLocalItem item in readItems)
        {
            Debug.Log(item);
        }
    }
    [MenuItem("CMCmd/打开背包主界面")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel); 
    }
}
