using System;

class henkan_double
{
    static void Main()
    {
        string inputstr;
        double outnum;

        inputstr = Console.ReadLine();
        inputstr = inputstr.Replace("'","");

        outnum = Convert.ToDouble(inputstr);
        outnum = outnum / 10;

        Console.WriteLine(outnum);

    }   
}