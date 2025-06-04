using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour , ICharacterSpawner
{
    public Team TeamInfo { get => _teamInfo; }
    [SerializeField] private Team _teamInfo = Team.None;

    [Header("AI Spawner Settings")]
    [SerializeField] private float _spawnInterval = 5f; // ���� ���� ���� (5��)
    private Coroutine _spawnRoutine; // �ڷ�ƾ ���� ����

    private void Start()
    {
        if (_teamInfo == Team.P1)
        {
            StartAutoSpawn();
        }
    }

    // �ڵ� ���� ���� �Լ�
    public void StartAutoSpawn()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine); // �̹� �ڷ�ƾ�� ���� ���̸� ����
        }
        _spawnRoutine = StartCoroutine(AutoSpawnRoutine());
    }

    private IEnumerator AutoSpawnRoutine()
    {
        while (true) // ���� �ݺ��Ͽ� ���������� ����
        {
            yield return new WaitForSeconds(_spawnInterval); // ������ ���ݸ�ŭ ���

            // ----------------------------------------------------
            // ���� ���� ID ���� ���� (����)
            // CharacterDataContainer�� ���� ID�� �������� �Լ��� �ִٰ� �����մϴ�.
            // ������, �̰����� ���� List<string>�� ����� �������� �����ϴ� ������ �ʿ��մϴ�.
            // ----------------------------------------------------
            string randomCharacterID = CharacterDataContainer.Instance.GetRandomCharacterID();
            // ���� GetRandomCharacterID()�� ���ٸ�,
            // string randomCharacterID = _availableCharacterIDs[Random.Range(0, _availableCharacterIDs.Count)];
            // �� ���� _availableCharacterIDs ����Ʈ���� �������� �����ؾ� �մϴ�.
            if (string.IsNullOrEmpty(randomCharacterID)) // null �Ǵ� �� ���ڿ� üũ
            {
                Debug.LogError("���� ĳ���� ID�� �������� �� �����߽��ϴ�. ���� ������ �ǳʍ��ϴ�. (CharacterDataContainer�� �������� �ε�Ǿ����� Ȯ���ϼ���)");
                continue; // �̹� ������ �ǳʶٰ� ���� ���ݱ��� ���
            }

            // SpawnRequest ����
            SpawnRequest request = new SpawnRequest(randomCharacterID, _teamInfo);
            // ���� ����
            CharacterUnit spawnedUnit = Spawn(request);
            if (spawnedUnit != null)
            {
                Debug.Log($"AI Spawner created a {spawnedUnit.CharacterName} for Team {_teamInfo}");
            }
        }
    }

    public void StopAutoSpawn()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
            Debug.Log("AI Spawner stopped auto-spawning.");
        }
    }



    public virtual CharacterUnit Spawn(SpawnRequest request)
    {
        GameObject prefab = CharacterDataContainer.Instance.GetPrefab(request.TargetID);
        if (prefab is null) Debug.LogError("�������� ������������ �ε�Ǿ� ���� �� �� �������ϴ�.");

        GameObject spawnedUnit = Instantiate(prefab, transform.position, Quaternion.identity);
        CharacterUnit characterUnitComponent = spawnedUnit.GetComponent<CharacterUnit>();
        if (characterUnitComponent is null)
            Debug.LogError($"{spawnedUnit.name}�� ĳ���� ���� ������Ʈ�� ã�� �� �������ϴ�.");

        characterUnitComponent.SetTeam(request.TeamInfo);
        // ���⿡ �߰������� ĳ������ ���ݷ�, ������ ���ȵ��� �����ϴ� �ڵ���� �ۼ��Ѵ�.
        // ���׷��̵� �ý����� ���� �ȸ���������� �ϴ��� �� ������ �����ߴ�.

        return characterUnitComponent;
    }
}
