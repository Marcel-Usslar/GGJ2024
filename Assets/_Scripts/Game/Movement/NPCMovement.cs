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
        [SerializeField] private List<Transform> _polygonPoints;

        public ReactiveProperty<Vector2> Position { get; } = new();

        private void Start()
        {
            _lastReachedPosition = 0;
            _speed = 1;
            if (!_polygonPoints.IsNullOrEmpty()) {
                transform.position = _polygonPoints[0].position;
                Debug.LogError("Start at position with index 0 at (" + _polygonPoints[0].position[0] + ";" + _polygonPoints[0].position[1] + ")" +
                    " running towards position with index 1 at (" + _polygonPoints[1].position[0] + ";" + _polygonPoints[1].position[1] +").");
            }
            else
            {
                Debug.LogError("_polygonPoints is empty. Spawn NPC at (0;0;0)!");
                transform.position = new Vector3(0,0,0);
            }
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            int nextPositionIndex = getNextPositionIndex();
            var movementDirection = _polygonPoints[nextPositionIndex].position - _polygonPoints[_lastReachedPosition].position;
            //var movementDirection = _polygonPoints[(_lastReachedPosition + 1) % _polygonPoints.Count].position -
            //                        _polygonPoints[_lastReachedPosition].position;
            //normalize movement direction
            movementDirection = movementDirection / movementDirection.sqrMagnitude;
            transform.position = transform.position + deltaTime * _speed * movementDirection;
            Debug.LogError("Current position of NPC " + _npcID + " is: (" + transform.position[0] + "," + transform.position[1] + ").");
            UpdateLastReachedPosition();
        }

        private void UpdateLastReachedPosition()
        {
            //check if (currentPosition - pathPoint[currentIndex+1]) * (pathPoint[currentIndex]-pathPoint[currentIndex+1]) > 0 (dh in dieselbe Richtung zeigen)
            int positionIndexToReachNext = getNextPositionIndex();
            //int positionIndexToReachNext = (_lastReachedPosition + 1) % _polygonPoints.Count;
            Vector3 currentPositionToNextPathPosition = _polygonPoints[positionIndexToReachNext].position - transform.position;
            Vector3 lastPathPositionToNextPathPosition = _polygonPoints[positionIndexToReachNext].position - _polygonPoints[_lastReachedPosition].position;
            Debug.LogError("Control variable: " + Vector3.Dot(currentPositionToNextPathPosition, lastPathPositionToNextPathPosition));
            if (Vector3.Dot(currentPositionToNextPathPosition, lastPathPositionToNextPathPosition) < 0)
            {
                _lastReachedPosition = getNextPositionIndex();
                positionIndexToReachNext = getNextPositionIndex();
                //positionIndexToReachNext = (_lastReachedPosition + 1) % _polygonPoints.Count;
                Debug.LogError("Last position with index " + _lastReachedPosition + " with coordiantes (" +
                    _polygonPoints[_lastReachedPosition].position[0] + ";" +
                    _polygonPoints[_lastReachedPosition].position[1] + ") was reached. " +
                    "The new position with index " + positionIndexToReachNext + " to reach is (" +
                    _polygonPoints[positionIndexToReachNext].position[0] + ";" +
                    _polygonPoints[positionIndexToReachNext].position[1] + ").");
            }
        }
        private int getNextPositionIndex()
        {
            return (_lastReachedPosition + 1) % _polygonPoints.Count;
        }
    }
}
