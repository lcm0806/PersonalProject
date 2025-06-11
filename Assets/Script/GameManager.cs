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

    public int Gold { get; private set; } // ��� ��
    public int UnitCost = 10; // ���� ���� ��� ����

    private void Awake()
    {
        SoonsoonData.Instance.GAM = this;
        _playerCastle = GameObject.FindGameObjectWithTag(PlayerCastleTag)?.GetComponent<CastleUnit>();
        _aiCastle = GameObject.FindGameObjectWithTag(AICastleTag)?.GetComponent<CastleUnit>();

        if (_playerCastle is null || _aiCastle is null)
            throw new System.NullReferenceException("��ä�� ã�� �� �����ϴ�!");
    }

    public bool TryUseGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            //Debug.Log($"Gold spent: {amount}, Remaining: {Gold}");
            // OnGoldChanged?.Invoke(Gold); // ��� ���� �̺�Ʈ �ߵ�
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
            // Debug.LogWarning("GetTarget: ��û�� CharacterUnit�� null�̰ų� ��Ȱ��ȭ/�ı��Ǿ� ��� Ž���� �ߴ��մϴ�.");
            return null; // ��ȿ���� ���� unit�̹Ƿ� ����� ã�� �� ����
        }

        CharacterUnit tUnit = null;

        List<CharacterUnit> tList = new List<CharacterUnit>();
        switch (team)
        {
            case Team.P1:
                {
                    tList = SpawnManager.Instance.PlayerUnits; 
                    Debug.Log("GetTarget: " + unit.name + " (" + team + ") �� AI ���� ����Ʈ Ž�� ��. ����Ʈ ũ��: " + SpawnManager.Instance.AIUnits.Count);
                    break;
                }
            case Team.P2:
                {
                    tList = SpawnManager.Instance.AIUnits;
                    Debug.Log("GetTarget: " + unit.name + " (" + team + ") �� �÷��̾� ���� ����Ʈ Ž�� ��. ����Ʈ ũ��: " + SpawnManager.Instance.PlayerUnits.Count);
                    break;
                }
        }

        float tSDis = 99999;

        for (var i = 0; i < tList.Count; i++)
        {
            if (tList[i] == null || !tList[i].gameObject.activeInHierarchy)
            {
                // ����Ʈ���� �����ϴ� ������ �߰��ϴ� ���� ����غ� �� �ֽ��ϴ�.
                 //SpawnManager.Instance.PlayerUnits.RemoveAt(i);
                 //i--; // ���� �Ŀ��� �ε����� �ϳ� �ٿ��� ���� ��Ҹ� �ǳʶ��� �ʽ��ϴ�.
                continue; // ��ȿ���� ���� ����̹Ƿ� �ǳʶݴϴ�.
            }
            float tDis = ((Vector2)tList[i].transform.localPosition - (Vector2)unit.transform.localPosition).sqrMagnitude; // ��Ʈó���� �������� �Ÿ��� ã�� ���� ������
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
                            //Debug.Log("GetTarget: " + unit.name + " (" + team + ") �� ���ο� ��� ã��: " + tUnit.name + " (��: " + tUnit.Teams + ") �Ÿ�: " + tDis);
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
                throw new System.NotSupportedException($"�� ���� {team}�� ���� ��ä�� �����ϴ�.");
        }
    }
    //������ �ش��ϴ� ��ä ������ ���� �ɴϴ�.
    public static CastleUnit GetEnemyCastle(Team team)
    {
        switch (team)
        {
            case Team.P1:
                return _playerCastle;
            case Team.P2:
                return _aiCastle;
            default:
                throw new System.NotSupportedException($"�� ���� {team}�� ���� ��ä�� �����ϴ�.");
        }
    }
}
