using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DungeonInfo : MonoBehaviour
{
    public Transform MainCharacter;
    public Transform Characters;
    public Transform Quest;
    public Transform Dungeon;

    public GameObject PrevBtn;
    public GameObject NextBtn;

    int mainCharacterID;

    int dungeonid;

    void OnEnable()
    {
        SetDungeonInfo();
    }

    void SetDungeonInfo(int setNum = 0)
    {
        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Icon");

        for (int i = 0; i < Characters.childCount; i++)
        {
            CharacterTable character = TableManager.Instance.GetCharacter(GameManager.Instance.userData.characterSet[setNum][i].id);

            Characters.GetChild(i).GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(character.id.ToString());
            Characters.GetChild(i).GetChild(1).GetComponent<Image>().sprite = TableManager.Instance.GetCharacterAttribute(character.attribute);
            Characters.GetChild(i).GetChild(2).GetComponent<TMP_Text>().text = character.atk.ToString();
            Characters.GetChild(i).GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = TableManager.Instance.GetLocalizeText(character.name);
            Characters.GetChild(i).GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(25), GameManager.Instance.userData.characters[character.id].level);
            Characters.GetChild(i).GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { SetMainCharacter(character.id); });
        }

        SetMainCharacter(GameManager.Instance.userData.characterSet[setNum].First().id);

        SetDungeon();
    }

    void SetDungeon(StageDungeonTable dungeon = null)
    {
        dungeon = dungeon == null ? DungeonSet() : dungeon;

        dungeonid = dungeon.id;

        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Character");
        SpriteAtlas spriteAtlasI = Resources.Load<SpriteAtlas>("Atlas/Icon");

        StageWeatherTable weather = TableManager.Instance.GetStageWeather(dungeon.DgWth);

        Dungeon.GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(GetBossInfo(dungeon).id.ToString());
        Dungeon.GetChild(1).GetComponent<TMP_Text>().text = string.Format("{0}-{1}", dungeon.DgChapter, dungeon.DgStage);
        Dungeon.GetChild(2).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(weather.WthName));
        Dungeon.GetChild(2).GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = spriteAtlasI.GetSprite(weather.id.ToString());
        Dungeon.GetChild(3).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(GetBossInfo(dungeon).name));
        Dungeon.GetChild(3).GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = TableManager.Instance.GetCharacterAttribute(GetBossInfo(dungeon).attribute);

        QuestTable quest = TableManager.Instance.GetFirstQuest();

        Quest.GetChild(0).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(quest.name));
        Quest.GetChild(1).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(quest.des));
        Quest.GetChild(2).GetComponent<TMP_Text>().text = string.Format("[{0}/{1}]", 0, quest.accomplishValue);

        PrevBtn.SetActive(TableManager.Instance.GetPreviousStageDungeon(dungeon.id) != dungeon);
        NextBtn.SetActive(TableManager.Instance.GetNextStageDungeon(dungeon.id) != dungeon);
    }

    StageDungeonTable DungeonSet()
    {
        if (GameManager.Instance.userData.lastStage == 0)//입장 기록없으면
            return TableManager.Instance.GetStageDungeonList(1).First();//처음으로

        StageDungeonTable last = TableManager.Instance.GetStageDungeon(GameManager.Instance.userData.lastStage);

        if (last == TableManager.Instance.GetStageDungeon(GameManager.Instance.userData.clearStage))//마지막으로 입장했던 던전과 클리어한 마지막던전이 같으면
        {
            int chapter = last.DgChapter;

            if (last.DgStage == TableManager.Instance.GetStageDungeonList(chapter).Last().DgStage)//현제 챕터의 마지막 스테이지 인지확인하고
                return TableManager.Instance.GetStageDungeonList(chapter + 1).First();//마지막 스테이지면 다음 챕터의 첫 스테이지

            return TableManager.Instance.GetNextStageDungeon(last.id);//아니면 현제 챕터의 다음 스테이지
        }

        return last;//위 조건이 전부 아니면 마지막에 입장했던 던전
    }

    MonsterTable GetBossInfo(StageDungeonTable dungeon = null)
    {
        StageSpawnOrderTable spawnOrder = TableManager.Instance.GetStageSpawnOrder(dungeon.SpawnOrder1_GroupID);
        StageSpawnTable spawn = TableManager.Instance.GetStageSpawnData(spawnOrder.SpawnSet5_ID == 0 ? (spawnOrder.SpawnSet4_ID == 0 ? (spawnOrder.SpawnSet3_ID == 0 ? (spawnOrder.SpawnSet2_ID == 0 ? spawnOrder.SpawnSet1_ID : spawnOrder.SpawnSet2_ID) : spawnOrder.SpawnSet3_ID) : spawnOrder.SpawnSet4_ID) : spawnOrder.SpawnSet5_ID);
        return TableManager.Instance.GetMonster(spawn.mob2_ID);
    }

    public void SetMainCharacter(int id)
    {
        mainCharacterID = id;

        SpriteAtlas spriteAtlas = Resources.Load<SpriteAtlas>("Atlas/Character");
        SpriteAtlas spriteAtlasW = Resources.Load<SpriteAtlas>("Atlas/Equip");

        CharacterTable character = TableManager.Instance.GetCharacter(id);
        EquipTable equip = TableManager.Instance.GetEquip(GameManager.Instance.userData.characters[id].equip.id);

        MainCharacter.GetChild(0).GetComponent<Image>().sprite = spriteAtlas.GetSprite(character.id.ToString());
        MainCharacter.GetChild(1).GetChild(0).GetComponent<Image>().sprite = spriteAtlasW.GetSprite(equip.id.ToString());
        MainCharacter.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = TableManager.Instance.GetLocalizeText(equip.name);
        MainCharacter.GetChild(2).GetComponent<TMP_Text>().text = character.atk.ToString();
        MainCharacter.GetChild(3).GetChild(1).GetChild(0).GetComponent<Image>().sprite = TableManager.Instance.GetCharacterAttribute(character.attribute);
        MainCharacter.GetChild(3).GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = TableManager.Instance.GetLocalizeText(character.name);
        MainCharacter.GetChild(3).GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = string.Format(TableManager.Instance.GetLocalizeText(25), GameManager.Instance.userData.characters[id].level);

        for (int i = 0; i < Characters.childCount; i++)
        {
            Characters.GetChild(i).GetChild(4).gameObject.SetActive(false);
        }
    }

    public void CharacterChangeOn()
    {
        for (int i = 0; i < Characters.childCount; i++)
        {
            Characters.GetChild(i).GetChild(4).gameObject.SetActive(true);
            Characters.GetChild(i).GetChild(4).GetChild(0).gameObject.SetActive(Characters.GetChild(i).GetChild(0).GetComponent<Image>().sprite.name == TableManager.Instance.GetCharacter(mainCharacterID).id.ToString());
            Characters.GetChild(i).GetChild(4).GetChild(1).gameObject.SetActive(Characters.GetChild(i).GetChild(0).GetComponent<Image>().sprite.name != TableManager.Instance.GetCharacter(mainCharacterID).id.ToString());
        }
    }

    public void Prev()
    {
        SetDungeon(TableManager.Instance.GetPreviousStageDungeon(DungeonSet().id));
    }

    public void Next()
    {
        SetDungeon(TableManager.Instance.GetNextStageDungeon(DungeonSet().id));
    }

    public void LoadSceneDungeon()
    {
        GameManager.Instance.userData.lastStage = dungeonid;
        UiManager.Instance.Action(2);
        GameManager.Instance.gameObject.GetComponent<SoundManager>().PlayBgm("StageBGM");
        SceneManager.LoadScene(3);
    }
}
