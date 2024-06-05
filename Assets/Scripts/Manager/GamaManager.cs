using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public enum LoadingTableResult
{
    None = 0,
    Loading,
    Success,
}

public enum Mode
{
    PC = 1,
    Mobile = 2,
}

public class GamaManager : MonoBehaviour
{
    public static GamaManager Instance;

    public Language language;

    public LoadingTableResult LoadResult { get; private set; }

    void Start()
    {
        Instance = this;

        StartCoroutine(TextLoad());
    }

    public async Task<Exception> TableDataLoad(int n)
    {
        LoadResult = LoadingTableResult.Loading;

        try
        {
            TableManager.Instance.Init(n);
        }
        catch (Exception e)
        {
            Debug.LogError($"DataLoaging Number = {n}\nMessage: {e.Message}");
            return e;
        }


        LoadResult = LoadingTableResult.Success;

        return null;
    }
    
    IEnumerator TextLoad()
    {
        while (TableManager.Instance == null)
        {
            yield return null;
        }
        Coroutine coroutine = StartCoroutine(TableReader.LoadData(Address(0), Sheet(0), TableManager.Instance.GetData));
        yield return coroutine;

        Task<Exception> task = TableDataLoad(0);

        yield return new WaitUntil(() => task.IsCompleted);
    }

    public string Address(int n)
    {
        switch (n)
        {
            case 0:
                return LocalizeTextTable.address;
            default:
                return string.Empty;
        }
    }

    public int Sheet(int n)
    {
        switch (n)
        {
            case 0:
                return LocalizeTextTable.sheet;
            default:
                return 0;
        }
    }
}
