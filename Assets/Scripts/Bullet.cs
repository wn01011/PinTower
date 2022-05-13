using System.Timers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Bullet : MonoBehaviour
{
    private float moveSpeed = 15f;
    public float damage = 0f;
    private float lifeTime = 1.0f;
    
    private void Awake()
    {
        this.UpdateAsObservable()
            .TakeUntilDisable(gameObject)
            .Subscribe((x) => Move());

        Observable.Timer(System.TimeSpan.FromSeconds(lifeTime))
            .TakeUntilDisable(gameObject)
            .Subscribe(_ => gameObject.SetActive(false));
    }

    private void Move()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
