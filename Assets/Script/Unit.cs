using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
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

    public Unit _target;

    public float _unitHP;

    public float _unitMS;

    public float _unitFR;

    public float _unitAT;

    public float _unitAS;

    public float _unitAR;

    public float _findTimer;

    public float _attackTimer;
    // Start is called before the first frame update

    //Move
    public Vector2 _tempDis;

    public Vector2 _dirVec;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();

    }

    void SetInitState()
    {
        _unitstate = UnitState.idle;

        Vector2 tVec = (Vector2)(_target.transform.localPosition - transform.position);
        _dirVec = tVec.normalized;

        transform.position += (Vector3)_dirVec * _unitMS * Time.deltaTime;
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
                //_animator.SetTrigger("2_Attack");
                //_spumPref.PlayAnimation(PlayerState.ATTACK, 0);
                break;

            case UnitState.stun:
                _spumPref.PlayAnimation(PlayerState.DEBUFF, 0);
                break;

            case UnitState.skil:
                _spumPref.PlayAnimation(PlayerState.ATTACK, 1);
                break;

            case UnitState.death:
                _spumPref.PlayAnimation(PlayerState.DEATH,0);
                break;


        }
    }

    void FindTarget()
    {
        _findTimer += Time.deltaTime;
        if (_findTimer > SoonsoonData.Instance.GAM._findTimer)
        {
            //_target = SoonsoonData.Instance.GAM.GetTarget(this);
            //if (_target != null) SetState(UnitState.run);
            //else SetState(UnitState.idle);
            //    _findTimer = 0;
        }
        
    }

    void SetDirection()
    {
        if(_dirVec.x >= 0)
        {
            _spumPref._anim.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _spumPref._anim.transform.localScale = new Vector3(1,1,1);
        }
    }

    void DoMove()
    {
        if (!CheckTarget()) return;
        CheckDistance();
        
        _dirVec = _tempDis.normalized;
        SetDirection();
        transform.position += (Vector3)_dirVec * _unitMS * Time.deltaTime;
    }

    bool CheckDistance()
    {
        _tempDis = (Vector2)(_target.transform.localPosition - transform.position);

        float tDis = _tempDis.sqrMagnitude;

        if (tDis <= _unitAR * _unitAR)
        {
            SetState(UnitState.attack);
            _animator.SetBool("1_Move", false);
            return true;
        }
        else
        {
            if(!CheckTarget()) SetState(UnitState.idle);
            else SetState(UnitState.run);

            return false;
        }
    }

    void CheckAttack()
    {
        if(!CheckTarget()) return;
        if(!CheckDistance()) return;

        _attackTimer += Time.deltaTime;
        if (_attackTimer > _unitAS)
        {
            DoAttack();
            _attackTimer = 0;
        }
    }

    void DoAttack()
    {
        _animator.SetTrigger("2_Attack");
    }

    bool CheckTarget()
    {
        bool value = true;
        if(_target == null) value = false;
        if(_target._unitstate == UnitState.death) value = false;
        if(!_target.gameObject.activeInHierarchy) value = false;

        if (!value)
        {
            SetState(UnitState.idle);
        }
        return value;

    }

}
