using System;
using System.IO.Ports;
using System.Threading;

class ESCControl
{
    static SerialPort serialPort;

    static void Main(string[] args)
    {
        // Set up the serial port
        serialPort = new SerialPort ("COM3", 9600); // Replace "COM3" with your COM port
        serialPort.Open();

        // Send PWM control signals
        while (true)
        {
            Console.WriteLine("Enter PWM duty cycle (0-100):");
            int pwm = int.Parse(Console.ReadLine());

            // Ensure the value is between 0 and 100
            pwm = Math.Max(0, Math.Min(100, pwm));

            // Send PWM value to STM32
            SendPwmCommand(pwm);

            // Add delay between commands
            Thread.Sleep(100);
        }
    }
    static void SendPwmCommand(int pwm)
    {
        byte[] command = new byte[] { (byte)pwm }; // Send PWM value as a single byte
        serialPort.Write(command, 0, command.Length);
    }
}