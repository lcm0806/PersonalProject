using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const string UserSpawnerTag = "userSpawner";
    private const string AISpawnerTag = "aiSpawner";
    public static SpawnManager Instance;

    public List<CharacterUnit> PlayerUnits;
    public List<CharacterUnit> AIUnits;

    private ICharacterSpawner userSpawner;
    private ICharacterSpawner aiSpawner;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("�ߺ��� CharacterDataContainer �ν��Ͻ� ����. �� �ν��Ͻ��� �ı��մϴ�.");
            Destroy(gameObject); // �� (�ߺ���) ���� ������Ʈ�� �ı�
            return; // �� �̻� �� Awake �Լ��� �������� �ʰ� ����
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //if (Instance is null) Instance = this;
        //else
        //{
        //    Debug.LogError("SpawnManager �� �ΰ� �̻����� �̱��� ������ ���� �� �� �����ϴ�.");
        //}
        userSpawner = GameObject.FindGameObjectWithTag(UserSpawnerTag)?.GetComponent<ICharacterSpawner>();
        aiSpawner = GameObject.FindGameObjectWithTag(AISpawnerTag)?.GetComponent<ICharacterSpawner>();
        if (userSpawner is null || aiSpawner is null) throw new System.NullReferenceException("������ ã�� ����!");

        PlayerUnits = new List<CharacterUnit>();
        AIUnits = new List<CharacterUnit>();
    }
    /// <summary>
    /// �����ʿ� ���� ��û�� �����մϴ�.
    /// </summary>
    /// <param name="request">��ȯ�� ĳ���Ϳ����� ��û �����Դϴ�.</param>
    public void SendSpawnRequest(SpawnRequest request)
    {
        if (request.Equals(default(SpawnRequest))) Debug.LogWarning("spawn request�� �ʱ�ȭ ���� ���� �� ���ƿ�.");
        // ��ȯ �� ĳ���͸� �����ϱ� ���� ��ȯ��û�� �ش��ϴ� ���� ����Ʈ�� �߰��մϴ�.
        GetUnitList(request.TeamInfo).Add(SelectSpawner().Spawn(request));

        ICharacterSpawner SelectSpawner()
        {
            switch (request.TeamInfo)
            {
                case Team.P1:
                    return aiSpawner;
                case Team.P2:
                    return userSpawner;
                default:
                    throw new System.NotSupportedException($"�� ���� {request.TeamInfo}�� ���� ���ָ���Ʈ�� �������� �ʽ��ϴ�.");
            }
        }
    }

    public List<CharacterUnit> GetUnitList(Team friendlyTeamData)
    {
        switch (friendlyTeamData)
        {
            case Team.P1:
                return AIUnits;
            case Team.P2:
                return PlayerUnits;
            default:
                throw new System.NotSupportedException($"�� ���� {friendlyTeamData}�� ���� ���ָ���Ʈ�� �������� �ʽ��ϴ�.");
        }
    }

    public List<CharacterUnit> GetEnemyUnitList(Team myTeamData)
    {
        switch (myTeamData)
        {
            case Team.P2:
                return PlayerUnits;
            case Team.P1:
                return AIUnits;
            default:
                throw new System.NotSupportedException($"�� ���� {myTeamData}�� ���� ���ָ���Ʈ�� �������� �ʽ��ϴ�.");
        }
    }
}
