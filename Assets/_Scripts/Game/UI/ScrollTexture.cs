using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class ScrollTexture : MonoBehaviour
    {
        [SerializeField] private RawImage _img;
        [SerializeField] private float _x, _y;

        private Vector2 _uvPosition;
        private Vector2 _uvSize;

        private void Awake()
        {
            _uvPosition = _img.uvRect.position;
            _uvSize = _img.uvRect.size;
        }

        private void Update()
        {
            _uvPosition += Time.deltaTime * new Vector2(_x, _y);
            _img.uvRect = new Rect(_uvPosition, _uvSize);
        }
    }
}