using System;

public class ArduinoDevice : IArduinoDevice
{
    Guid index;
	public ArduinoDevice()
	{
        index = new Guid();
	}


    public Guid ShowId()
    {
        return index;
    }
}
