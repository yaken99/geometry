using System;

class Program
{

    static void Main()
    {
        double inp1,inp2;
        inp1 = double.Parse(Console.ReadLine());
        inp2 = double.Parse(Console.ReadLine());
        
        Console.WriteLine(sixty2ten(inp1));
        Console.WriteLine(sixty2ten(inp2));
    }

//60進数から10進数に変更する関数
    static double sixty2ten(double x)
    {
        double d,m,s;
        
        //度
        d = x / 10000;
        d = Math.Truncate(d);
 
        //分のところを10進数に変換
        m = x / 100; 
        m = Math.Truncate(m);
        m = m - (d * 100);
        
        //秒算出のための準備
        s = x * 10;
        s = Math.Truncate(s);
        s = s - (d * 100000);
        s = s - (m * 1000);
        s = s / 10;        

        //分及び秒を算出→全体の算出
        if((d<360) && (m<60) && (s<60))
        {
            m = Math.Round(m / 60,7);
            s = Math.Round(s / (60*60), 8);
            d = d + m + s;
            return Math.Round(d,6);
        }else
        {
            return 1;
        }
        
    }
}