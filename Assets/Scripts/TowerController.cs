using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private Transform bullet = null;
    
    public void ShootTheBall(float _damage)
    {
        Transform curBulletTr = Instantiate(bullet.gameObject, transform.position, Quaternion.identity).transform;
        Bullet curBullet = null;
        curBulletTr.TryGetComponent<Bullet>(out curBullet);
        curBullet.damage = _damage;
    }
}
