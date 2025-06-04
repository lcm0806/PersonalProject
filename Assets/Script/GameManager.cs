using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private const string PlayerCastleTag = "playerCastle";
    private const string AICastleTag = "aiCastle";

    [SerializeField]
    private static CastleUnit _playerCastle;
    [SerializeField]
    private static CastleUnit _aiCastle;

    public float _findTimer;


    private void Awake()
    {
        SoonsoonData.Instance.GAM = this;
        _playerCastle = GameObject.FindGameObjectWithTag(PlayerCastleTag)?.GetComponent<CastleUnit>();
        _aiCastle = GameObject.FindGameObjectWithTag(AICastleTag)?.GetComponent<CastleUnit>();

        if (_playerCastle is null || _aiCastle is null)
            throw new System.NullReferenceException("성채를 찾을 수 없습니다!");
    }

    // Start is called before the first frame update
    void Start()
    {
        //SetUnitList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //void SetUnitList()
    //{
    //    _p1UnitList.Clear();
    //    _p2UnitList.Clear();

        
    //}



    public CharacterUnit GetTarget(CharacterUnit unit,Team team)
    {
        CharacterUnit tUnit = null;

        List<CharacterUnit> tList = new List<CharacterUnit>();
        switch (team)
        {
            case Team.P1: tList = SpawnManager.Instance.PlayerUnits; break;
            case Team.P2: tList = SpawnManager.Instance.AIUnits; break;
        }

        float tSDis = 99999;

        for (var i = 0; i < tList.Count; i++)
        {
            float tDis = ((Vector2)tList[i].transform.localPosition - (Vector2)unit.transform.localPosition).sqrMagnitude; // 루트처리가 되지않은 거리를 찾는 것은 가벼움
            if(tDis <= unit.EnemyDetectionRadious * unit.EnemyDetectionRadious)
            {
                if (tList[i].gameObject.activeInHierarchy) 
                {
                    if (tList[i]._unitstate != CharacterUnit.UnitState.death)
                    {
                        if (tDis < tSDis)
                        {
                            tUnit = tList[i];
                            tSDis = tDis;
                            Debug.Log(tUnit);
                        }
                    }
                }
                
            }
        }

        return tUnit;
    }

    public static CastleUnit GetMyCastle(Team team)
    {
        switch (team)
        {
            case Team.P1:
                return _aiCastle;
            case Team.P2:
                return _playerCastle;
            default:
                throw new System.NotSupportedException($"팀 정보 {team}에 대한 성채는 없습니다.");
        }
    }
    //적팀에 해당하는 성채 유닛을 가져 옵니다.
    public static CastleUnit GetEnemyCastle(Team team)
    {
        switch (team)
        {
            case Team.P1:
                return _playerCastle;
            case Team.P2:
                return _aiCastle;
            default:
                throw new System.NotSupportedException($"팀 정보 {team}에 대한 성채는 없습니다.");
        }
    }
}
