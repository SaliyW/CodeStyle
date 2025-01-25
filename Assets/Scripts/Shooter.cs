using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shotPause;

    private readonly int _poolDefaultCapacity = 10;
    private readonly int _poolMaxSize = 20;
    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
        createFunc: () => Instantiate(_bulletPrefab, transform.position, Quaternion.identity),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolDefaultCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(Shoot(_shotPause));
    }

    private void ActionOnGet(Bullet obj)
    {
        Vector3 direction = (_target.position - transform.position).normalized;

        obj.SetVelocity(direction * _bulletSpeed);
        obj.gameObject.SetActive(true);
    }

    private IEnumerator Shoot(float delay)
    {
        WaitForSecondsRealtime wait = new(delay);

        while (true)
        {
            _pool.Get();

            yield return wait;
        }
    }
}