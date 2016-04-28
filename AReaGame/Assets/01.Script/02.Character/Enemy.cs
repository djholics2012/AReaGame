using UnityEngine;
using System.Collections;

public class Enemy : CharacterBase<Enemy>
{
    private float _searchRange = 5f;
    private float _moveRange = 3f;


    // 연결 오브젝트
    // public GameObject HpBar;
    public float MoveRange { get { return _moveRange; } }

    protected override void Start( )
    {
        base.Start( );

        // HP바 생성
        //GameObject hpbar = Instantiate(HpBar);

        GameObject canvas = GameObject.Find("Canvas");

        // 임시 능력치
        

        // 스킬 등록

        // 상태
        AddState(CHARACTER_STATE.IDLE, new EnemyState_Idle( ));
        AddState(CHARACTER_STATE.TRACE, new EnemyState_Trace( ));
        AddState(CHARACTER_STATE.MOVETO, new EnemyState_MoveTo( ));
        AddState(CHARACTER_STATE.ATTACK, new EnemyState_Attack( ));
        ChangeState(CHARACTER_STATE.IDLE);
    }

    protected override void Update( )
    {
        base.Update( );
    }

    protected override void FixedUpdate( )
    {
        base.FixedUpdate( );
    }

    public void ReceiveDamage(int damage)
    {
        Hp -= damage;
        Debug.Log(Hp);

        if (Hp <= 0)
            Destroy(gameObject);
    }

    void OnDestroy( )
    {

    }

    public GameObject SearchTarget( )
    {
        float dist;

        GameObject[] taggedTesters = GameObject.FindGameObjectsWithTag("Player");
        float closestDistSqr = Mathf.Infinity;
        Transform closestEnemy = null;
        GameObject closestEnemyObj = null;
        foreach (GameObject taggedTester in taggedTesters)
        {
            Vector3 objectPos = taggedTester.transform.position;
            dist = (objectPos - transform.position).sqrMagnitude;
            if (dist < 100000.0f)
            {
                if (dist < closestDistSqr)
                {
                    closestDistSqr = dist;
                    closestEnemy = taggedTester.transform;
                    closestEnemyObj = taggedTester;
                }
            }
        }

        Debug.Log(closestEnemyObj);
        return closestEnemyObj;
    }

    public void SearchAndTrace( )
    {
        GameObject playerObj = SearchTarget();

        if (playerObj != null)
        {
            TargetObj = playerObj;
            ChangeState(CHARACTER_STATE.TRACE);
        }
    }
}
