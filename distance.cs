using System;

class distance
{
    static void Main()
    {
        double ilat1,ilng1,ilat2,ilng2;
        ilat1 = double.Parse(Console.ReadLine());
        ilng1 = double.Parse(Console.ReadLine());
        ilat2 = double.Parse(Console.ReadLine());
        ilng2 = double.Parse(Console.ReadLine());

        Console.WriteLine(dist_hubeny(ilat1,ilng1,ilat2,ilng2));
    }

    static double dist_hubeny(double lat1,double lng1,double lat2,double lng2)
    {
        double WGS84_A = 6378137.000;
        double WGS84_E2 = 0.0066943799901975;
        double WGS84_MNUM = 6335439.327292462;
        double P,W,W3,M,N,dx,dy;
        double dist;

        P = ((lat1+lat2)/2) * Math.PI / 180;
        W = Math.Sqrt(1-WGS84_E2*Math.Pow(Math.Sin(P),2));
        W3 = W * W * W;
        M = WGS84_MNUM/W3;
        N = WGS84_A/W;
        dx = (lng1-lng2) * Math.PI / 180;
        dy = (lat1-lat2) * Math.PI / 180;

        dist = Math.Sqrt(Math.Pow(dy*M,2) + Math.Pow(dx * N * Math.Cos(P),2)); 
        dist = Math.Round(dist,3);
        return dist;
    }    
}