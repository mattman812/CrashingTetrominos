using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrashingTetrominos
{
   static class ShapesHandler
    {
        private static Shape[] shapesArray;

        static ShapesHandler()
        {
            shapesArray = new Shape[]
                 {
                   new Shape{
                       width = 2,
                       height = 2,
                       dots = new int[,]
                       {
                           {1,1 },
                           {1,1 }
                       }
                   } ,
                   new Shape
                   {
                       width = 1,
                       height = 4,
                       dots = new int[,]
                       {
                           {1},
                           {1},
                           {1},
                           {1}
                       }
                   },
                   new Shape
                   {
                       width = 3,
                       height = 2,
                       dots = new int[,]
                       {
                           {0,0,1},
                           {1,1,1}
                       }
                   },
                  new Shape
                   {
                       width = 3,
                       height = 2,
                       dots = new int[,]
                       {
                           {0,1,0},
                           {1,1,1}
                       }
                   },
                  new Shape {
                        width = 3,
                        height = 2,
                        dots = new int[,]
                        {
                            { 1, 0, 0 },
                            { 1, 1, 1 }
                        }
                    },
                    new Shape {
                        width = 3,
                        height = 2,
                        dots = new int[,]
                        {
                            { 1, 1, 0 },
                            { 0, 1, 1 }
                        }
                    },
                    new Shape {
                        width = 3,
                        height = 2,
                        dots = new int[,]
                        {
                            { 0, 1, 1 },
                            { 1, 1, 0 }
                        }
                    }
                 };
        }
        public static Shape GetRandomShape()
        {
            var shape = shapesArray[new Random().Next(shapesArray.Length)];
            return shape;
        }
    }
}
 
