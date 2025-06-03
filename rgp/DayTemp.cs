using System;

public class DayTemp : ArduinoDevice
{
	public string Day;
	public DateTime[] Hours = new DateTime[24];
	public int Temp = 0;
	public DayTemp()
	{
	}
}
