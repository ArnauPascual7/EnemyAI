using UnityEngine;

[CreateAssetMenu(fileName = "DieState", menuName = "Scriptable Objects/Die State")]
public class DieStateSO : NodeSO
{
    public override bool EnterCondition(EnemyController ec)
    {
        return ec.die.check;
    }

    public override bool ExitCondition(EnemyController ec)
    {
        return !ec.die.check;
    }

    public override void OnUpdate(EnemyController ec)
    {
        base.OnUpdate(ec);

        Debug.Log("Enemy -> Die");
    }

    public override void OnStart(EnemyController ec)
    {
        ec.gameObject.SetActive(false);
    }

    public override void OnExit(EnemyController ec)
    {
        ec.gameObject.SetActive(true);
    }
}
