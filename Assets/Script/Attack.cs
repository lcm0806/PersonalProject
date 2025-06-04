public enum AttackStyle { Projectile, ShortRange } // 장거리, 근접
public enum AttackPowerType { Normal, Magical } // 일반, 마법

public struct Attack
{
    // 공격력
    public float AttackPower { get => _atkPower; }
    // 공격 방식
    public AttackStyle attackStyle { get => _atkStyle; }
    // 공격 종류
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
