public struct SpawnRequest
{
    // ��ȯ�� ����� ID�Դϴ�.
    public string TargetID { get => _targetID; }
    // ��ȯ�� ����� �Ҽ� ���Դϴ�.
    public Team TeamInfo { get => _team; }
    private string _targetID;
    private Team _team;

    public SpawnRequest(string spawnTargetID, Team spawnTargetTeam)
    {
        _targetID = spawnTargetID;
        _team = spawnTargetTeam;
    }
}
