using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    //Singleton pattern용 인스턴스
    public static CharacterDataContainer Instance;

    //프리팹의 저장 위치
    public const string CharacterPrefabPath = "SPUM/SPUM_Units/Units";
    //프리팹을 캐싱하는 저장소
    public Dictionary<string, GameObject> characterPrefabs;

    private void Awake()
    {
        if (Instance is null) Instance = this;
        else Debug.LogError("Singleton 인스턴스가 이미 존재합니다. 초기화에 실패했습니다.");

        characterPrefabs = new Dictionary<string, GameObject>();
        LoadCharacterPrefabs();
    }
    // 이 함수로 프리팹을 메모리에서 로드합니다.
    private void LoadCharacterPrefabs()
    {
        List<GameObject> prefabList = new List<GameObject>();
        prefabList.AddRange(Resources.LoadAll<GameObject>(CharacterPrefabPath));

        if (prefabList is null || prefabList.Count == 0)
            throw new System.NullReferenceException($"Prefab이 {CharacterPrefabPath}에 없어요!");

        foreach (GameObject prefab in prefabList)
        {
            characterPrefabs.Add(prefab.name, prefab);
        }
    }
    // 캐싱된 프리팹 저장소를 검색하여 targetID에 해당하는 프리팹 오브젝트를 가져 옵니다.
    public GameObject GetPrefab(string targetID)
    {
        if (characterPrefabs.Count == 0)
            throw new System.NullReferenceException("캐릭터 프리팹이 로드되지 않았거나 없습니다.");
        else if (characterPrefabs.TryGetValue(targetID, out GameObject temp))
            return temp;
        else
            throw new System.ArgumentException($"Target Prefab ID ({targetID}) 가 데이터 리스트에 존재하지 않습니다.");
    }
}
