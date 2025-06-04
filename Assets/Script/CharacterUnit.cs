using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Unit;
using static UnityEngine.GraphicsBuffer;
public enum LookDirection { Left, Right }

public class CharacterUnit : GameUnit
{
    public SPUM_Prefabs _spumPref;

    public enum UnitState
    {
        idle,
        run,
        attack,
        stun,
        skil,
        death
    }

    public UnitState _unitstate = UnitState.idle;

    public Animator _animator;

    public GameUnit _target;


    public float _findTimer;
    public string CharacterName { get => _characterName; }
    public float MoveSpeed { get => _moveSpeed; }
    public float ATK { get => _atk; }
    public float NormalAttackCooldown { get => _normalAttackCooldown; }
    public float NormalAttackCastingTime { get => _noramlAttackCastingTime; }
    public float SpawnWaitingTime { get => _spawnWaitingTime; }

    public float AttackRange { get => _attackRange; }

    public float AttackTimer { get => _attackTimer; }

    //Move

    public Vector2 _tempDis;

    public Vector2 _dirVec;

    //Attack

    

    private void Update()
    {
        CheckState();
        
    }

    void CheckState()
    { 
        switch (_unitstate)
        {
            case UnitState.idle:
                FindTarget();
                break;

            case UnitState.run:
                FindTarget();
                DoMove();
                break;

            case UnitState.attack:
                CheckAttack();
                break;

            case UnitState.stun:
                break;

            case UnitState.skil:
                break;

            case UnitState.death:
                //SetDeathDone();
                break;


        }
    }

    void SetState(UnitState state)
    {
        _unitstate = state;
        switch (_unitstate)
        {
            case UnitState.idle:
                //_spumPref.PlayAnimation(PlayerState.IDLE, 0);
                break;

            case UnitState.run:
                _animator.SetBool("1_Move", true);
                //_spumPref.PlayAnimation(PlayerState.MOVE, 0);
                break;

            case UnitState.attack:
                _animator.SetTrigger("2_Attack");
                //_spumPref.PlayAnimation(PlayerState.ATTACK, 0);
                break;

            case UnitState.stun:
                break;

            case UnitState.skil:
                break;

            case UnitState.death:
                _animator.SetTrigger("4_Death");
                break;


        }
    }

    /// <summary>
    /// 기본 시선 방향
    /// </summary>
    [SerializeField]
    public LookDirection defaultLookDirection;

    protected Transform AttackTargetPos
    {
        get
        {
            if (AttackTarget is null)
                throw new System.NullReferenceException("공격 대상의 좌표를 가져 오려 했으나 현재 이 유닛은 공격대상이 비어 있습니다.");
            return AttackTarget.transform;
        }
    }
    protected Unit AttackTarget;

    private string _characterName = "Unit";
    private float _moveSpeed = 5f;
    private float _atk = 1f;
    private float _normalAttackCooldown = 1f;
    private float _noramlAttackCastingTime = 0.25f;
    private float _spawnWaitingTime = 1f;
    private float _attackRange = 1f;
    private float _attackTimer = 1f;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }


    public override void SetTeam(Team settingValue)
    {
        if (settingValue == Team.P2 && defaultLookDirection == LookDirection.Left)
        {
            _spumPref._anim.transform.localScale = new Vector3(-1, 1, 1);  
            //Debug.Log(settingValue.ToString());
        }
        else if(settingValue == Team.P1 && defaultLookDirection == LookDirection.Right)
        {
            //Debug.Log(settingValue.ToString());
            _spumPref._anim.transform.localScale = new Vector3(1, 1, 1);
        }
            base.SetTeam(settingValue);
    }

    void DoMove()
    {
        if (!CheckTarget()) return;
        CheckDistance();

        _dirVec = _tempDis.normalized;
        transform.position += (Vector3)_dirVec * _moveSpeed * Time.deltaTime;
    }

    bool CheckDistance()
    {
        _tempDis = (Vector2)(_target.transform.localPosition - transform.position);

        float tDis = _tempDis.sqrMagnitude;

        if (tDis <= _attackRange * _attackRange)
        {
            SetState(UnitState.attack);
            _animator.SetBool("1_Move", false);
            return true;
        }
        else
        {
            if (!CheckTarget()) SetState(UnitState.idle);
            else SetState(UnitState.run);

            return false;
        }
    }

    bool CheckTarget()
    {
        
        if (_target == null) return false;
        if (_target.HP <= 0) return false;
        if(_target.gameObject == null) return false;
        if (!_target.gameObject.activeInHierarchy) return false;

        SetState(UnitState.idle);

        return true;

    }

    void FindTarget()
    {
        _findTimer += Time.deltaTime;
        if (_findTimer > SoonsoonData.Instance.GAM._findTimer)
        {
            
            _target = SoonsoonData.Instance.GAM.GetTarget(this, this.Teams);
            //Debug.Log(SoonsoonData.Instance.GAM.GetTarget(this, this.Teams));
            if (_target == null)
            {
                _target = GameManager.GetEnemyCastle(this.Teams);
            }
            if (_target != null)
            {   
                SetState(UnitState.run);
            }
            else 
            {
                //Debug.Log(this.Teams);

            } 
                
            _findTimer = 0;
        }

    }

    Attack basicAttack = new Attack(20f);

    void CheckAttack()
    {
        if (!CheckTarget()) return;
        if (!CheckDistance()) return;

        _attackTimer += Time.deltaTime;
        if (_attackTimer > _normalAttackCooldown)
        {
            DoAttack();
            _attackTimer = 0;
        }
    }

    void DoAttack()
    {
        _animator.SetTrigger("2_Attack");
        _target.TakeDamage(_target,basicAttack);
        if (_target.HP <= 0)
        {
            FindTarget();
        }
        //Debug.Log(_target.HP);
    }

    public void SetDeath(Team team)
    {
        switch (team)
        {
            case Team.P1: SpawnManager.Instance.PlayerUnits.Remove(this); break;
            case Team.P2: SpawnManager.Instance.AIUnits.Remove(this); break; 
        }

        SetState(UnitState.death);
    }

    public void SetDeathDone()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(GameUnit unit, Attack attack)
    {
        float actualDemage = attack.GetDemageAmount() - Armor;
        HP = HP - actualDemage;
        if (HP <= 0)
        {
            SetDeath(this.Teams);
        }
    }


}
