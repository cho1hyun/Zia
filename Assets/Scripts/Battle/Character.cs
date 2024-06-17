using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;

    public List<Transform> Characters;

    public Animator Main;

    KeyCode a, b, c, d;

    float x;
    float z;
    bool back;

    List<int> characterId;

    void Update()
    {
        if (MoveCheck() && (Main.GetCurrentAnimatorStateInfo(0).IsName("Idle") || Main.GetCurrentAnimatorStateInfo(0).IsName("Run")))
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, back ? -1 : 1);

            transform.Translate(Vector3.forward * GetKeyInput().z * speed * Time.deltaTime);

            transform.eulerAngles += new Vector3(0, back ? -GetKeyInput().x : GetKeyInput().x * rotateSpeed * Time.deltaTime, 0);

            AnimMove(GetKeyInput() != Vector3.zero);
        }

        AttackCheck();
    }

    bool MoveCheck()
    {
        if (Main == null)
            return false;

        if (UiManager.Instance.ingame.Over.gameObject.activeSelf)
            return false;

        return true;
    }

    bool AtkCheck()
    {
        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return false;

        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            return false;

        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return false;

        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Evasion"))
            return false;

        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Evasion 1"))
            return false;

        if (Main.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            return false;

        return true;
    }

    void AttackCheck()
    {
        if (MoveCheck())
        {
            for (int i = 0; i < Main.GetComponent<SkillAction>().Weapon.Count; i++)
            {
                Main.GetComponent<SkillAction>().Weapon[i].GetComponent<BoxCollider>().isTrigger = AtkCheck();
                Main.GetComponent<SkillAction>().Weapon[i].GetComponent<MeshCollider>().isTrigger = AtkCheck();
            }
        }
    }

    public void SetCharacter(int num = 0)
    {
        characterId = new List<int>();
        for (int i = 0; i < Characters.Count; i++)
        {
            int id = GameManager.Instance.userData.characterSet[num][i].id;
            characterId.Add(id);
            Animator cha = Instantiate(Resources.Load("Character/" + id) as GameObject, Characters[i]).GetComponent<Animator>();
            cha.SetInteger("Type", id);

            if (Main == null)
                Main = cha.GetComponent<Animator>();
        }

        SetSkill();

        a = GameManager.Instance.keys[0];
        b = GameManager.Instance.keys[1];
        c = GameManager.Instance.keys[2];
        d = GameManager.Instance.keys[3];
    }

    void SetSkill()
    {
        int skillset = TableManager.Instance.GetCharacter(characterId[0]).skillset;
        UiManager.Instance.ingame.SkillGroup.GetChild(3).GetComponent<Skill>().SetSkill(TableManager.Instance.GetCharacterSkill(skillset, SkillType.skill1));
        UiManager.Instance.ingame.SkillGroup.GetChild(4).GetComponent<Skill>().SetSkill(TableManager.Instance.GetCharacterSkill(skillset, SkillType.skill2));
        UiManager.Instance.ingame.SkillGroup.GetChild(5).GetComponent<Skill>().SetSkill(TableManager.Instance.GetCharacterSkill(skillset, SkillType.skill3));
        UiManager.Instance.ingame.SkillGroup.GetChild(6).GetComponent<Skill>().SetSkill(TableManager.Instance.GetCharacterSkill(skillset, SkillType.Ult));

        for (int i = 0; i < UiManager.Instance.ingame.SkillGroup.childCount; i++)
        {
            UiManager.Instance.ingame.SkillGroup.GetChild(i).GetComponent<Skill>().ChangeCharacter();
        }

        for (int i = 0; i < UiManager.Instance.ingame.Skills.childCount; i++)
        {
            UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().SetSkillUlt(characterId[i + 1]);
            UiManager.Instance.ingame.Skills.GetChild(i).GetComponent<Skill>().ChangeCharacter();
        }
    }

    Vector3 GetKeyInput()
    {
        if (Input.GetKeyDown(a))
        {
            z = 1;
            back = false;
        }

        if (Input.GetKeyDown(b))
        {
            z = -1;
            back = true;
        }

        if ((z > 0 && Input.GetKeyUp(a)) || (z < 0 && Input.GetKeyUp(b))) 
        {
            z = 0;
        }

        if (Input.GetKeyDown(c))
        {
            x = -1;
        }

        if (Input.GetKeyDown(d))
        {
            x = 1;
        }

        if ((x < 0 && Input.GetKeyUp(c)) || (x > 0 && Input.GetKeyUp(d)))
        {
            x = 0;
        }

        return new Vector3(x, 0, z);
    }

    void AnimMove(bool isMove)
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].GetChild(0).GetComponent<Animator>().SetBool("Move", isMove);
        }
    }

    public void SwitchCharacter(int n = 1)
    {
        Vector3 pos = Characters[0].localPosition;
        Characters[0].localPosition = Characters[n].localPosition;
        Characters[n].localPosition = pos;

        Transform transform = Characters[0];
        Characters[0] = Characters[n];
        Characters[n] = transform;

        int id = characterId[0];
        characterId[0] = characterId[n];
        characterId[n] = id;

        Main = Characters[0].GetChild(0).GetComponent<Animator>();
        SetSkill();
    }
}
