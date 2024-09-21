using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Clock : MonoBehaviour
{
    public event Action<TimeDTO> OnClockInitialized;
    public event Action<TimeDTO> OnCurrentTimeChanged;
    public event Action<float> OnCurrentSecondsChanged;
    public event Action<float> OnCurrentMinutesChanged;
    public event Action<float> OnCurrentHoursChanged;

    [SerializeField] private float _checkTimOffsetSeconds; 

    private TimeDTO _startTime;
    private TimeDTO _currentTime;

    private TimePicker _timePicker;

    [Inject] 
    private void Initialize(TimePicker timePicker)
    {
        _timePicker = timePicker;

        InitializeClock();
    }

    private void Awake()
    {
        StartCoroutine(CheckTimePerOffset());
    }

    private async void InitializeClock()
    {
        if (_timePicker is not null)
        {
            _startTime = await _timePicker.GetTime();
            _currentTime = new TimeDTO(_startTime);

            OnClockInitialized?.Invoke(_currentTime);

            StartCoroutine(ChangeTimePerSecond());
        }
    }

    private async void CheckTime()
    {
        if (_timePicker is not null)
        {
            TimeDTO timeDTO = await _timePicker.GetTime();

            if (!_currentTime.Equals(timeDTO))
            {
                _currentTime = timeDTO;

                OnCurrentTimeChanged?.Invoke(_currentTime);
                OnClockInitialized?.Invoke(_currentTime);
            }
        }
    }

    private IEnumerator CheckTimePerOffset()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkTimOffsetSeconds);

            CheckTime();
        }
    }

    private IEnumerator ChangeTimePerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            _currentTime.seconds += 1;

            OnCurrentSecondsChanged?.Invoke(_currentTime.seconds);

            if (_currentTime.seconds >= 60)
            {
                _currentTime.seconds = 0;
                _currentTime.minute += 1;

                OnCurrentMinutesChanged?.Invoke(_currentTime.minute);
            }

            if (_currentTime.minute >= 60)
            {
                _currentTime.minute = 0;
                _currentTime.hour += 1;

                OnCurrentHoursChanged?.Invoke(_currentTime.hour);
            }

            if (_currentTime.hour >= 24)
            {
                _currentTime.hour = 0;
            }

            OnCurrentTimeChanged?.Invoke(_currentTime);
        }
    }
}
