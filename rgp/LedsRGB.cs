using System;

public class LEdsRGB : ArduinoDevice
{
    public int r = 0;
    public int g = 0;
    public int b = 0;
    public LEdsRGB()
	{
	}

    public void set(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public int[] get()
    {
        int[] rgb = new int[3];
        rgb[0] = r;
        rgb[1] = g;
        rgb[2] = b;
        return rgb;
    }
}
