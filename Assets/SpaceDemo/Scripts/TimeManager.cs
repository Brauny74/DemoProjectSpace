using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceDemo
{
    public class TimeManager : Singleton<TimeManager>
    {
        [Tooltip("How long the day is in seconds")]
        public float dayLength;
        public UnityEvent OnDayEnd;
        public bool paused = false;

        private float _currentTimeInDay;

        private void Start()
        {
            OnDayEnd.Invoke();
        }

        public void DayEnd()
        {
            OnDayEnd.Invoke();
        }

        private void Update()
        {
            ProcessTime();
        }

        private void ProcessTime()
        {
            if (paused)
                return;

            _currentTimeInDay += Time.deltaTime;
            if (_currentTimeInDay >= dayLength)
            {
                _currentTimeInDay = 0;
                DayEnd();
            }

            GUIManager.Instance.SetDayImage(_currentTimeInDay / dayLength);
        }
    }
}