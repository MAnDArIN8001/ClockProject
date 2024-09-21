using UnityEngine;
using Zenject;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private bool _ignoreYAxis;
    [SerializeField] private bool _reverseAxis;

    [SerializeField, Range(0, 10)] private float _sensetivity;

    [SerializeField] private Transform _rotationPivot;

    private MainInput _input;

    [Inject]
    private void Initialize(MainInput input)
    {
        _input = input;
    }

    private void OnEnable()
    {
        if (_input is not null)
        {
            _input.Enable();
        }
    }

    private void OnDisable()
    {
        if (_input is not null)
        {
            _input.Disable();
        }
    }

    private void Update()
    {
        Vector2 mouseDelta = ReadMouseDeltaValues();

        mouseDelta *= _reverseAxis ? 1 : -1;
        mouseDelta.y = _ignoreYAxis ? 0 : mouseDelta.y;

        if (mouseDelta != Vector2.zero)
        {
            Vector3 rotationAxis = ConverInputDeltaToRotationAxis(mouseDelta);

            Rotate(rotationAxis);
        }
    }

    private void Rotate(Vector3 rotationDirection)
    {
        if (_rotationPivot is null)
        {
            return;
        }

        transform.RotateAround(_rotationPivot.position, rotationDirection, rotationDirection.magnitude * _sensetivity);
    }

    private Vector3 ConverInputDeltaToRotationAxis(Vector3 inputValue)
    {
        Vector3 rotationAxis = new Vector3(0, inputValue.x, inputValue.y);

        return rotationAxis.normalized;
    }

    private Vector2 ReadMouseDeltaValues() => _input.Controls.Delta.ReadValue<Vector2>();
}
