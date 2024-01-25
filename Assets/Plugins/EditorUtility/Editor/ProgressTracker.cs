using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Packages.EditorUtility
{
    public class ProgressTracker
    {
        private readonly List<int> _progressSteps = new List<int>();
        
        public T TrackProgress<T>(T value) where T : class
        {
            _progressSteps.Add(value == default(T) ? 0 : 1);
            return value;
        }
        public bool TrackProgress(bool completed)
        {
            _progressSteps.Add(completed ? 1 : 0);
            return completed;
        }

        internal ProgressTracker() { }

        internal float CalculateProgress()
        {
            return _progressSteps.Any() ? _progressSteps.Sum() / (float) _progressSteps.Count : 0f;
        }
    }
}