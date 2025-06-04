using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUnit : GameUnit
{
    public void Awake()
    {
        if (gameObject.CompareTag("playerCastle")) // Unity �����Ϳ��� PlayerCastle �±׸� ����� ��ä�� �Ҵ�
        {
            SetTeam(Team.P1); // SetTeam �޼��尡 GameUnit �Ǵ� CastleUnit�� ���ǵǾ� �־�� �մϴ�.
        }
        else if (gameObject.CompareTag("aiCastle")) // Unity �����Ϳ��� AICastle �±׸� ����� ��ä�� �Ҵ�
        {
            SetTeam(Team.P2);
        }
        else
        {          
            SetTeam(Team.None); // ������ ���� None���� ����
        }
    }

    public void Update()
    {
        
    }
    public override void SetTeam(Team settingValue)
    {
        if (settingValue == Team.P2)
        {
            //Debug.Log(settingValue.ToString());
        }
        else if (settingValue == Team.P1)
        {
            //Debug.Log(settingValue.ToString());
        }
        base.SetTeam(settingValue);
    }



    protected override void OnDie()
    {
        Debug.Log("END Game!");
    }
}
