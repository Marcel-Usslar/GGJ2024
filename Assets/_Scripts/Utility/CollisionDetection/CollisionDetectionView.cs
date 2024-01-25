using System.Collections.Generic;
using Game.Utility;
using UnityEngine;

namespace Utility.CollisionDetection
{
    public class CollisionDetectionView : MonoBehaviour
    {
        private readonly List<ICollisionUpdateModel> _models = new();

        private bool _isSetUp;

        public ICollisionDetectionModel<T> Setup<T>() where T : Component
        {
            var model = new CollisionDetectionModel<T>();
            _models.Add(model);
            _isSetUp = true;

            return model;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerStay(other.gameObject));
        }

        private void OnCollisionStay(Collision other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerStay(other.gameObject));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerEnter(other.gameObject));
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerEnter(other.gameObject));
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerExit(other.gameObject));
        }

        private void OnCollisionExit(Collision other)
        {
            if (!_isSetUp)
                return;

            _models.ForEach(model => model.TryTriggerExit(other.gameObject));
        }
    }
}