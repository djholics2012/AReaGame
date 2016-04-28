using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// CharacterBase<T> 의 역할
/// CharacterBase는 Enemy, Ally(정확히는 Player's Unit)의 Script를 총괄하는 Script이다.
/// </summary>

public class CharacterBase<T> : MonoBehaviour
{
    private StateManager<T> _stateManager = null;

    private string          _name;

    private int             _hp;
    private int             _maxHp;
    private float           _moveSpeed;
    private int             _attackDamage;

    private GameObject      _targetObj; // 목표(공격, NPC, 플레이어)
    private Vector3         _targetPos; // 이동 위치


    // Property

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int Hp
    {
        get { return _hp; }
        set { _hp = Mathf.Clamp(value, 0, MaxHp); }
    }

    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            if (value <= 0)
                value = 0;
            _maxHp = value;
        }
    }

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set
        {
            if (value <= 0)
                value = 0;
            _moveSpeed = value;
            // Navigation 이용해서 그 상에서의 이동속도값을 집어넣어야한다.
        }
    }

    public int AttackDamage
    {
        get { return _attackDamage; }
        set
        {
            if (value <= 0)
                value = 0;
            _attackDamage = value;
        }
    }

    public bool IsAlive { get { return Hp > 0; } }

    public GameObject TargetObj
    {
        get { return _targetObj; }
        set { _targetObj = value; }
    }

    public Vector3 TargetPos
    {
        get { return _targetPos; }
        set { _targetPos = value; }
    }

    // Component
    private NavMeshAgent _navMeshAgentComp;
    private Animator _animatorComp;

    protected virtual void Start( )
    {
        Debug.Log("Start");
        InitComponent( );
        _stateManager = new StateManager<T>((T)(object)this);

        _targetPos = transform.position;
        InvokeRepeating("SearchTarget", 0, 5.0f);
    }

    protected virtual void Update( )
    {
        _stateManager.Update( );
        if (_targetObj != null) TargetPos = _targetObj.transform.position;
    }

    protected virtual void FixedUpdate( )
    {
        _stateManager.FixedUpdate( );
    }

    ///<summary>
    ///상태 변경 함수
    ///</summary>
    ///
    public void ChangeState(CHARACTER_STATE state)
    {
        _stateManager.ChangeState((int)state);
    }

    /// <summary>
    /// Start() 함수에 콤포넌트 입력
    /// </summary>
    /// 
    private void InitComponent( )
    {
        _navMeshAgentComp = GetComponent<NavMeshAgent>( );
        _animatorComp = GetComponent<Animator>( );
    }

    ///<summary>
    ///초깃값 입력 
    ///</summary>
    ///
    public void InitDate(string name, int hp, float moveSpeed, int attackDamage)
    {
        Name = name;
        MaxHp = _hp = hp;
        MoveSpeed = moveSpeed;
        AttackDamage = attackDamage;
    }

    /// <summary>
    /// 애니메이션 변경 함수
    /// </summary>
    /// <param name="state"></param>
    public void SetAnimation(ANIMATION_STATE state)
    {
        _animatorComp.SetInteger("AnimState", (int)state);
    }

    /// <summary>
    /// 현재 수행중인 Animation의 진행시간(1회 진행이 1f)
    /// </summary>
    /// <returns></returns>
    public float GetCurAnimationNormalizedTime( )
    {
        return _animatorComp.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    /// <summary>
    /// INPUTMODE에 따라 이동을 명령할때만 실행
    /// </summary>
    public void MoveToTarget( )
    {
        _navMeshAgentComp.enabled = true;
        _navMeshAgentComp.SetDestination(TargetPos);
        Debug.Log("x : " + TargetPos.x);
        Debug.Log("y : " + TargetPos.y);
    }

    /// <summary>
    /// 타겟 위치로 이동 완료 체크
    /// </summary>
    /// <returns></returns>
    public bool IsReachTargetPos( )
    {

        if (_navMeshAgentComp.desiredVelocity == Vector3.zero)
        {
            TargetPos = transform.position;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 이동 중지 함수
    /// </summary>
    public void MoveStop( )
    {
        TargetPos = transform.position;
    }

    /// <summary>
    ///  이동 완료 여부 확인함수
    /// </summary>
    /// <returns></returns>

    /// <summary>
    /// 타겟을 향해 회전하는 함수
    /// </summary>
    public void RotateToTarget( )
    {
        if (TargetObj == null)
            return;

        Quaternion rot = Quaternion.LookRotation(_targetObj.transform.position - transform.position);
        transform.rotation = rot;

        Vector3 rotation = transform.rotation.eulerAngles;  //현재각도
        rotation.y = rot.eulerAngles.y;                     //위에서 구한각도 Y값만 대입

        //부드럽게 회전
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * 3f);
    }

    protected void AddState(CHARACTER_STATE stateID, State<T> state)
    {
        _stateManager.AddState((int)stateID, state);
    }
}