using UnityEngine;

namespace CustomButton
{
    public abstract class ButtonDecoratorComponent : MonoBehaviour
    {
        public virtual void OnClick(bool interactable)
        { }

        public virtual void OnPointerUp(bool interactable)
        { }

        public virtual void OnPointerDown(bool interactable)
        { }

        public virtual void OnInteractabilityChanged(bool interactable)
        { }
    }
}