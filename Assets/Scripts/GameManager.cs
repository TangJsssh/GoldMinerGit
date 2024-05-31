using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private packageTable _packageTable;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);

    }
     void Start()
    {
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        
    }
    void Update()
    {
        
    }
    public packageTable GetPackageTable()
    {
        if(_packageTable == null)
        {
            _packageTable = Resources.Load<packageTable>("DataTable/packageTable");
        }
        return _packageTable;
    }
    public List<packageLocalItem> GetPackageLocalData()
    {
        return packageLocalData.Instance.loadPackage();
    }
    //显示优先级
    public List<packageLocalItem> GetSortPackageLocalData()
    {
        List<packageLocalItem> LocalItems = packageLocalData.Instance.loadPackage();
        LocalItems.Sort(new PackageItemComparer());
        return LocalItems;
    }
    public class PackageItemComparer : IComparer<packageLocalItem>
    {
        public int Compare(packageLocalItem x, packageLocalItem y)
        {
            packageTableItem xItem = GameManager.Instance.GetPackageLocalItemByID(x.id);
            packageTableItem yItem=GameManager.Instance.GetPackageLocalItemByID(y.id);
            int starComparison=yItem.star.CompareTo(xItem.star);
            if(starComparison == 0)
            {
                int idComparison=yItem.id.CompareTo(xItem.id);
                if(idComparison == 0)
                {
                    return y.level.CompareTo(x.level);
                }
                return idComparison;
            }
            return starComparison;


        }
    }
    public packageTableItem GetPackageLocalItemByID(int id)
    {
        List<packageTableItem> packageDataList = GetPackageTable().DataList;
        foreach(packageTableItem item in packageDataList)
        {
            if(item.id == id)
            {
                return item;
            }
        }
        return null;
    }
    public packageLocalItem GetPackageLocalItemByUID(string uid)
    {
        List<packageLocalItem> packageDataList = GetPackageLocalData();
        foreach(packageLocalItem item in packageDataList)
        {
            if (item.uid == uid)
            {
                return item;
            }
        }
        return null;
    }

}
