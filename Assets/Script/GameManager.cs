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

    public int Gold { get; private set; } // 골드 값
    public int UnitCost = 10; // 유닛 생성 비용 예시

    private void Awake()
    {
        SoonsoonData.Instance.GAM = this;
        _playerCastle = GameObject.FindGameObjectWithTag(PlayerCastleTag)?.GetComponent<CastleUnit>();
        _aiCastle = GameObject.FindGameObjectWithTag(AICastleTag)?.GetComponent<CastleUnit>();

        if (_playerCastle is null || _aiCastle is null)
            throw new System.NullReferenceException("성채를 찾을 수 없습니다!");
    }

    public bool TryUseGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            //Debug.Log($"Gold spent: {amount}, Remaining: {Gold}");
            // OnGoldChanged?.Invoke(Gold); // 골드 변경 이벤트 발동
            return true;
        }
        //Debug.LogWarning("Not enough gold!");
        return false;
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
        if (unit == null || !unit.gameObject.activeInHierarchy)
        {
            // Debug.LogWarning("GetTarget: 요청한 CharacterUnit이 null이거나 비활성화/파괴되어 대상 탐색을 중단합니다.");
            return null; // 유효하지 않은 unit이므로 대상을 찾을 수 없음
        }

        CharacterUnit tUnit = null;

        List<CharacterUnit> tList = new List<CharacterUnit>();
        switch (team)
        {
            case Team.P1:
                {
                    tList = SpawnManager.Instance.PlayerUnits; 
                    Debug.Log("GetTarget: " + unit.name + " (" + team + ") 이 AI 유닛 리스트 탐색 중. 리스트 크기: " + SpawnManager.Instance.AIUnits.Count);
                    break;
                }
            case Team.P2:
                {
                    tList = SpawnManager.Instance.AIUnits;
                    Debug.Log("GetTarget: " + unit.name + " (" + team + ") 이 플레이어 유닛 리스트 탐색 중. 리스트 크기: " + SpawnManager.Instance.PlayerUnits.Count);
                    break;
                }
        }

        float tSDis = 99999;

        for (var i = 0; i < tList.Count; i++)
        {
            if (tList[i] == null || !tList[i].gameObject.activeInHierarchy)
            {
                // 리스트에서 제거하는 로직을 추가하는 것을 고려해볼 수 있습니다.
                 //SpawnManager.Instance.PlayerUnits.RemoveAt(i);
                 //i--; // 제거 후에는 인덱스를 하나 줄여야 다음 요소를 건너뛰지 않습니다.
                continue; // 유효하지 않은 대상이므로 건너뜁니다.
            }
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
                            //Debug.Log("GetTarget: " + unit.name + " (" + team + ") 이 새로운 대상 찾음: " + tUnit.name + " (팀: " + tUnit.Teams + ") 거리: " + tDis);
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
