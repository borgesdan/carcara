// Polygon base class by Laurent Cozic, 2006
// Code: https://www.codeproject.com/Articles/15573/2D-Polygon-Collision-Detection
// MIT License

using System;

namespace Microsoft.Xna.Framework
{
    /// <summary>
    /// Representa um forma ligada por dois ou mais vertices.
    /// </summary>
    public class CPolyForm : IEquatable<CPolyForm>
    {
        private static CPolyForm recPoly = null;

        //vertices
        private Vector2[] points = null;
        //arestas
        private Vector2[] edges = null;
        private bool needBuildEdges = false;        

        /// <summary>
        /// Obtém o centro do polígono.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                float totalX = 0;
                float totalY = 0;

                for (int i = 0; i < points.Length; i++)
                {
                    totalX += points[i].X;
                    totalY += points[i].Y;
                }

                return new Vector2(totalX / points.Length, totalY / points.Length);
            }
        }     
        
        /// <summary>
        /// Obtém True caso o polígono esteja vazio.
        /// </summary>
        public bool IsEmpty
        {
            get => points == null || points.Length == 0;
        }        

        /// <summary>
        /// Inicializa uma nova instância da classe
        /// </summary>
        /// <param name="verticesCount">Inicializa o polígono vazio mas com uma quantidade limite de vertices.</param>
        public CPolyForm(int verticesCount)
        {
            if (verticesCount < 2)
                throw new ArgumentException($"{nameof(verticesCount)} must be greater than or equals to 2.");

            points = new Vector2[verticesCount];
            edges = new Vector2[points.Length];
            //needBuildEdges = true;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe.
        /// </summary>
        /// <param name="vertices">Os vertices do polígono.</param>
        public CPolyForm(params Vector2[] vertices)
        {
            if (vertices.Length < 2)
                throw new ArgumentException($"{nameof(vertices)} must be greater than or equals to 2.");

            points = vertices;
            edges = new Vector2[points.Length];
            needBuildEdges = true;
        }

        /// <summary>
        /// Inicializa uma nova instância como cópia de outra instância.
        /// </summary>
        /// <param name="source">A instância a ser copiada.</param>
        public CPolyForm(CPolyForm source)
        {
            Array.Copy(source.points, this.points, source.points.Length);
            Array.Copy(source.edges, this.edges, source.edges.Length);
            this.needBuildEdges = source.needBuildEdges;
        }
        
        /// <summary>
        /// Obtém um ponto do array de vertices.
        /// </summary>
        public Vector2 this[int index]
        {
            get => points[index];
            set
            {
                points[index] = value;
                needBuildEdges = true;
            }
        }

        // Verifica e calcula as bordas do polígono.        
        private void BuildEdges()
        {
            Vector2 p1;
            Vector2 p2;            
            
            for (int i = 0; i < points.Length; i++)
            {
                p1 = points[i];
                if (i + 1 >= points.Length)
                {
                    p2 = points[0];
                }
                else
                {
                    p2 = points[i + 1];
                }
                //_edges.Add(p2 - p1);
                edges[i] = p2 - p1;
            }

            needBuildEdges = false;
        }

        /// <summary>
        /// Aplica o deslocamento das posições dos pontos do polígono.
        /// </summary>
        /// <param name="vector">O valor no eixo X e Y.</param>
        public void Offset(Vector2 vector)
        {
            Offset(vector.X, vector.Y);
        }

        /// <summary>
        /// Aplica o deslocamento das posições dos pontos do polígono.
        /// </summary>
        /// <param name="x">O valor no eixo X.</param>
        /// <param name="y">O valor no eixo Y.</param>
        public void Offset(float x, float y)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Vector2 p = points[i];
                points[i] = new Vector2(p.X + x, p.Y + y);
            }

            needBuildEdges = true;
        }

        /// <summary>
        /// Define os pontos do polígono.
        /// </summary>
        /// <param name="vertices">Define os pontos através de uma lista de pontos.</param>
        public void Set(params Vector2[] vertices)
        {
            if (vertices.Length < 2)
                throw new ArgumentException($"{nameof(vertices)} must be greater than or equals to 2.");

            if (points == null || (points.Length != vertices.Length))
            {
                points = new Vector2[vertices.Length];
                edges = new Vector2[points.Length];
            }                

            Array.Copy(vertices, points, points.Length);

            needBuildEdges = true;
        }

        /// <summary>
        /// Obtém uma cópia dos vertices do polígono.
        /// </summary>
        public Vector2[] GetVertices()
        {
            if (points == null)
                return new Vector2[0];

            Vector2[] _p = new Vector2[points.Length];
            Array.Copy(points, _p, points.Length);

            return _p;
        }

        /// <summary>
        /// Obtém uma cópia das arestas do polígono.
        /// </summary>
        public Vector2[] GetEdges()
        {
            if (edges == null)
                return new Vector2[0];

            if (needBuildEdges)
                BuildEdges();

            Vector2[] _e = new Vector2[edges.Length];
            Array.Copy(points, _e, points.Length);

            return _e;
        }

        public bool Intersect(CPolyForm other)
        {
            return Intersects(this, other);
        }

        public bool Intersect(Rectangle rectangle)
        {
            foreach(Vector2 p in points)
            {
                if (rectangle.Contains(p))
                    return true;
            }

            if (recPoly == null)
                recPoly = new CPolyForm(new Vector2[4]);

            rectangle.ToPolygon(recPoly);

            return Intersects(this, recPoly);
        }        

        public bool Equals(CPolyForm other)
        {
            if(other.points.Length == this.points.Length)
            {
                for(int i = 0; i < other.points.Length; i++)
                {
                    if(other.points[i] != points[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Retorna true caso o polígono A intersectou o polígono B.
        /// </summary>
        /// <param name="polygonA">O primeiro polígono.</param>
        /// <param name="polygonB">O segundo polígono.</param>     
        public static bool Intersects(CPolyForm polygonA, CPolyForm polygonB)
        {
            if (polygonA.IsEmpty || polygonB.IsEmpty)
                return false;

            if (polygonA.needBuildEdges)
                polygonA.BuildEdges();
            if (polygonB.needBuildEdges)
                polygonB.BuildEdges();

            bool intersect = true;
            bool willIntersect = true;

            int edgeCountA = polygonA.edges.Length;
            int edgeCountB = polygonB.edges.Length;
            float minIntervalDistance = float.PositiveInfinity;
            Vector2 translationAxis = new Vector2();
            Vector2 edge;

            // Loop through all the edges of both polygons
            for (int edgeIndex = 0; edgeIndex < edgeCountA + edgeCountB; edgeIndex++)
            {
                if (edgeIndex < edgeCountA)
                {
                    edge = polygonA.edges[edgeIndex];
                }
                else
                {
                    edge = polygonB.edges[edgeIndex - edgeCountA];
                }

                // ===== 1. Find if the polygons are currently intersecting =====

                // Find the axis perpendicular to the current edge
                Vector2 axis = new Vector2(-edge.Y, edge.X);
                axis.Normalize();

                // Find the projection of the polygon on the current axis
                float minA = 0; float minB = 0; float maxA = 0; float maxB = 0;
                ProjectPolygon(axis, polygonA, ref minA, ref maxA);
                ProjectPolygon(axis, polygonB, ref minB, ref maxB);

                // Check if the polygon projections are currentlty intersecting
                if (IntervalDistance(minA, maxA, minB, maxB) > 0) intersect = false;

                // ===== 2. Now find if the polygons *will* intersect =====

                // Project the velocity on the current axis
                float velocityProjection = Vector2.Dot(axis, Vector2.Zero);

                // Get the projection of polygon A during the movement
                if (velocityProjection < 0)
                {
                    minA += velocityProjection;
                }
                else
                {
                    maxA += velocityProjection;
                }

                // Do the same test as above for the new projection
                float intervalDistance = IntervalDistance(minA, maxA, minB, maxB);
                if (intervalDistance > 0) willIntersect = false;

                // If the polygons are not intersecting and won't intersect, exit the loop
                if (!intersect && !willIntersect) break;

                // Check if the current interval distance is the minimum one. If so store
                // the interval distance and the current distance.
                // This will be used to calculate the minimum translation vector
                intervalDistance = Math.Abs(intervalDistance);
                if (intervalDistance < minIntervalDistance)
                {
                    minIntervalDistance = intervalDistance;
                    translationAxis = axis;

                    Vector2 d = polygonA.Center - polygonB.Center;
                    if (Vector2.Dot(d, translationAxis) < 0) translationAxis = -translationAxis;
                }
            }

            // The minimum translation vector can be used to push the polygons appart.
            // First moves the polygons by their velocity
            // then move polygonA by MinimumTranslationVector.
            //if (willIntersect) result.Subtract = translationAxis * minIntervalDistance;

            return intersect;
        }

        // Calculate the distance between [minA, maxA] and [minB, maxB]
        // The distance will be negative if the intervals overlap
        private static float IntervalDistance(float minA, float maxA, float minB, float maxB)
        {
            if (minA < minB)
            {
                return minB - maxA;
            }
            else
            {
                return minA - maxB;
            }
        }

        // Calculate the projection of a polygon on an axis and returns it as a [min, max] interval
        private static void ProjectPolygon(Vector2 axis, CPolyForm polygon, ref float min, ref float max)
        {
            // To project a point on an axis use the dot product            
            float d = Vector2.Dot(axis, polygon.points[0]);
            min = d;
            max = d;
            for (int i = 0; i < polygon.points.Length; i++)
            {
                d = Vector2.Dot(polygon.points[i], axis);
                if (d < min)
                {
                    min = d;
                }
                else
                {
                    if (d > max)
                    {
                        max = d;
                    }
                }
            }
        }
    }    
}
