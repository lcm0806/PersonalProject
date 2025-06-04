using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour , ICharacterSpawner
{
    public Team TeamInfo { get => _teamInfo; }
    [SerializeField] private Team _teamInfo = Team.None;

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
