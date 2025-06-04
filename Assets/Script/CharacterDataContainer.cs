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

    // ���� ID ������ ���� ĳ�̵� ID ����Ʈ (�߰�)
    private List<string> _cachedCharacterIDs;

    private void Awake()
    {
        if (Instance is null) Instance = this;
        else Debug.LogError("Singleton �ν��Ͻ��� �̹� �����մϴ�. �ʱ�ȭ�� �����߽��ϴ�.");

        characterPrefabs = new Dictionary<string, GameObject>();
        LoadCharacterPrefabs();

        // ĳ�̵� ������ �ε� ��, ID ����Ʈ�� ĳ�� (�߰�)
        _cachedCharacterIDs = new List<string>(characterPrefabs.Keys);
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

    public string GetRandomCharacterID()
    {
        if (_cachedCharacterIDs == null || _cachedCharacterIDs.Count == 0)
        {
            // �� ��Ȳ�� LoadCharacterPrefabs()���� ������ ���ų�, Resources ������ �������� ���ٴ� ��
            Debug.LogError("�ε�� ĳ���� �������� ��� ���� ID�� ������ �� �����ϴ�. Resources ���� �� ��θ� Ȯ���ϼ���.");
            return null; // ���� ID�� ��ȯ�� �� �����Ƿ� null ��ȯ
        }

        // ĳ�̵� ID ����Ʈ���� ������ �ε����� ����
        int randomIndex = Random.Range(0, _cachedCharacterIDs.Count);

        // �ش� �ε����� ID ��ȯ
        return _cachedCharacterIDs[randomIndex];
    }
}
