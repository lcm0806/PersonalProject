using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleUnit : GameUnit
{
    public void Awake()
    {
        if (gameObject.CompareTag("playerCastle")) // Unity 에디터에서 PlayerCastle 태그를 만들고 성채에 할당
        {
            SetTeam(Team.P1); // SetTeam 메서드가 GameUnit 또는 CastleUnit에 정의되어 있어야 합니다.
        }
        else if (gameObject.CompareTag("aiCastle")) // Unity 에디터에서 AICastle 태그를 만들고 성채에 할당
        {
            SetTeam(Team.P2);
        }
        else
        {          
            SetTeam(Team.None); // 안전을 위해 None으로 설정
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
