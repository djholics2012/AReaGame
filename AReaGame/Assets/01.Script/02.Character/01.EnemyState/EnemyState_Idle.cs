using UnityEngine;
using System.Collections;

public class EnemyState_Idle : State<Enemy>
{
    public override void Enter(Enemy owner)
    {
        owner.SetAnimation(ANIMATION_STATE.IDLE);
        owner.MoveStop( );
    }

    public override void Update(Enemy owner)
    {
        // 탐색범위 안에 플레이어가 있으면 쫓아간다
        owner.SearchAndTrace();

        GameObject target = owner.SearchTarget();
        if (target != null)
        {
            owner.TargetObj = target;
            owner.ChangeState(CHARACTER_STATE.TRACE);
            return;
        }


        // 일정 확률로 임의 위치로 이동
        if (Random.Range(0, 100) == 0)
        {
            // 반대로 곱하면 안된다. 왜?
            Vector3 dir = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * Vector3.forward;

            owner.TargetPos = dir * Random.Range(0f, owner.MoveRange) + owner.transform.position;
            owner.ChangeState(CHARACTER_STATE.MOVETO);
        }
    }

    public override void Exit(Enemy owner)
    {
    }
}