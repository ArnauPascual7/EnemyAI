using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private Material _attackMaterial;

    private Material _initialMaterial;

    private void Awake()
    {
        _initialMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Attack()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr != null)
        {
            mr.material = _attackMaterial;
        }
    }

    public void StopAttack()
    {
        GetComponent<MeshRenderer>().material = _initialMaterial;
    }

    public bool IsTargetInAttackRange(Vector3 targetPosition, LayerMask _ignoreLayers)
        => DistanceUtils.HasLineOfSight(transform.position, targetPosition, _attackRange, _ignoreLayers);
}
