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
    }


    protected virtual void OnDie() { }

    public virtual void SetTeam(Team settingValue) 
    {
        _team = settingValue;
    }
}
