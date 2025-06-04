public struct SpawnRequest
{
    // 소환할 대상의 ID입니다.
    public string TargetID { get => _targetID; }
    // 소환할 대상의 소속 팀입니다.
    public Team TeamInfo { get => _team; }
    private string _targetID;
    private Team _team;

    public SpawnRequest(string spawnTargetID, Team spawnTargetTeam)
    {
        _targetID = spawnTargetID;
        _team = spawnTargetTeam;
    }
}
