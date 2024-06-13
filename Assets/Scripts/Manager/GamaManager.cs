using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Language language;

    public bool load;

    public Mode mode;
    public KeyCode menuKey;

    public List<KeyCode> keys;

    public Action languageChange;

    public LoadingTableResult LoadResult { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            if (load)
                for (int i = 0; i < transform.childCount; i++)
                    transform.GetChild(i).gameObject.SetActive(i == 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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
            case 1:
                return ScenarioTable.address;
            case 2:
                return ScenarioTextTable.address;
            case 3:
                return NoticeTable.address;
            case 4:
                return GoodsTable.address;
            case 5:
                return EquipTable.address;
            case 6:
                return CharacterTable.address;
            case 7:
                return CharacterSkillTable.address;
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
            case 1:
                return ScenarioTable.sheet;
            case 2:
                return ScenarioTextTable.sheet;
            case 3:
                return NoticeTable.sheet;
            case 4:
                return GoodsTable.sheet;
            case 5:
                return EquipTable.sheet;
            case 6:
                return CharacterTable.sheet;
            case 7:
                return CharacterSkillTable.sheet;
            default:
                return 0;
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
#else
        Application.Quit();
#endif
    }
}
