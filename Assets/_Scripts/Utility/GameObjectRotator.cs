using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Utility
{
    public class GameObjectRotator : MonoBehaviour
    {
        [Button("Rotate All Children Randomly (90 degrees snap)")]
        private void RotateAllChildren()
        {
            var children = transform.GetComponentsInChildren<Transform>().Skip(1);

            foreach (var child in children)
            {
                child.rotation = Quaternion.Euler(0, 90 * Random.Range(0, 4), 0);
            }
        }
    }
}