using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Scriptable Objects/Attack State")]
public class AttackStateSO : NodeSO
{
    public override bool EnterCondition(EnemyController ec)
    {
        return ec.attack.check;
    }

    public override bool ExitCondition(EnemyController ec)
    {
        return !ec.attack.check || ec.die.check;
    }

    public override void OnUpdate(EnemyController ec)
    {
        base.OnUpdate(ec);

        AttackBehaviour ab = ec.GetComponent<AttackBehaviour>();
        if (ab != null)
        {
            ab.Attack();
        }

        Debug.Log("Enemy -> Attack");
    }

    public override void OnExit(EnemyController ec)
    {
        AttackBehaviour ab = ec.GetComponent<AttackBehaviour>();
        if (ab != null)
        {
            ab.StopAttack();
        }
    }
}
