using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCutApp.Model
{
    public class Trajectory
    {
        
        private double px;
        public double Px
        {
            get
            {
               
                return px;
            }

            set
            {
               px = Math.Round(value, 3);
            }
        }

        private double py;
        public double Py
        {
            get
            {
               

                return py;
            }

            set
            {
                py = Math.Round(value, 3);
            }
        }


        private  double rl;
        public double Rl
        {
            get
            {
              
                return rl;
            }

            set
            {
              rl= Math.Round( value,3);
            }
        }

        private double rr;
        public double Rr
        {
            get
            {
               
                return rr;
            }

            set
            {
               rr = Math.Round(value, 3);
            }
        }

       public ushort VelocityRatio { get; set; }

        public Trajectory(double x, double y, double l, double r,ushort v)
        {
            px = x;
            py =y;
            rl = l;
            rr = r;
            VelocityRatio = v;

        }

    }
}
