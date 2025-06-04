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
        if (prefab is null) Debug.LogError("프리팹이 비정상적으로 로드되어 가져 올 수 없었습니다.");

        GameObject spawnedUnit = Instantiate(prefab, transform.position, Quaternion.identity);
        CharacterUnit characterUnitComponent = spawnedUnit.GetComponent<CharacterUnit>();
        if (characterUnitComponent is null)
            Debug.LogError($"{spawnedUnit.name}의 캐릭터 유닛 컴포넌트를 찾을 수 없었습니다.");

        characterUnitComponent.SetTeam(request.TeamInfo);
        // 여기에 추가적으로 캐릭터의 공격력, 방어력의 스탯등을 적용하는 코드들을 작성한다.
        // 업그레이드 시스템을 아직 안만들었음으로 일단은 팀 정보만 적용했다.

        return characterUnitComponent;
    }
}
