using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Game.Movement
{
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private int _npcID;
        [SerializeField] private float _speed;
        [SerializeField] private int _lastReachedPosition;
        [SerializeField] private bool _isWaiting;
        [SerializeField] private float _currentIdleTime;
        [SerializeField] private List<Transform> _pathPositions;
        [SerializeField] private List<float> _waitingTimesAtPathPositions;

        public ReactiveProperty<Vector2> Position { get; } = new();

        private void Start()
        {
            _speed = 5;
            _lastReachedPosition = 0;
            _isWaiting = true;
            _currentIdleTime = 0;

            if (!_pathPositions.IsNullOrEmpty()) {
                transform.position = _pathPositions[0].position;
            }
            else
            {
                transform.position = new Vector3(0,0,0);
            }
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            if (_isWaiting) {
                _currentIdleTime = _currentIdleTime + deltaTime;
                if(_currentIdleTime > _waitingTimesAtPathPositions[_lastReachedPosition]) { 
                    _isWaiting = false;
                    _currentIdleTime = 0;
                }
            }
            if (!_isWaiting){
                int nextPositionIndex = getNextPositionIndex();
                var movementDirection = _pathPositions[nextPositionIndex].position - _pathPositions[_lastReachedPosition].position;
                //normalize movement direction
                movementDirection = movementDirection / movementDirection.sqrMagnitude;
                transform.position = transform.position + deltaTime * _speed * movementDirection;
                UpdateLastReachedPosition();
            }     
        }

        private void UpdateLastReachedPosition()
        {
            int positionIndexToReachNext = getNextPositionIndex();
            Vector3 currentPositionToNextPathPosition = _pathPositions[positionIndexToReachNext].position - transform.position;
            Vector3 lastPathPositionToNextPathPosition = _pathPositions[positionIndexToReachNext].position - _pathPositions[_lastReachedPosition].position;
            if (Vector3.Dot(currentPositionToNextPathPosition, lastPathPositionToNextPathPosition) < 0)
            {
                _lastReachedPosition = getNextPositionIndex();
                _isWaiting = true;
            }
        }
        private int getNextPositionIndex()
        {
            return (_lastReachedPosition + 1) % _pathPositions.Count;
        }
    }
}
