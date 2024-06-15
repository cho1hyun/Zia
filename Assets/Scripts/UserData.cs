using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string userId;                               //유저 아이디
    public Dictionary<int, CharacterSet> characters;    //보유 캐릭터
    public CharacterSet mainCharacter;                  //메인 캐릭터
    public List<List<CharacterSet>> characterSet;       //캐릭터 프리셋
    public Dictionary<int, int> gooods;                 //보유 재화

    public int clearStage;                              //현재까지 클리어한 스테이지 중 가장 높은 스테이지
    public int lastStage;                               //마지막으로 도전한 스테이지

    public UserData SetUserData(string _id, Dictionary<int, CharacterSet> _characters = null, CharacterSet _mainCharacter = null, List<List<CharacterSet>> _characterSet = null, Dictionary<int, int> _gooods = null, int _clearStage = 0, int _lastStage = 0)
    {
        CharacterSet character0 = new CharacterSet().SetCharacterSet(90000, 1, 0, new EquipSet().SetEquipSet(20000, 1), new EquipSet().SetEquipSet(30000, 1));
        CharacterSet character1 = new CharacterSet().SetCharacterSet(90001, 1, 0, new EquipSet().SetEquipSet(20001, 1), new EquipSet().SetEquipSet(30001, 1));
        CharacterSet character2 = new CharacterSet().SetCharacterSet(90002, 1, 0, new EquipSet().SetEquipSet(20002, 1), new EquipSet().SetEquipSet(30002, 1));
        List<List<CharacterSet>> characterList = new List<List<CharacterSet>>() { new List<CharacterSet>() { character0, character1, character2 } };
        Dictionary<int, CharacterSet> characterSets = new Dictionary<int, CharacterSet>() { { character0.id, character0 }, { character1.id, character1 }, { character2.id, character2 } };
        Dictionary<int, int> goood = new Dictionary<int, int>() { { 10000, 0 }, { 10001, 0 } };

        userId = _id;
        characters = _characters == null ? characterSets : _characters;
        mainCharacter = _mainCharacter == null ? character0 : _mainCharacter;
        characterSet = _characterSet == null ? characterList : _characterSet;
        gooods = _gooods == null ? goood : _gooods;

        clearStage = _clearStage == 0 ? 0 : _clearStage;
        lastStage = _lastStage == 0 ? 0 : _lastStage;

        return this;
    }
}

public class CharacterSet
{
    public int id;
    public int level;
    public int exp;

    public EquipSet equip;
    public EquipSet memoriao;

    public CharacterSet SetCharacterSet(int _id, int _level, int _exp, EquipSet _equip = null, EquipSet _memoriao = null)
    {
        id = _id;
        level = _level;
        exp = _exp;
        equip = _equip;
        memoriao = _memoriao;

        return this;
    }
}

public class EquipSet
{
    public int id;
    public int level;

    public EquipSet SetEquipSet(int _id,int _level)
    {
        id = _id;
        level = _level;

        return this;
    }
}
