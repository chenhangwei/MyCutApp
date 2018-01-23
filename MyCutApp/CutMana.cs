using MyCutApp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace MyCutApp
{
   


    public class CutMana
    {
        public  Grid MyGridCut = new Grid();
        public static bool Dd { get; set; } = true;
        public static string Runstr { get; set; }
        public  async Task CutAdd(Trajectory trajectory)
        {
            for (int i = 0; i <5000; i++)
            {
                    Runstr = "程序运行中";
                   
                    if (Dd)
                    {
                        await Task.Delay(1000);
                }
                else
                    {
                        return ;
                    }
                    trajectory.Px++;
                    trajectory.Py++;
                    trajectory.Rl++;
                    trajectory.Rr++;
            }
        }
        public Grid DrawCut(Trajectory trajectory)
        {
            var polygon1 = new Polygon();
            polygon1.Fill = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            var points = new PointCollection();
            points.Add(new Point(10, 200));
            points.Add(new Point(60, 140));
            points.Add(new Point(130, 140));
            points.Add(new Point(180, 200));
            polygon1.Points = points;

           
           
            var path1 = new Windows.UI.Xaml.Shapes.Path();
        
            path1.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
            path1.StrokeThickness = 1;

            var geometryGroup1 = new GeometryGroup();
          

            var pathGeometry1 = new PathGeometry();
            var pathFigureCollection1 = new PathFigureCollection();
            var pathFigure1 = new PathFigure();
          
            pathFigureCollection1.Add(pathFigure1);
            pathGeometry1.Figures = pathFigureCollection1;

            var pathSegmentCollection1 = new PathSegmentCollection();
          

            var pathSegment2 = new BezierSegment();
            pathSegment2.Point1 = new Point(125, 300);
            pathSegment2.Point2 = new Point(275, 100);
            pathSegment2.Point3 = new Point(trajectory.Px, trajectory.Px);
            pathSegmentCollection1.Add(pathSegment2);
            pathFigure1.Segments = pathSegmentCollection1;

            geometryGroup1.Children.Add(pathGeometry1);
            path1.Data = geometryGroup1;

         
            MyGridCut.Children.Add(path1);

         


            return MyGridCut;
        }

      
    }
}
