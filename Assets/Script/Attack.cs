public enum AttackStyle { Projectile, ShortRange } // ��Ÿ�, ����
public enum AttackPowerType { Normal, Magical } // �Ϲ�, ����

public struct Attack
{
    // ���ݷ�
    public float AttackPower { get => _atkPower; }
    // ���� ���
    public AttackStyle attackStyle { get => _atkStyle; }
    // ���� ����
    public AttackPowerType attackPowerType { get => _atkPowerType; }

    private float _atkPower;
    private AttackStyle _atkStyle;
    private AttackPowerType _atkPowerType;

    public Attack(float attackPower)
    {
        _atkPower = attackPower;
        _atkStyle = AttackStyle.ShortRange;
        _atkPowerType = AttackPowerType.Normal;
    }
    public Attack(float attackPower, AttackStyle attackStyle)
    {
        _atkPower = attackPower;
        _atkStyle = attackStyle;
        _atkPowerType = AttackPowerType.Normal;
    }
    public Attack(float attackPower, AttackStyle attackStyle, AttackPowerType powerType)
    {
        _atkPower = attackPower;
        _atkStyle = attackStyle;
        _atkPowerType = powerType;
    }

    public float GetDemageAmount()
    {
        return AttackPower;
    }
}
