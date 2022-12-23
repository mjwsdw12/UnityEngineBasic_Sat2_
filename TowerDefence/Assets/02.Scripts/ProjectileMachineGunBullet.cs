using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMachineGunBullet : Projectile
{
    [SerializeField] private ParticleSystem _explosionEffect;

    protected override void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == TargetLayer)
        {
            other.gameObject.GetComponent<Enemy>().Hurt(Damage);
            ExplosionEffect();
            ObjectPool.Instance.Return(gameObject);
        }
        else if ((1 << other.gameObject.layer & TouchLayer) > 0)
        {
            ExplosionEffect();
            ObjectPool.Instance.Return(gameObject);
        }
    }

    private void ExplosionEffect()
    {
        GameObject effect = ObjectPool.Instance.Spawn("BulletExplosionEffect", transform.position, Quaternion.LookRotation(transform.position - Target.position));
        ObjectPool.Instance.Return(effect, _explosionEffect.main.duration + _explosionEffect.main.startLifetime.constantMax);
    }
}
