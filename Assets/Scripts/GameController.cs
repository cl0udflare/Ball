using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float _speed = 1f;

    private Camera _camera;
    private Vector3 _startPoint;
    private Vector3 _force;
    private float _distance;
    private bool _isDragging;

    private void Awake() => _camera = Camera.main;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            OnDragStart();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            OnDragEnd();
        }

        if (_isDragging)
            OnDrag();
    }

    private void OnDragStart()
    {
        _startPoint = DetectMousePosition();

        _line.gameObject.SetActive(true);
    }

    private void OnDrag()
    {
        var endPoint = DetectMousePosition();
        
        var direction = _startPoint - endPoint + _ball.Position;

        _distance = Vector3.Distance(_startPoint, endPoint);
        
        _force = direction;

        _line.SetPosition(0, _ball.Position);
        _line.SetPosition(1, new Vector3(_force.x, _ball.Position.y, _force.z));
    }

    private void OnDragEnd()
    {
        _line.gameObject.SetActive(false);

        _ball.StartMove(_force, _distance * _speed);
    }

    private Vector3 DetectMousePosition()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out var raycastHit) ? raycastHit.point : _ball.Position;
    }
}