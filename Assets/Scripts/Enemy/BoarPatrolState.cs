using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override void LogicUpdate()
    {
        if (!_enemy.physicsCheck.isGrounded || (_enemy.physicsCheck.touchLeftWall && _enemy.faceDirection.x < 0) ||
            (_enemy.physicsCheck.touchRigntWall && _enemy.faceDirection.x > 0))
        {
            _enemy.waiting = true;
            _enemy.anim.SetBool("isWalk", false);
        }
        else
        {
            _enemy.anim.SetBool("isWalk", true);
        }
        // Todo: 发现敌人后切换到追击状态
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        _enemy.anim.SetBool("isWalk", false);
    }
}