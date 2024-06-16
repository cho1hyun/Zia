using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

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

    private const int _MAXDATA = 15;   //Å×ÀÌºí ÃÑ °¹¼ö

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
        else if (n == 8)
        {
            MonsterData = new MonsterTable_Parser().Parse();
        }
        else if (n == 9)
        {
            MonsterSkillData = new MonsterSkillTable_Parser().Parse();
        }
        else if (n == 10)
        {
            StageDungeonData = new StageDungeonTable_Parser().Parse();
        }
        else if (n == 11)
        {
            StageWeatherData = new StageWeatherTable_Parser().Parse();
        }
        else if (n == 12)
        {
            StageSpawnOrderData = new StageSpawnOrderTable_Parser().Parse();
        }
        else if (n == 13)
        {
            StageSpawnData = new StageSpawnTable_Parser().Parse();
        }
        else if (n == 14)
        {
            QuestData = new QuestTable_Parser().Parse();
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
    Dictionary<int, MonsterTable> MonsterData;
    Dictionary<int, MonsterSkillTable> MonsterSkillData;
    Dictionary<int, StageDungeonTable> StageDungeonData;
    Dictionary<int, StageWeatherTable> StageWeatherData;
    Dictionary<int, StageSpawnOrderTable> StageSpawnOrderData;
    Dictionary<int, StageSpawnTable> StageSpawnData;
    Dictionary<int, QuestTable> QuestData;

    public string GetLocalizeText(int id)
    {
        if (id == 0 || LocalizeTextData == null)
            return string.Empty;

        switch (GameManager.Instance.language)
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
        if (!IsInitialized)
            return null;

        return ScenarioData[id];
    }

    public List<ScenarioTable> GetScenarioList(int scenario)
    {
        List<ScenarioTable> scenarios = new List<ScenarioTable>();

        bool bookmark = false;

        foreach (var item in ScenarioData)
        {
            if (item.Value.ScenarioID == scenario)
                bookmark = true;

            if (bookmark)
                scenarios.Add(item.Value);

            if (item.Value.Command == ScenarioCommand.EndScenario && bookmark)
                return scenarios;
        }

        return scenarios;
    } 

    public string GetScenarioText(int id)
    {
        if (!IsInitialized || id == 0)
            return string.Empty;

        switch (GameManager.Instance.language)
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
        if (!IsInitialized)
            return null;

        return NoticeData[id];
    }

    public List<NoticeTable> GetNoticeAll()
    {
        if (!IsInitialized)
            return null;

        List<NoticeTable> noticeList = new List<NoticeTable>();

        foreach (var item in NoticeData)
        {
            noticeList.Add(item.Value);
        }
        return noticeList;
    }

    public GoodsTable GetGoods(int id)
    {
        if (!IsInitialized)
            return null;

        return GoodsData[id];
    }

    public EquipTable GetEquip(int id)
    {
        if (!IsInitialized)
            return null;

        return EquipData[id];
    }

    public CharacterTable GetCharacter(int id)
    {
        if (!IsInitialized)
            return null;

        return CharacterData[id];
    }

    public Sprite GetCharacterAttribute(Attribute_ attribute_)
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Icon");

        string fileNasme = "Attribute_";
        switch (attribute_)
        {
            case Attribute_.None:
                return null;
            case Attribute_.Mare:
                fileNasme += 11;
                break;
            case Attribute_.Flamma:
                fileNasme += 1;
                break;
            case Attribute_.Vita:
                fileNasme += 15;
                break;
            case Attribute_.Lucere:
                fileNasme += 8;
                break;
            case Attribute_.Umbra:
                fileNasme += 6;
                break;
            default:
                return null;
        }

        return spriteAtlas.GetSprite(fileNasme);
    }

    public Color GetAttributeColor(Attribute_ attribute_)
    {
        switch (attribute_)
        {
            case Attribute_.None:
                return Color.white;
            case Attribute_.Mare:
                ColorUtility.TryParseHtmlString("#7BEAFFFF", out Color Mare);
                return Mare;
            case Attribute_.Flamma:
                ColorUtility.TryParseHtmlString("#E23636FF", out Color Flamma);
                return Flamma;
            case Attribute_.Vita:
                ColorUtility.TryParseHtmlString("#54FB8CFF", out Color Vita);
                return Vita;
            case Attribute_.Lucere:
                ColorUtility.TryParseHtmlString("#F4FFDDFF", out Color Lucere);
                return Lucere;
            case Attribute_.Umbra:
                ColorUtility.TryParseHtmlString("#6B5B7BFF", out Color Umbra);
                return Umbra;
            default:
                return Color.black;
        }
    }

    public CharacterSkillTable GetCharacterSkillSet(int id)
    {
        if (!IsInitialized)
            return null;

        return CharacterSkillData[id];
    }

    public SkillSet GetCharacterSkill(int id, int skillid)
    {
        if (!IsInitialized)
            return null;

        return CharacterSkillData[id].skillSet[skillid];
    }

    public SkillSet GetCharacterSkill(int id, SkillType type)
    {
        if (!IsInitialized)
            return null;

        foreach (var item in CharacterSkillData[id].skillSet)
        {
            if (item.Value.type == type)
            {
                return item.Value;
            }
        }

        return null;
    }

    public MonsterTable GetMonster(int id)
    {
        return MonsterData[id];
    }

    public MonsterSkillTable GetMonsterSkill(int id)
    {
        return MonsterSkillData[id];
    }

    public StageDungeonTable GetStageDungeon(int id)
    {
        return StageDungeonData[id];
    }

    public StageDungeonTable GetPreviousStageDungeon(int id)
    {
        StageDungeonTable Previous = StageDungeonData[id];

        foreach (var item in StageDungeonData)
        {
            if (item.Key == id)
                return Previous;

            if (item.Value.DgChapter != 0 && item.Value.DgStage != 0)
                Previous = item.Value;
        }

        return StageDungeonData[id];
    }

    public StageDungeonTable GetNextStageDungeon(int id)
    {
        bool next = false;

        foreach (var item in StageDungeonData)
        {
            if (next)
                return item.Value;

            if (item.Key == id)
                next = true;
        }

        return StageDungeonData[id];
    }

    public List<StageDungeonTable> GetStageDungeonList(int Chapter)
    {
        List<StageDungeonTable> list = new List<StageDungeonTable>();

        foreach (var item in StageDungeonData)
        {
            if (item.Value.DgChapter == Chapter)
            {
                list.Add(item.Value);
            }
        }
        return list;
    }

    public StageWeatherTable GetStageWeather(int id)
    {
        return StageWeatherData[id];
    }

    public StageSpawnOrderTable GetStageSpawnOrder(int id)
    {
        return StageSpawnOrderData[id];
    }

    public StageSpawnTable GetStageSpawnData(int id)
    {
        return StageSpawnData[id];
    }

    public QuestTable GetQuest(int id)
    {
        return QuestData[id];
    }
    public QuestTable GetFirstQuest()
    {
        return QuestData.First().Value;
    }
}
