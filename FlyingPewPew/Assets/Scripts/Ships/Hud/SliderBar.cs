using UnityEngine;
using UnityEngine.UI;

namespace Hud
{
    public class SliderBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private int _maxValue;
        private int _currentValue;

        public void Init(int maxValue)
        {
            _maxValue = maxValue;
            _currentValue = maxValue;
            _slider.maxValue = maxValue;
            _slider.value = _currentValue;
        }

        public void SetProgress(int value)
        {
            _currentValue = Mathf.Clamp(_currentValue - value, 0, _maxValue);
            _slider.value = _currentValue;
        }

        public void SetValue(int newValue)
        {
            _currentValue = newValue;
            _slider.value = _currentValue;
        }
    }
}