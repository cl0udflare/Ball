using UnityEngine;

namespace Coins
{
    public class CollectingCoins : MonoBehaviour
    {
        [SerializeField] private CoinTextUI _coinText;

        private void OnTriggerEnter(Collider other)
        {
            var coin = other.gameObject.GetComponent<Coin>();

            if (coin == null)
                return;

            _coinText.AddCoin(coin.CoinValue);

            coin.DestroyCoin();
        }
    }
}