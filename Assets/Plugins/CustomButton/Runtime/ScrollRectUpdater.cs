using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CustomButton
{
    public class ScrollRectUpdater : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IInitializePotentialDragHandler
    {
        private ScrollRect _scrollRect;

        private ScrollRect ScrollRect
        {
            get
            {
                if(_scrollRect == null)
                    _scrollRect = GetComponentInParent<ScrollRect>();
                return _scrollRect;
            }
        }

        private void OnEnable()
        {
            _scrollRect = GetComponentInParent<ScrollRect>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (ScrollRect != null)
            {
                ScrollRect.OnDrag(eventData);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (ScrollRect != null)
            {
                ScrollRect.OnBeginDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (ScrollRect != null)
            {
                ScrollRect.OnEndDrag(eventData);
            }
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (ScrollRect != null)
            {
                ScrollRect.OnInitializePotentialDrag(eventData);
            }
        }
    }
}