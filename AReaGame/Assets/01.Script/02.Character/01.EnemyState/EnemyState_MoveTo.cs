using UnityEngine;
using System.Collections;

public class EnemyState_MoveTo : State<Enemy>
{
    public override void Enter(Enemy owner)
    {
        owner.SetAnimation(ANIMATION_STATE.RUN);
        owner.MoveToTarget( ); // 목표 위치로 이동한다.
    }

    public override void Update(Enemy owner)
    {
        owner.MoveToTarget( );

        // 탐색범위 안에 플레이어가 있으면 쫓아간다
        //owner.SearchAndTrace();

        GameObject target = owner.SearchTarget();
        if (target != null)
        {
            owner.TargetObj = target;
            owner.ChangeState(CHARACTER_STATE.TRACE);
            return;
        }

        if (owner.IsReachTargetPos( ))
        {
            Debug.Log(owner.TargetPos);
            owner.ChangeState(CHARACTER_STATE.IDLE);
            return;
        }
    }

    public override void Exit(Enemy owner)
    {

    }
}
