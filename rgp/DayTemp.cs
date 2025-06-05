using System;

public class DayTemp : ArduinoDevice, IComparable<DayTemp>
{
	public DateTime Day;
	public int Temp;
	public DayTemp()
	{
		Day = DateTime.Now;
	}

    public int CompareTo(DayTemp other)
		=> other.Temp.CompareTo(Temp);
    
}
