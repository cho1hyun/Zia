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

    void Update()
    {
        if (Main != null && (Main.GetCurrentAnimatorStateInfo(0).IsName("Idle") || Main.GetCurrentAnimatorStateInfo(0).IsName("Run")))
        {
            transform.GetChild(0).localScale = new Vector3(1, 1, back ? -1 : 1);

            transform.Translate(Vector3.forward * GetKeyInput().z * speed * Time.deltaTime);

            transform.eulerAngles += new Vector3(0, back ? -GetKeyInput().x : GetKeyInput().x * rotateSpeed * Time.deltaTime, 0);

            AnimMove(GetKeyInput() != Vector3.zero);
        }
    }

    public void SetCharacter(int num = 0)
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Animator cha = Instantiate(Resources.Load("Character/" + GameManager.Instance.userData.characterSet[0][i].id) as GameObject, Characters[i]).GetComponent<Animator>();
            cha.SetInteger("Type", GameManager.Instance.userData.characterSet[num][i].id);

            if (Main == null)
                Main = cha.GetComponent<Animator>();
        }

        a = GameManager.Instance.keys[0];
        b = GameManager.Instance.keys[1];
        c = GameManager.Instance.keys[2];
        d = GameManager.Instance.keys[3];
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

        Main = Characters[0].GetChild(0).GetComponent<Animator>();
    }
}
