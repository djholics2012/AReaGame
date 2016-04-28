using UnityEngine;
using System.Collections;

public class EnemyState_Trace : State<Enemy>
{

    public override void Enter(Enemy owner)
    {
        owner.SetAnimation(ANIMATION_STATE.RUN);
        owner.MoveToTarget( ); // 목표 위치로 이동한다.
    }

    public override void Update(Enemy owner)
    {
        owner.MoveToTarget( );
        // 타겟이 없으면 IDLE
        if (owner.TargetObj == null)
        {
            owner.ChangeState(CHARACTER_STATE.IDLE);
            return;
        }

        // 공격 범위에 들어오면 공격한다.
        float range = 1f; // 수정할 사항
        float dist = Vector3.SqrMagnitude(owner.transform.position - owner.TargetObj.transform.position);
        if (dist <= range * range)
            owner.ChangeState(CHARACTER_STATE.ATTACK);

    }

    public override void Exit(Enemy owner)
    {

    }
}
