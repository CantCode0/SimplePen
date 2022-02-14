//============================================================================
// SimplePend.cs Defines a class for simulating a simple pendulum
//============================================================================
using System;

namespace Sim
{
    public class SimplePend
    {
        private double len=1.1; // pendulum length
        private double g=9.81; // gravity
        int n=2;
        private double[] x;  //array of states
        private double[] x1;
        private double[] f;  //right side of equation
        private double[] k1;
        private double[] k2;
        private double[] k3;
        private double[] k4;
        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SimplePend()
        {
            x= new double[n]; //array with n element
            f= new double[n]; 
            k1= new double[n];
            k2= new double[n];
            k3= new double[n];
            k4= new double[n];
            x1= new double[n];

            x[0]= 1.0;      //theta , intial condition
            x[1]= 0.0;      //thetadot
        }
        //--------------------------------------------------------------------
        // step: perform one intergration step via Euler
        //--------------------------------------------------------------------
        public void step(double dt)
        {
            rhsFunc(x,f);

            int i;
            for(i=0;i<n;++i)
            {
                x[i] =x[i]+f[i] *dt;
            }

        }

        
        public void rk4(double dt)
        {
            rhsFunc(x,k1);
            x1[0]= x[0]+ 0.5*dt*k1[1];
            rhsFunc(x1,k2);
            x1[0]= x[0]+ 0.5*dt*k2[1];
            rhsFunc(x1,k3);
            x1[0]= x[0]+ dt*k3[1];
            rhsFunc(x1,k4);
            //Console.WriteLine(k1[1]+" " + k2[1]+" "+k3[1] +" "+k4[1]);
            int i;
            for(i=0;i<2;++i)
            {
                x[i] =x[i]+(1.0/6.0)*(k1[i] +2*k2[i]+2*k3[i]+k4[i])*dt;
            }

        }
        
        //--------------------------------------------------------------------
        // rhsFunc: function to calculate right hand side of pendulum
        //--------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {

            ff[0] = st[1];  //st[1] is thetadot
            ff[1] = -g/len* Math.Sin(st[0]);  //st[0] is theta, ff[1] represent the slope 

        }
        //--------------------------------------------------------------------
        // Getters and setters
        //--------------------------------------------------------------------
        public double L
        {
           get {return(len);} 

           set
           {
                if(value>0.0)
                    len=value;
           }
        }

        public double G
        {
           get {return(g);} 

           set
           {
                if(value>=0.0)
                    g=value;
           }
        }

        public double theta 
        {
            get {return x[0];}

            set {x[0] =value;}
        }

        public double thetaDot
        {
            get {return x[1];}

            set {x[1] =value;}
        }

    }

}