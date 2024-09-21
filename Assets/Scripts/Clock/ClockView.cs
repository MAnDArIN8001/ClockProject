using UnityEngine;

[RequireComponent(typeof(Clock))]
public class ClockView : MonoBehaviour
{
    [SerializeField] private int _secondsTicksCount;
    [SerializeField] private int _minutesTicksCount;
    [SerializeField] private int _hoursTicksCount;

    [SerializeField] private float _fullCircleRotation;

    [SerializeField] private Transform _secondsArrow;
    [SerializeField] private Transform _minutesArrow;
    [SerializeField] private Transform _hoursArrow;

    private Clock _clock;

    private void Awake()
    {
        _clock = GetComponent<Clock>();
    }

    private void OnEnable()
    {
        if (_clock is not null)
        {
            _clock.OnClockInitialized += HandleClockInitialization;
            _clock.OnCurrentSecondsChanged += HandleSecondsChanged;
            _clock.OnCurrentMinutesChanged += HandleMinutesChanged;
            _clock.OnCurrentHoursChanged += HandleHoursChanged;
        }
    }

    private void OnDisable()
    {
        if (_clock is not null)
        {
            _clock.OnClockInitialized -= HandleClockInitialization;
            _clock.OnCurrentSecondsChanged -= HandleSecondsChanged;
            _clock.OnCurrentMinutesChanged -= HandleMinutesChanged;
            _clock.OnCurrentHoursChanged -= HandleHoursChanged;
        }
    }

    private void HandleClockInitialization(TimeDTO timeDTO)
    {
        int hourTick = timeDTO.hour <= _hoursTicksCount ? timeDTO.hour : timeDTO.hour - _hoursTicksCount;

        float initSecondsArrowRotationZ = -(_fullCircleRotation / _secondsTicksCount) * timeDTO.seconds;
        float initMinuteArrowRotationZ = -(_fullCircleRotation / _minutesTicksCount) * timeDTO.minute;
        float initHourArrowRotationZ = -(_fullCircleRotation / _hoursTicksCount) * hourTick;

        _secondsArrow.eulerAngles = new Vector3(0, 0, initSecondsArrowRotationZ);
        _minutesArrow.eulerAngles = new Vector3(0, 0, initMinuteArrowRotationZ);
        _hoursArrow.eulerAngles = new Vector3(0, 0, initHourArrowRotationZ);
    } 

    private void HandleSecondsChanged(float value)
    {
        float newZRotationValue = _secondsArrow.eulerAngles.z - _fullCircleRotation / _secondsTicksCount;

        _secondsArrow.eulerAngles = new Vector3(0, 0, newZRotationValue);
    }

    private void HandleMinutesChanged(float value)
    {
        float newZRotationValue = _minutesArrow.eulerAngles.z - _fullCircleRotation / _minutesTicksCount;

        _minutesArrow.eulerAngles = new Vector3(0, 0, newZRotationValue);
    }

    private void HandleHoursChanged(float value)
    {
        float newZRotationValue = _hoursArrow.eulerAngles.z - _fullCircleRotation / _hoursTicksCount;

        _hoursArrow.eulerAngles = new Vector3(0, 0, newZRotationValue);
    }
}
