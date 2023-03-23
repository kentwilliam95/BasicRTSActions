using Core;
using UnityEngine;

public class DamageableItem : Item, IDamage
{
    protected float health;
    public BaseInteractableDataSO data;
    [SerializeField] protected PoolSpawnID _getHitParticle;
    [SerializeField] protected PoolSpawnID _deadParticle;

    public Component Component => this;
    protected virtual void Start()
    {
        health = data.health;
    }
    
    public virtual void Damage(float value)
    {
        if(IsDead())
            return;

        health -= value;
        health = Mathf.Clamp(health, 0, 100);
        
        var go = PoolSpawn.Instance.Spawn(_getHitParticle);
        go.transform.position = transform.position;
        
        if (IsDead())
        {
            data.SpawnInGameMaterial(transform.position);
            PoolSpawn.Instance.UnSpawn(gameObject);
            
            var deadParticle = PoolSpawn.Instance.Spawn(_deadParticle);
            deadParticle.transform.position = transform.position;
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }
}