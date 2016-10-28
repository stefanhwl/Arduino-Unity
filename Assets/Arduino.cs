using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Arduino : MonoBehaviour {
	private SerialPort sp = new SerialPort("COM5", 9600);
    private char splitchar = ',';
    private Transform cube;
    private float[] lastRot = { 0, 0, 0 };

	void Start () {
		sp.Open ();
		sp.ReadTimeout = 1;

        cube = GameObject.Find("Cube").transform;
	}

	void Update () 
	{
		try{
            string[] line = sp.ReadLine().Split(splitchar);
            
            if (line.Length == 2 || line[0] != "" || line[1] != "")
            {                
                float x = float.Parse(line[0]);
                float y = float.Parse(line[1]);

                print("<b>" + x + " | " + y + "</b>");

                cube.Rotate(x - lastRot[0], y - lastRot[1], 0f, Space.Self);
                lastRot[0] = x;
                lastRot[1] = y;
                sp.BaseStream.Flush();
            }

		}
		catch(System.Exception){
		}
	}
}
/* 
 * 
 * The code that i run on the arduino
int count = 0;
int joyPin1 = 0;
int joyPin2 = 1;

int x = 0;
int y = 0;

void setup()
{
	Serial.begin(9600);
}

int treatValue(int data)
{
	return map(data, 0, 1023, 0, 360);
}
void loop()
{
	x = analogRead(joyPin1);
	y = analogRead(joyPin2);

	Serial.print(treatValue(x));
	Serial.print(",");
	Serial.println(treatValue(y));
}
*/
