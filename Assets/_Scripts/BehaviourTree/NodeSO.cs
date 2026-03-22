using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Node", menuName = "Scriptable Objects/Node")]
public class NodeSO : ScriptableObject
{
    public List<NodeSO> children;

    public virtual bool EnterCondition(EnemyController ec) { return true; }
    public virtual bool ExitCondition(EnemyController ec) { return true; }
    public virtual void OnStart(EnemyController ec) { }
    public virtual void OnExit(EnemyController ec) { }

    public virtual void OnUpdate(EnemyController ec)
    {
        if (EnterCondition(ec))
        {
            ec.ChangeState();
        }
    }
}
