using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shotPause;

    private readonly int _poolDefaultCapacity = 10;
    private readonly int _poolMaxSize = 20;
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
        createFunc: () => Instantiate(_bulletPrefab, transform.position, Quaternion.identity),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.SetActive(false),
        actionOnDestroy: (obj) => Destroy(obj),
        collectionCheck: true,
        defaultCapacity: _poolDefaultCapacity,
        maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(Shoot(_shotPause));
    }

    private void ActionOnGet(GameObject obj)
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;

        obj.GetComponent<Rigidbody>().velocity = direction * _bulletSpeed;
        obj.SetActive(true);
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