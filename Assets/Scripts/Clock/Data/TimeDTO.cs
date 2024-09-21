using System;

[Serializable]
public class TimeDTO
{
    public bool dstActive;

    public int year;
    public int milliSeconds;

    public short month;
    public short day;
    public short hour;
    public short minute;
    public short seconds;

    public string dateTime;
    public string date;
    public string time;
    public string timeZone;
    public string dayOfWeek;

    public TimeDTO() { }

    public TimeDTO(TimeDTO time)
    {
        this.dstActive = time.dstActive;
        this.year = time.year;
        this.milliSeconds = time.milliSeconds;
        this.month = time.month;
        this.day = time.day;
        this.hour = time.hour;
        this.minute = time.minute;
        this.seconds = time.seconds;
        this.dateTime = time.dateTime;
        this.date = time.date;
        this.time = time.time;
        this.timeZone = time.timeZone;
        this.dayOfWeek = time.dayOfWeek;
    }

    public bool Equals(TimeDTO time) => 
        this.dstActive == time.dstActive 
        && this.year == time.year
        && this.milliSeconds == time.milliSeconds
        && this.month == time.month
        && this.day == time.day
        && this.hour == time.hour
        && this.minute == time.minute
        && this.seconds == time.seconds
        && this.dateTime == time.dateTime
        && this.date == time.date
        && this.time == time.time
        && this.timeZone == time.timeZone
        && this.dayOfWeek == time.dayOfWeek;
}
