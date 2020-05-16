using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeefyGameEngine
{
    public class BeefyVertex : ICloneable
    {
        public float X;
        public float Y;

        public BeefyVertex(float targetX, float targetY)
        {
            X = targetX;
            Y = targetY;
        }

        public BeefyVertex(Vector2 data)
        {
            X = data.X;
            Y = data.Y;
        }

        public void Translate(Vector2 trans)
        {
            X += trans.X;
            Y += trans.Y;
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public object Clone()
        {
            return new BeefyVertex(X, Y);
        }

        public static BeefyVertex operator +(BeefyVertex val1, BeefyVertex val2)
        {
            return new BeefyVertex(val1.X + val2.X, val1.Y + val2.Y);
        }

        public static BeefyVertex operator -(BeefyVertex val1, BeefyVertex val2)
        {
            return new BeefyVertex(val1.X - val2.X, val1.Y - val2.Y);
        }
    }

    public class BeefyShape : ICloneable, IDisposable
    {
        protected List<BeefyVertex> vertexSet;
        protected Vector2 origin;
        protected Vector2 localY;
        protected Vector2 localX;

        public BeefyShape()
        {
            vertexSet = new List<BeefyVertex>();
            origin = Vector2.Zero;
            localY = Vector2.UnitY;
            localX = Vector2.UnitX;
        }

        public BeefyShape(Rectangle rect)
        {
            vertexSet = new List<BeefyVertex>();
            vertexSet.Add(new BeefyVertex(rect.X, rect.Y));
            vertexSet.Add(new BeefyVertex(rect.X + rect.Width, rect.Y));
            vertexSet.Add(new BeefyVertex(rect.X + rect.Width, rect.Y - rect.Height));
            vertexSet.Add(new BeefyVertex(rect.X, rect.Y - rect.Height));
            localY = Vector2.UnitY;
            localX = Vector2.UnitX;
            this.origin = new Vector2(rect.X, rect.Y);
        }

        public BeefyShape(List<BeefyVertex> targetPSet, Vector2 targetOrigin = default)
        {
            vertexSet = targetPSet;
            origin = targetOrigin;
            localY = Vector2.UnitY;
            localX = Vector2.UnitX;
        }

        public List<BeefyVertex> VertexSet
        {
            get { return vertexSet; }
        }

        public int VertexCount
        {
            get { return vertexSet.Count; }            
        }

        public Vector2 Origin
        {
            get { return origin; }
        }

        #region AddVertex Implementations
        public void AddVertex(Vector2 vec)
        {
            vertexSet.Add(new BeefyVertex(vec));
        }

        public void AddVertex(BeefyVertex targetVert)
        {
            vertexSet.Add(targetVert);
        }

        public void AddVertex(float targetX, float targetY)
        {
            vertexSet.Add(new BeefyVertex(targetX, targetY));
        }
        #endregion

        #region RemoveVertex Implementations
        public void RemoveVertex(int targetIndex)
        {
            vertexSet.RemoveAt(targetIndex);
        }

        public void RemoveVertex(float targetX, float targetY)
        {
            vertexSet.Remove(vertexSet.Find(item => (item.X == targetX) && (item.Y == targetY)));
        }

        public void RemoveVertex(BeefyVertex targetVert)
        {
            vertexSet.Remove(targetVert);
        }
        #endregion

        #region Scale
        public void Scale(Vector2 factor, bool local = true)
        {
            local = false;
            if (local)
            {                
                foreach (BeefyVertex vert in vertexSet)
                {
                    vert.Translate(-origin);
                    vert.X += localX.X * factor.X;
                    vert.X += localY.X * factor.X;
                    vert.Y += localX.Y * factor.Y;
                    vert.Y += localY.Y * factor.Y;
                    vert.Translate(origin);
                }
            }
            else //Global X and Y scaling
            {
                foreach (BeefyVertex vert in vertexSet)
                {
                    vert.Translate(-origin);
                    vert.X *= factor.X;
                    vert.Y *= factor.Y;
                    vert.Translate(origin);
                }
            }
        }

        public void Scale(float factor, bool local = true)
        {
            Scale(new Vector2(factor), local);
        }

        public void Scale(float scalingX, float scalingY, bool local = true)
        {
            Scale(new Vector2(scalingX, scalingY), local);
        }
        #endregion

        #region Rotation
        public void Rotate(double theta)
        {
            foreach (BeefyVertex vert in vertexSet)
            {
                //TODO Fix
                vert.Translate(-origin);
                float vx = vert.X;
                float vy = vert.Y;
                vert.X = (float)(vx * Math.Cos(theta) - vy * Math.Sin(theta));
                vert.Y = (float)(vy * Math.Cos(theta) + vx * Math.Sin(theta));
                vert.Translate(origin);
            }
            float x = localX.X; float y = localX.Y;
            localX.X = (float)(x * Math.Cos(theta) - y * Math.Sin(theta));
            localX.Y = (float)(y * Math.Cos(theta) + x * Math.Sin(theta));
            x = localY.X; y = localY.Y;
            localY.X = (float)(x * Math.Cos(theta) - y * Math.Sin(theta));
            localY.Y = (float)(y * Math.Cos(theta) + x * Math.Sin(theta));
        }
        #endregion

        #region Translation
        public void Translate(Vector2 translation)
        {
            foreach (BeefyVertex vert in vertexSet)
            {
                vert.X = (int)(vert.X + translation.X);
                vert.Y = (int)(vert.Y + translation.Y);
            }
            origin += translation;
        }

        public void Translate(float dX, float dY)
        {
            foreach (BeefyVertex vert in vertexSet)
            {
                vert.X = (int)(vert.X + dX);
                vert.Y = (int)(vert.Y + dY);
            }
            origin = new Vector2(origin.X + dX, origin.Y + dY);
        }
        #endregion

        public void SetOrigin(Vector2 newOrigin)
        {
            origin = newOrigin;
        }

        public void MoveTo(Vector2 targetPoint)
        {
            foreach (BeefyVertex vert in vertexSet)
            {                
                vert.X += targetPoint.X - origin.X;
                vert.Y += targetPoint.Y - origin.Y;
            }
            origin = targetPoint;
        }

        public void FindOrigin()
        {
            float averageX = 0, averageY = 0;
            foreach (BeefyVertex vert in vertexSet)
            {
                averageX += vert.X;
                averageY += vert.Y;
            }
            averageX /= vertexSet.Count;
            averageY /= vertexSet.Count;
            origin = new Vector2(averageX, averageY);
        }

        public Rectangle GetBoundingRectangle() //+X +Y
        {
            int x = (int)GetFarthestVertexInDirection(new Vector2(-1, 0)).X;
            int y = (int)GetFarthestVertexInDirection(new Vector2(0, -1)).Y;
            return new Rectangle(
                x, 
                y,
                (int)(GetFarthestVertexInDirection(new Vector2(1, 0)).X - x),
                (int)(GetFarthestVertexInDirection(new Vector2(0, 1)).Y - y)
                );
        }

        public BeefyVertex GetFarthestVertexInDirection(Vector2 vec)
        {   
            int index = 0;
            float maxDot = Vector2.Dot(VertexSet[index].ToVector2(), vec);
            for (int i = 0; i < VertexCount; i++)
            {
                float dot = Vector2.Dot(VertexSet[i].ToVector2(), vec);
                if (dot>maxDot)
                {
                    maxDot = dot;
                    index = i;
                }
            }
            return VertexSet[index];
        }

        public override string ToString()
        {
            string s = "";
            foreach (BeefyVertex vert in vertexSet)
            {
                s += "{" + vert.X + "," + vert.Y + "}" + " ";
            }
            return s;
        }

        public static BeefyShape Minkowski(BeefyShape bs1, BeefyShape bs2)
        {
            BeefyShape bs = new BeefyShape();
            foreach(BeefyVertex bv0 in bs1.VertexSet)
            {
                foreach(BeefyVertex bv1 in bs2.VertexSet)
                {
                    bs.AddVertex(bv0 - bv1);
                }
            }
            return bs;
        }

        /// <summary>
        /// This function uses the GJK Algorithm
        /// </summary>
        /// <param name="bs1">Shape 1</param>
        /// <param name="bs2">Shape 2</param>
        /// <returns></returns>
        public static bool IsIntersecting(BeefyShape bs1, BeefyShape bs2) //TODO : Concave Shape Fix
        {
            Vector2 Support(BeefyShape shape1, BeefyShape shape2, Vector2 v)
            {
                BeefyVertex bv1 = shape1.GetFarthestVertexInDirection(v);
                BeefyVertex bv2 = shape2.GetFarthestVertexInDirection(-v);
                BeefyVertex bv3 = bv1 - bv2;
                return bv3.ToVector2();
            }

            List<Vector2> simplex = new List<Vector2>();
            Vector2 d = new Vector2(1, -1);
            simplex.Add(Support(bs1, bs2, d));
            d = -d;

            bool ContainsOrigin(ref Vector2 vec)
            {
                Vector2 a = simplex.Last();
                Vector2 ao = -a;
                if (simplex.Count == 3)
                {
                    Vector2 b = simplex[1];
                    Vector2 c = simplex[0];
                    Vector2 ab = b - a;
                    Vector2 ac = c - a;
                    d = new Vector2(-ab.Y, ab.X);
                    if (Vector2.Dot(d, c) > 0)
                    {
                        d = -d;
                    }
                    if (Vector2.Dot(d, ao) > 0)
                    {
                        simplex.Remove(c);
                        return false;
                    }
                    d = new Vector2(-ac.Y, ac.X);
                    if (Vector2.Dot(d, b) > 0)
                    {
                        d = -d;
                    }
                    if (Vector2.Dot(d, ao) > 0)
                    {
                        simplex.Remove(b);
                        return false;
                    }
                    return true;
                }
                else
                {
                    Vector2 b = simplex[0];
                    Vector2 ab = b - a;
                    d = new Vector2(-ab.Y, ab.X);
                    if (Vector2.Dot(d, ao) < 0)
                    {
                        d = -d;
                    }
                }
                return false;
            }

            while (true)
            {
                simplex.Add(Support(bs1, bs2, d));
                if (Vector2.Dot(simplex.Last(), d)<= 0){
                    return false;
                }
                else
                {
                    if (ContainsOrigin(ref d))
                    {
                        return true;
                    }
                }
            }
        }

        public bool ContainsPoint(Point targetPoint)
        {
            return ContainsPoint(targetPoint.ToVector2());
        }

        public bool ContainsPoint(Vector2 targetPoint)
        {
            //This function uses the PnPoly Algorithm
            #region PnPoly
            bool contains;
            float[] vX = new float[vertexSet.Count];
            float[] vY = new float[vertexSet.Count];
            int i = 0, j = 0, len = 0;
            float maxX = 0, maxY = 0, minX = 100000, minY = 100000;
            foreach (BeefyVertex p in vertexSet)
            {
                maxX = MathHelper.Max(maxX, p.X);
                minX = MathHelper.Min(minX, p.X);
                maxY = MathHelper.Max(maxY, p.Y);
                minY = MathHelper.Min(minY, p.Y);
                vX[i] = p.X;
                vY[i] = p.Y;
                i++;
            }
            //If not in rectangle boundaries return false
            if ((targetPoint.X > maxX) || (targetPoint.X < minX) || (targetPoint.Y > maxY) || (targetPoint.Y < minY)) return false;
            len = vertexSet.Count();
            contains = false;
            for (i = 0; i < len; i++)
            {
                if (i == 0)
                {
                    j = len - 1;
                }
                else
                {
                    j = i - 1;
                }
                if (((vY[i] > targetPoint.Y) != (vY[j] > targetPoint.Y)) && (targetPoint.X < ((vX[j] - vX[i]) * (targetPoint.Y - vY[i]) / (vY[j] - vY[i]) + vX[i])))
                {
                    contains = !contains;
                }
            }
            return contains;
            #endregion
        }

        public object Clone()
        {
            List<BeefyVertex> bvList = new List<BeefyVertex>();
            foreach (BeefyVertex bv in vertexSet)
                bvList.Add((BeefyVertex)bv.Clone());
            BeefyShape bs = new BeefyShape(bvList, origin);
            bs.localX = localX;
            bs.localY = localY;
            return bs;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class BeefyRectangle : BeefyShape, ICloneable
    {
        bool RectangleConstructed;
        public Vector2 Location { get { return new Vector2(X,Y); } }
        public float X { get { if (RectangleConstructed) return vertexSet.First().X; else return 0; } set { if (RectangleConstructed) vertexSet.First().X = value; } }
        public float Y { get { if (RectangleConstructed) return vertexSet.First().Y; else return 0; } set { if (RectangleConstructed) vertexSet.First().Y = value; } }

        public float Height 
        { 
            get { if (RectangleConstructed) return vertexSet[2].Y - vertexSet.First().Y; else return 0; } 
            set { if (RectangleConstructed) vertexSet[2].Y = vertexSet.First().Y + value; } 
        }

        public float Width 
        { 
            get { if (RectangleConstructed) return vertexSet[2].X - vertexSet.First().X; else return 0; } 
            set { if (RectangleConstructed) vertexSet[2].X = vertexSet.First().X + value; } 
        }

        public BeefyRectangle() : base()
        {            
        }

        /// <summary>
        /// Defines a Beefy Rectangle using two diagonally opposite vertices
        /// </summary>
        public BeefyRectangle(Vector2 v1, Vector2 v2) : base()
        {   
            vertexSet.Add(new BeefyVertex(v1));
            vertexSet.Add(new BeefyVertex(new Vector2(v1.X, v2.Y)));
            vertexSet.Add(new BeefyVertex(v2));
            vertexSet.Add(new BeefyVertex(new Vector2(v2.X, v1.Y)));
            origin = (v1 + v2) / 2;
            RectangleConstructed = true;
        }

        public BeefyRectangle Clone()
        {
            BeefyRectangle br;
            if (RectangleConstructed)
                br = new BeefyRectangle(vertexSet.First().ToVector2(), vertexSet[2].ToVector2());
            else
                br = new BeefyRectangle();
            return br;
        }
    }

    public class GraphingTools
    {
        GraphicsDevice graphics;
        SpriteBatch canvas;
        Texture2D pixel;
        public int Thickness { get; set; }
        public Color PenColor { get; set; }

        public GraphingTools(GraphicsDevice gd, SpriteBatch batch, int brushThickness, Color brushColor)
        {
            graphics = gd;
            canvas = batch;
            Thickness = brushThickness;
            PenColor = brushColor;
            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new Color[1] { Color.White });
        }

        public void SetColor(Color target)
        {
            PenColor = target;
        }

        public void PlotPoint(int x, int y, Single depth = 0f)
        {
            canvas.Draw(pixel, new Rectangle(x - (int)(Thickness/2f), - y - (int)(Thickness/2f), Thickness, Thickness), null, PenColor, 0f, Vector2.Zero, SpriteEffects.None, depth);
        }

        public void PlotLine(Vector2 p1, Vector2 p2, Single depth = 0f)
        {
            int x0 = (int)p1.X; int x1 = (int)p2.X;
            int y0 = (int)p1.Y; int y1 = (int)p2.Y;
            float dX = p2.X - p1.X; float dY = p2.Y - p1.Y;
            if ((int)dX == 0)
            {
                if ((int)dY > 0)
                    for (int i = y0; i < y1; i++)
                        PlotPoint(x0, i, depth);         
                else
                    for (int i = y1; i < y0; i++)
                        PlotPoint(x0, i, depth);
                return;
            }
            float gradient = dY / dX;
            float dGrad = Math.Abs(gradient);
            float error = 0f;
            int xMin = Math.Min(x0, x1);
            int xMax = Math.Max(x0, x1);
            int y; int targetY;
            if (xMin == x0)
            {
                y = y0;
                targetY = y1;
            }
            else
            {
                y = y1;
                targetY = y0;
            }                
            for (int i = xMin; i <= xMax; i++)
            {
                PlotPoint(i, y, depth);
                error += dGrad;
                while (error >= 0.5)
                {                    
                    y += Math.Sign(gradient);
                    if (dGrad != 0 && y == targetY + Math.Sign(gradient)*Thickness)
                        return;
                    PlotPoint(i, y, depth);
                    error -= 1f;
                }                    
            }
        }

        public void PlotShape(BeefyShape shape, Single depth = 0f)
        {
            for (int i = 0; i < shape.VertexCount; i++)
            {
                if (i == shape.VertexCount - 1)
                    PlotLine(shape.VertexSet.First().ToVector2(), shape.VertexSet[i].ToVector2(), depth);
                else
                    PlotLine(shape.VertexSet[i].ToVector2(), shape.VertexSet[i + 1].ToVector2(), depth);
            }
        }

        public void PlotRectangle(BeefyRectangle rect, bool fill = true, Color fill_color = default, Single depth = 0f)
        {
            PlotShape(rect, depth);
            if (fill)
            {
                if (fill_color != default)
                    PenColor = fill_color;
                int width = (int)Math.Abs(rect.Width);
                int height = (int)Math.Abs(rect.Height);
                if (width < 1) width = 1;
                if (height < 1) height = 1;
                Texture2D fillTex = new Texture2D(graphics, width, height);
                Color[] cData = new Color[width * height];
                for (int i = 0; i < width; i++)
                    for (int j = 0; j < height; j++)
                        cData[i + j * width] = PenColor;
                fillTex.SetData(cData);
                Vector2 dest = new Vector2(rect.X, -rect.Y);
                if (rect.Width < 0)
                    dest = new Vector2(dest.X - width, dest.Y);
                if(rect.Height > 0)
                    dest = new Vector2(dest.X, dest.Y - height);
                canvas.Draw(fillTex, dest, Color.White);
                /*
                if (fill_color!=default)
                {
                    penColor = fill_color;
                }                
                float strokes = Math.Abs(rect.Height / thickness);
                int strokeWidth;
                if (rect.Height < 0)
                    strokeWidth = -thickness;
                else
                    strokeWidth = thickness;
                for (uint i = 1; i < strokes; i++)
                {
                    PlotLine(new Vector2(rect.X, rect.Y + strokeWidth * i), new Vector2(rect.X + rect.Width, rect.Y + strokeWidth * i));
                }*/
            }
        }
    }
}