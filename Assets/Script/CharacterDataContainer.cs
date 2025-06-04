using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour
{
    //Singleton pattern�� �ν��Ͻ�
    public static CharacterDataContainer Instance;

    //�������� ���� ��ġ
    public const string CharacterPrefabPath = "SPUM/SPUM_Units/Units";
    //�������� ĳ���ϴ� �����
    public Dictionary<string, GameObject> characterPrefabs;

    private void Awake()
    {
        if (Instance is null) Instance = this;
        else Debug.LogError("Singleton �ν��Ͻ��� �̹� �����մϴ�. �ʱ�ȭ�� �����߽��ϴ�.");

        characterPrefabs = new Dictionary<string, GameObject>();
        LoadCharacterPrefabs();
    }
    // �� �Լ��� �������� �޸𸮿��� �ε��մϴ�.
    private void LoadCharacterPrefabs()
    {
        List<GameObject> prefabList = new List<GameObject>();
        prefabList.AddRange(Resources.LoadAll<GameObject>(CharacterPrefabPath));

        if (prefabList is null || prefabList.Count == 0)
            throw new System.NullReferenceException($"Prefab�� {CharacterPrefabPath}�� �����!");

        foreach (GameObject prefab in prefabList)
        {
            characterPrefabs.Add(prefab.name, prefab);
        }
    }
    // ĳ�̵� ������ ����Ҹ� �˻��Ͽ� targetID�� �ش��ϴ� ������ ������Ʈ�� ���� �ɴϴ�.
    public GameObject GetPrefab(string targetID)
    {
        if (characterPrefabs.Count == 0)
            throw new System.NullReferenceException("ĳ���� �������� �ε���� �ʾҰų� �����ϴ�.");
        else if (characterPrefabs.TryGetValue(targetID, out GameObject temp))
            return temp;
        else
            throw new System.ArgumentException($"Target Prefab ID ({targetID}) �� ������ ����Ʈ�� �������� �ʽ��ϴ�.");
    }
}
