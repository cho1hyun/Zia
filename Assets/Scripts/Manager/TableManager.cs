using System.Collections.Generic;

public class TableManager
{
    static TableManager _instance;
    public static TableManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new TableManager();

            return _instance;
        }
    }
    public delegate void OnInitializedTable();
    public static event OnInitializedTable OnInitializedTableEvent;
    public bool IsInitialized = false;

    private const int _MAXDATA = 1;   //Å×ÀÌºí ÃÑ °¹¼ö

    List<Dictionary<string, object>> data;

    public int getMaxData()
    {
        return _MAXDATA;
    }

    public void Init(int n)
    {
        if (n == 0)
        {
            LocalizeTextData = new LocalizeTextTable_Parser().Parse();
        }
        else if (n == getMaxData())
        {
            IsInitialized = true;
            OnInitializedTableEvent?.Invoke();
        }
    }

    public void GetData(List<Dictionary<string, object>> getData)
    {
        data = getData;
    }

    public List<Dictionary<string, object>> SetData()
    {
        return data;
    }

    Dictionary<int, LocalizeTextTable> LocalizeTextData;

    public string GetLocalizeText(int id)
    {
        switch (GamaManager.Instance.language)
        {
            case Language.None:
                return LocalizeTextData[id].id.ToString();
            case Language.KOR:
                return LocalizeTextData[id].kor;
            case Language.ENG:
                return LocalizeTextData[id].eng;
            case Language.JAP:
                return LocalizeTextData[id].jap;
            default:
                return string.Empty;
        }
    }
}
