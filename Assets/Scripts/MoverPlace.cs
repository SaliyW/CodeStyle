using UnityEngine;

public class MoverPlace : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform _parentOfPlaces;

    private Transform[] _places;
    private int _numberOfPlace = 0;

    private void Start()
    {
        SetPlaces();
    }

    private void Update()
    {
        MoveToPlace();
    }

    private void SetPlaces()
    {
        _places = new Transform[_parentOfPlaces.childCount];

        for (int i = 0; i < _parentOfPlaces.childCount; i++)
        {
            _places[i] = _parentOfPlaces.GetChild(i).GetComponent<Transform>();
        }
    }

    private void MoveToPlace()
    {
        Transform place = _places[_numberOfPlace];
        Vector3 placeDirection = (place.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(placeDirection);
        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, place.position, _speed * Time.deltaTime), Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _speed));

        if (transform.position.IsEnoughClose(place.position))
        {
            IncreaseNumberOfPlace();
        }
    }

    private void IncreaseNumberOfPlace()
    {
        _numberOfPlace++;

        if (_numberOfPlace == _places.Length)
        {
            _numberOfPlace = 0;
        }
    }
}