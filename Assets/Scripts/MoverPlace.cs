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
        Vector3 currentPlace = _places[_numberOfPlace].position;

        transform.position = Vector3.MoveTowards(transform.position, currentPlace, _speed * Time.deltaTime);
        transform.forward = currentPlace - transform.position;

        if (transform.position.IsEnoughClose(currentPlace))
        {
            _numberOfPlace++;

            if (_numberOfPlace == _places.Length)
            {
                _numberOfPlace = 0;
            }
        }
    }
}