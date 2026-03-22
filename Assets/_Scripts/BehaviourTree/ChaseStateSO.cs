using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "Scriptable Objects/Chase State")]
public class ChaseStateSO : NodeSO
{
    public override bool EnterCondition(EnemyController ec)
    {
        return ec.chase.check;
    }

    public override bool ExitCondition(EnemyController ec)
    {
        return !ec.chase.check || ec.attack.check || ec.die.check;
    }

    public override void OnUpdate(EnemyController ec)
    {
        base.OnUpdate(ec);

        TargetDetectionBehaviour tdb = ec.GetComponent<TargetDetectionBehaviour>();
        ChaseBehaviour cb = ec.GetComponent<ChaseBehaviour>();
        if (tdb != null && cb != null)
        {
            cb.ChaseTarget(tdb.TargetPosition);
        }

        Debug.Log("Enemy -> Chase");
    }
}
