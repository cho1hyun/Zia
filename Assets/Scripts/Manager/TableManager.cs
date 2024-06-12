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

    private const int _MAXDATA = 8;   //Å×ÀÌºí ÃÑ °¹¼ö

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
        else if (n == 1)
        {
            ScenarioData = new ScenarioTable_Parser().Parse();
        }
        else if (n == 2)
        {
            ScenarioTextData = new ScenarioTextTable_Parser().Parse();
        }
        else if (n == 3)
        {
            NoticeData = new NoticeTable_Parser().Parse();
        }
        else if (n == 4)
        {
            GoodsData = new GoodsTable_Parser().Parse();
        }
        else if (n == 5)
        {
            EquipData = new EquipTable_Parser().Parse();
        }
        else if (n == 6)
        {
            CharacterData = new CharacterTable_Parser().Parse();
        }
        else if (n == 7)
        {
            CharacterSkillData = new CharacterSkillTable_Parser().Parse();
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
    Dictionary<string, ScenarioTable> ScenarioData;
    Dictionary<int, ScenarioTextTable> ScenarioTextData;
    Dictionary<int, NoticeTable> NoticeData;
    Dictionary<int, GoodsTable> GoodsData;
    Dictionary<int, EquipTable> EquipData;
    Dictionary<int, CharacterTable> CharacterData;
    Dictionary<int, CharacterSkillTable> CharacterSkillData;

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

    public ScenarioTable GetScenario(string id)
    {
        return ScenarioData[id];
    }

    public string GetScenarioText(int id)
    {
        switch (GamaManager.Instance.language)
        {
            case Language.None:
                return ScenarioTextData[id].id.ToString();
            case Language.KOR:
                return ScenarioTextData[id].kor;
            case Language.ENG:
                return ScenarioTextData[id].eng;
            case Language.JAP:
                return ScenarioTextData[id].jap;
            default:
                return string.Empty;
        }
    }

    public NoticeTable GetNotice(int id)
    {
        return NoticeData[id];
    }

    public List<NoticeTable> GetNoticeAll()
    {
        List<NoticeTable> noticeList = new List<NoticeTable>();

        foreach (var item in NoticeData)
        {
            noticeList.Add(item.Value);
        }
        return noticeList;
    }

    public GoodsTable GetGoods(int id)
    {
        return GoodsData[id];
    }

    public EquipTable GetEquip(int id)
    {
        return EquipData[id];
    }

    public CharacterTable GetCharacter(int id)
    {
        return CharacterData[id];
    }

    public CharacterSkillTable GetCharacterSkillSet(int id)
    {
        return CharacterSkillData[id];
    }

    public SkillSet GetCharacterSkill(int id, int skillid)
    {
        return CharacterSkillData[id].skillSet[skillid];
    }

    public SkillSet GetCharacterSkill(int id, SkillType type)
    {
        foreach (var item in CharacterSkillData[id].skillSet)
        {
            if (item.Value.type == type)
            {
                return item.Value;
            }
        }

        return null;
    }
}
