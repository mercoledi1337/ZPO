using System;

public class ArduinoDevice : IArduinoDevice
{
    public Guid index;
	public ArduinoDevice()
	{
        index = new Guid();
	}


    public Guid ShowId()
    {
        return index;
    }
}
