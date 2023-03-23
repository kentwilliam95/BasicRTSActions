using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core
{
    public class ThumbstickController : MonoBehaviour
    {
        private Vector2 _startPosition;
        private Vector2 _initPosition;
        private Vector2 _direction;
        private float _power;

        private bool _isDrag;
        [SerializeField] private RectTransform _overlay;
        [SerializeField] private Image _container;
        [SerializeField] private Image _center;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _radius;

        public UnityAction Thumbstick_OnDrag;
        public UnityAction Thumbstick_OnPointerEnter;
        public UnityAction Thumbstick_OnPointerExit;
        public Vector3 Direction { get { return _direction; } }
        public float Power { get { return _power; } }
        public Vector2 Velocity { get { return _direction * _power; } }

        private void Update() 
        {
            if(_isDrag)
                Thumbstick_OnDrag?.Invoke();
        }

        public void OnPointerEnter(BaseEventData evt)
        {
            var mp = (PointerEventData)evt;
            var ml = _overlay.InverseTransformPoint(mp.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_overlay, mp.position, _canvas.worldCamera, out Vector2 newPos);
            _initPosition = newPos;
            _container.rectTransform.localPosition = newPos;
            _center.rectTransform.localPosition = newPos;
            
            _power = 0f;
            _direction = Vector2.zero;
            Thumbstick_OnPointerEnter?.Invoke();
        }

        public void OnDrag(BaseEventData evt)
        {
            _isDrag = true;
            var mp = (PointerEventData)evt;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_overlay, mp.position, _canvas.worldCamera, out Vector2 newPos);

            var rect = _overlay.rect;
            if (!rect.Contains(newPos))
                return;

            var diff = (_initPosition - newPos);
            var distance = diff.magnitude;
            _direction = diff.normalized;

            _center.rectTransform.localPosition = newPos;
            if (distance >= _radius)
            {
                _initPosition = newPos + _direction * _radius;
                _container.rectTransform.localPosition = _initPosition;
            }

            _power = distance / _radius;
        }

        public void OnPointerExit(BaseEventData evt)
        {
            var mp = (PointerEventData)evt;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_overlay, mp.position, _canvas.worldCamera, out Vector2 newPos);
            if (!_overlay.rect.Contains(newPos))
            {
                _container.rectTransform.localPosition = Vector2.zero;
                _center.rectTransform.localPosition = Vector2.zero;
            }
            _isDrag = false;
            _container.rectTransform.localPosition = newPos;
            _center.rectTransform.localPosition = newPos;

            _power = 0f;
            _direction = Vector2.zero;
            Thumbstick_OnPointerExit?.Invoke();
        }
    }
}
