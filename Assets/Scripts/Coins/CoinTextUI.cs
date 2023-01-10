using System.Collections;
using TMPro;
using UnityEngine;

namespace Coins
{
    public class CoinTextUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private int _coinCount;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = _coinCount.ToString();
        }

        private IEnumerator AnimationAddCoin(int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                _coinCount++;
                _text.text = _coinCount.ToString();
                ChangeScale(new Vector3(1.2f, 1.3f, 1));
                yield return new WaitForSeconds(.02f);
                
                ChangeScale(Vector3.one);
                yield return new WaitForSeconds(.02f);
            }
        }

        private void ChangeScale(Vector3 scale) => transform.localScale = scale;
        
        public void AddCoin(int amount) => StartCoroutine(AnimationAddCoin(amount));
    }
}