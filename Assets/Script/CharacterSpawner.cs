using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour , ICharacterSpawner
{
    public Team TeamInfo { get => _teamInfo; }
    [SerializeField] private Team _teamInfo = Team.None;

    [Header("AI Spawner Settings")]
    [SerializeField] private float _spawnInterval = 5f; // 유닛 생성 간격 (5초)
    private Coroutine _spawnRoutine; // 코루틴 참조 변수

    private void Start()
    {
        if (_teamInfo == Team.P1)
        {
            StartAutoSpawn();
        }
    }

    // 자동 생성 시작 함수
    public void StartAutoSpawn()
    {
        if (_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine); // 이미 코루틴이 실행 중이면 중지
        }
        _spawnRoutine = StartCoroutine(AutoSpawnRoutine());
    }

    private IEnumerator AutoSpawnRoutine()
    {
        while (true) // 무한 반복하여 지속적으로 생성
        {
            yield return new WaitForSeconds(_spawnInterval); // 지정된 간격만큼 대기

            // ----------------------------------------------------
            // 랜덤 유닛 ID 선택 로직 (가정)
            // CharacterDataContainer에 랜덤 ID를 가져오는 함수가 있다고 가정합니다.
            // 없으면, 이곳에서 직접 List<string>을 만들어 랜덤으로 선택하는 로직이 필요합니다.
            // ----------------------------------------------------
            string randomCharacterID = CharacterDataContainer.Instance.GetRandomCharacterID();
            // 만약 GetRandomCharacterID()가 없다면,
            // string randomCharacterID = _availableCharacterIDs[Random.Range(0, _availableCharacterIDs.Count)];
            // 와 같이 _availableCharacterIDs 리스트에서 랜덤으로 선택해야 합니다.
            if (string.IsNullOrEmpty(randomCharacterID)) // null 또는 빈 문자열 체크
            {
                Debug.LogError("랜덤 캐릭터 ID를 가져오는 데 실패했습니다. 유닛 생성을 건너뜝니다. (CharacterDataContainer에 프리팹이 로드되었는지 확인하세요)");
                continue; // 이번 루프는 건너뛰고 다음 간격까지 대기
            }

            // SpawnRequest 생성
            SpawnRequest request = new SpawnRequest(randomCharacterID, _teamInfo);
            // 유닛 생성
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
