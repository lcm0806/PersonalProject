public interface ICharacterSpawner
{
    /// <summary>
    /// 소환 요청을 받아 캐릭터를 씬에 소환하고 소환된 캐릭터를 반환합니다.
    /// </summary>
    CharacterUnit Spawn(SpawnRequest request);
}