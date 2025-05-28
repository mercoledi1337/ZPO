using System.IO.Ports;
using System;
using System.Diagnostics;

public class ProgramRGB()
{

    private SerialPort serialPort { get; set; }
    

    public void MainInterface()
    {
        serialPort = new()
        {
            PortName = "COM3",
            BaudRate = 9600
        };
        serialPort.DataReceived += SerialDataRecieved;
        serialPort.Open();
        
    }

    public void SerialDataRecieved(object sender, SerialDataReceivedEventArgs e)
    {
        //string inData = serialPort.ReadLine();
        //Console.WriteLine($"Data Received: {inData}");
    }

    public string SerialTempPublisher() => serialPort.ReadLine();
    


    public void SerialDataSender(string color)
    {
        serialPort.Write(color);
    }
}
