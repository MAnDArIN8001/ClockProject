public partial class TimePicker
{
    private static TimePicker _timePickerInstance;

    private TimePicker() { }

    public static TimePicker GetInstance()
    {
        if (_timePickerInstance is null)
        {
            _timePickerInstance = new TimePicker();
        }

        return _timePickerInstance;
    }
}
