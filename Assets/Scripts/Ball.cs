using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Material _ballMaterial;
    [SerializeField] private Transform _sphereChild;
    [SerializeField] private ParticleSystem _hitParticle;

    private Vector3 _endPoint;
    private Vector3 _direction;
    private float _distance;

    public Vector3 Position => transform.position;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>() == null)
            return;

        var point = collision.contacts[0].point;
        var normal = collision.contacts[0].normal;
        var reflect = Vector3.Reflect(transform.forward, normal);

        StartHitParticle(normal.normalized);
        
        StartCoroutine(nameof(StretchAndChangeColor));

        Rebound(point + reflect);
    }

    private void Rebound(Vector3 point) => _direction = (point - Position).normalized;

    private void StartHitParticle(Vector3 rotate)
    {
        var hit = Instantiate(_hitParticle, Position, Quaternion.LookRotation(rotate));
        
        hit.Play();
    }
    
    private IEnumerator Movement()
    {
        _direction = (_endPoint - Position).normalized;

        var length = (Position - _endPoint).magnitude;
        var i = _distance;
        float time = 1;

        while (0 < i)
        {
            var step = 0.01f / length;

            i = Mathf.Lerp(0, _distance, time);
            time -= step;

            _sphereChild.rotation *= Quaternion.Euler(Vector3.right * i);

            transform.rotation = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
            transform.Translate(Vector3.forward * i * Time.deltaTime);

            yield return null;
        }
    }

    private IEnumerator StretchAndChangeColor()
    {
        var newScale = new Vector3(.05f, 0, -.03f);

        _ballMaterial.color = new Color(1f, .6f, .6f);

        for (var i = 0; i < 6; i++)
        {
            var scale = i < 3 ? newScale : -newScale;

            _sphereChild.localScale += scale;
            yield return new WaitForSeconds(.02f);
        }

        _ballMaterial.color = Color.white;
    }

    public void StartMove(Vector3 point, float distance)
    {
        StopCoroutine(nameof(Movement));
        _endPoint = point;
        _distance = distance;
        StartCoroutine(nameof(Movement));
    }
}