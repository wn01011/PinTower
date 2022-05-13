using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class BallController : MonoBehaviour
{
    private Rigidbody2D rigid = null;
    private int count = 0;
    private float bouncePower = 4f;
    [SerializeField]
    private TowerController owner = null;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        this.OnDisableAsObservable()
            .Select(_ => owner)
            .Subscribe(x => x.ShootTheBall(count));

        this.UpdateAsObservable()
            .Select(_ => transform.localPosition.y)
            .TakeUntilDisable(gameObject)
            .Subscribe(x => { if (x <= -5.5f) gameObject.SetActive(false); });
            

        this.OnCollisionEnter2DAsObservable()
            .Select(_ => _)
            .Subscribe(x => OnCollisionFunc(x));

    }

    private void OnCollisionFunc(Collision2D _collision)
    {
        Vector2 norm = _collision.contacts[0].normal;
        if (norm == Vector2.up)
            norm = new Vector2(Random.Range(-0.01f, 0.01f), 1f);
        rigid.AddForceAtPosition(norm * bouncePower, _collision.contacts[0].point, ForceMode2D.Impulse);
        ++count;
    }

}
