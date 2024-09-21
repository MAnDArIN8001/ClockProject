using UnityEngine;
using Zenject;
using TMPro;

public class ClockUIController : MonoBehaviour 
{
    private Clock _clock;

    [SerializeField] private TMP_Text _timeText;

    [Inject]
    private void Initialize(Clock clock)
    {
        _clock = clock;
    }

    private void OnEnable()
    {
        if (_clock is not null)
        {
            _clock.OnClockInitialized += HandleTimeChnaged;
            _clock.OnCurrentTimeChanged += HandleTimeChnaged;
        }
    }

    private void OnDisable()
    {
        if (_clock is not null)
        {
            _clock.OnClockInitialized -= HandleTimeChnaged;
            _clock.OnCurrentTimeChanged -= HandleTimeChnaged;
        }
    }

    private void HandleTimeChnaged(TimeDTO timeDTO)
    {
        string time = $"{timeDTO.hour}:{timeDTO.minute}:{timeDTO.seconds}";

        _timeText.text = time;
    }
}
