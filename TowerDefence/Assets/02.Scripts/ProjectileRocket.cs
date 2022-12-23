using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRocket : Projectile
{
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRange = 2.0f;
    
    protected override void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == TargetLayer ||
            (1 << other.gameObject.layer & TouchLayer) > 0)
        {
            Collider[] cols = Physics.OverlapSphere(other.transform.position, _explosionRange, TargetLayer);
            Enemy enemy;
            foreach (Collider col in cols)
            {
                enemy = col.gameObject.GetComponent<Enemy>();
                enemy.Hurt((int)((1.0f - Vector3.Distance(transform.position, col.transform.position) / _explosionRange) * Damage));
                enemy.BuffManager.ActiveBuff(new BuffBurning<Enemy>(2, 0.5f), 5.0f);
            }
            ExplosionEffect();
            ObjectPool.Instance.Return(gameObject);
        }
    }

    private void ExplosionEffect()
    {
        GameObject effect = ObjectPool.Instance.Spawn("RocketExplosionEffect", transform.position, Quaternion.LookRotation(transform.position - Target.position));
        ObjectPool.Instance.Return(effect, _explosionEffect.main.duration + _explosionEffect.main.startLifetime.constantMax);
    }
}
