using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameUnit : MonoBehaviour
{
    public Team Teams => _team;
    public float HP = 100f;
    public float Armor => _armor;
    public UnityEvent DieEvent => _dieEvent;

    public float EnemyDetectionRadious { get => _enemyDetectRadius; }

    private Team _team;
    //private float _hp = 100;
    private float _armor = 0;
    private UnityEvent _dieEvent;
    private float _enemyDetectRadius = 100f;
    internal CharacterUnit.UnitState _unitState;

    private void Awake()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        if (_dieEvent is null) _dieEvent = new UnityEvent();
        _dieEvent.AddListener(OnDie);
    }

    public virtual void TakeDamage(Attack attack)
    {
        float actualDemage = attack.GetDemageAmount() - Armor;
        HP = HP - actualDemage;
        if (HP <= 0)
        {
            DieEvent.Invoke();
        }
    }

    public virtual void TakeDamage(GameUnit unit, Attack attack)
    {
        float actualDamage = attack.GetDemageAmount() - Armor;
        // 실제 데미지가 0보다 작으면 데미지를 주지 않음 (방어력이 너무 높을 때)
        if (actualDamage < 0) actualDamage = 0;

        HP -= actualDamage; // 이 부분이 정확한지 확인

        //Debug.Log($"{this.name} took {actualDamage} damage. Current HP: {HP}"); // 디버그 로그 추가

        if (HP <= 0)
        {
            HP = 0; // HP가 음수가 되는 것을 방지
            OnDie(); // CastleUnit의 OnDie가 호출될 것으로 예상
        }
    }


    protected virtual void OnDie() { }

    public virtual void SetTeam(Team settingValue) 
    {
        _team = settingValue;
    }
}
