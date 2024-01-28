using System.Collections.Generic;
using Game.GameState;
using Game.Input;
using UnityEngine;
using Utility;

namespace Game.Movement
{
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] private int _npcID;
        [SerializeField] private float _speed;
        [SerializeField] private Vector2 _currentPosition;
        [SerializeField] private int _lastReachedPosition;
        [SerializeField] private List<Vector2> _polygonPoints;

        public ReactiveProperty<Vector2> Position { get; } = new();

        public NPCMovement(int NPCID, List<Vector2> polygonPoints)
        {
            _npcID = NPCID;
            _polygonPoints = polygonPoints;
            _lastReachedPosition = 0;
            if (!_polygonPoints.IsNullOrEmpty()) { 
                _currentPosition = polygonPoints[0];
            }
            else
            {
                Debug.LogError("_polygonPoints is empty. Spawn NPC at (0,0)!");
                _currentPosition = new Vector2(0,0);
            }
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            var movementDirection = _polygonPoints[_lastReachedPosition + 1] - _polygonPoints[_lastReachedPosition];
            //normalize movement direction
            movementDirection = movementDirection / movementDirection.sqrMagnitude;
            _currentPosition = _currentPosition + deltaTime * _speed * movementDirection;
            UpdateLastReachedPosition();
        }

        private void UpdateLastReachedPosition()
        {
            //check if (currentPosition - pathPoint[currentIndex+1]) * (pathPoint[currentIndex]-pathPoint[currentIndex+1]) > 0 (dh in dieselbe Richtung zeigen)
            int positionIndexToReach = (_lastReachedPosition + 1) % _polygonPoints.Count;
            Vector2 currentPositionToNextPathPosition = _polygonPoints[positionIndexToReach] - _currentPosition;
            Vector2 lastPathPositionToNextPathPosition = _polygonPoints[positionIndexToReach] - _polygonPoints[_lastReachedPosition];
            
            if (Vector2.Dot(currentPositionToNextPathPosition,lastPathPositionToNextPathPosition) > 0)
            {
                _lastReachedPosition += 1;
            }
        }
    }
}
