using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(TargetDetectionBehaviour))]
public class EnemyController : MonoBehaviour
{
    [HideInInspector] public Condition patrol;
    [HideInInspector] public Condition chase;
    [HideInInspector] public Condition attack;
    [HideInInspector] public Condition die;

    public int health = 3;

    public NodeSO root;
    public NodeSO currentState;

    private TargetDetectionBehaviour _tdb;
    private AttackBehaviour _ab;

    private void OnEnable()
    {
        PlayerController.OnAttack += TakeDamage;
        PlayerController.OnRespawn += Respawn;
    }

    private void OnDisable() => PlayerController.OnAttack -= TakeDamage;
    private void OnDestroy() => PlayerController.OnRespawn -= Respawn;

    private void Awake()
    {
        patrol = new Condition(ConditionName.Patrol);
        chase = new Condition(ConditionName.Chase);
        attack = new Condition(ConditionName.Attack);
        die = new Condition(ConditionName.Dead);

        _tdb = GetComponent<TargetDetectionBehaviour>();
        _ab = GetComponent<AttackBehaviour>();

        ChangeState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(this);
        }

        if (health <= 0)
        {
            die.check = true;
        }
        else
        {
            die.check = false;
        }

        if (_ab != null && _ab.IsTargetInAttackRange(_tdb.TargetPosition, _tdb.IgnoreLayers))
        {
            attack.check = true;
        }
        else
        {
            attack.check = false;
        }

        if (_tdb.IsTargetWithinTheLineOfSight())
        {
            chase.check = true;
        }
        else
        {
            chase.check = false;
        }
    }

    private void TakeDamage()
    {
        if (health > 0)
        {
            Debug.Log("Enemy -> Damage Recieved");

            health -= 1;
        }
    }
    private void Respawn()
    {
        if (health <= 0)
        {
            health = 3;
            gameObject.SetActive(true);

            Debug.Log("Enemy -> Respawn");
        }
    }

    public void ChangeState()
    {
        StartCoroutine(WaitToTheEndOfFrame());
    }

    private IEnumerator WaitToTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        foreach (var node in root.children)
        {
            if (node.EnterCondition(this))
            {
                if (currentState != null)
                {
                    currentState.OnExit(this);
                }

                currentState = node;
                node.OnStart(this);

                break;
            }
        }
    }

}
