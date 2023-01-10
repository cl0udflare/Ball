using UnityEngine;

namespace Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _destroyCoinParticle;
        [SerializeField] private int _coinValue = 50;
        [SerializeField] private float _speedRotate = 40f;

        public int CoinValue => _coinValue;
    
        private void Update() => transform.Rotate(Vector3.up * (_speedRotate * Time.deltaTime));

        public void DestroyCoin()
        {
            var coin = Instantiate(_destroyCoinParticle, transform.position, Quaternion.identity);
        
            coin.Play();
            
            Destroy(gameObject);
        }
    }
}
