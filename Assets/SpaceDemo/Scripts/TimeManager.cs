using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SpaceDemo
{
    public class TimeManager : Singleton<TimeManager>
    {
        [Tooltip("How long the day is in seconds")]
        public float dayLength;
        public UnityEvent OnDayEnd;
        public bool paused = false;

        private float _currentTimeInDay;
        private GUIManager _guiManager;

        #region Zenject
        [Inject]
        public void Constructor(GUIManager guiManager)
        {
            _guiManager = guiManager;
        }
        #endregion

        #region MonoBehavior
        private void Start()
        {
            OnDayEnd.Invoke();
        }

        private void Update()
        {
            ProcessTime();
        }
        #endregion

        #region TimeManager

        public void DayEnd()
        {
            OnDayEnd.Invoke();
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

            _guiManager.SetDayImage(_currentTimeInDay / dayLength);
        }
        #endregion
    }
}