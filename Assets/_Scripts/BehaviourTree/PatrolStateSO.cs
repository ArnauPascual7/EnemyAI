using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Scriptable Objects/Patrol State")]
public class PatrolStateSO : NodeSO
{
    public override bool ExitCondition(EnemyController ec)
    {
        return ec.chase.check || ec.attack.check || ec.die.check;
    }

    public override void OnUpdate(EnemyController ec)
    {
        base.OnUpdate(ec);

        PatrolBehaviour pb = ec.GetComponent<PatrolBehaviour>();
        if (pb != null)
        {
            pb.Patrol();
        }

        Debug.Log("Enemy -> Patrol");
    }
}
