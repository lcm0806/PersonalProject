public interface ICharacterSpawner
{
    /// <summary>
    /// ��ȯ ��û�� �޾� ĳ���͸� ���� ��ȯ�ϰ� ��ȯ�� ĳ���͸� ��ȯ�մϴ�.
    /// </summary>
    CharacterUnit Spawn(SpawnRequest request);
}