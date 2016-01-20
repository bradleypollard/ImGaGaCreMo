using UnityEngine;
using System.Collections.Generic;

public class Galaxy {

    public int numSystems = 25;
    public int gridSize = 10;

    public int maxDist = 5;
    public float pathProb = 0.314f;

    private SolarSystem[] m_systems;

    public Galaxy()
    {

        // Init all solar systems
        m_systems = new SolarSystem[numSystems];
        for (int i = 0; i < numSystems; ++i)
        {
            m_systems[i] = new SolarSystem(Mathf.RoundToInt(Random.Range(0, 1) * 7) + 3);
        }

        // Find system coordinates
        SplatSystems(gridSize);
        // Generate lists of neighbours
        FindNeighbours();

    }

    // Test function
    public string DebugGalaxy()
    {
        return "Size: " + m_systems.Length + ", Coords of first system: " + m_systems[0].x + "," + m_systems[0].y;
    }

    public SolarSystem[] GetSystems()
    {
        return m_systems;
    }

    // Sets the x and y coord of each system
    private void SplatSystems( int _size )
    {
        int[] xs = new int[_size];
        int[] ys = new int[_size];

        foreach (SolarSystem s in m_systems)
        {
            int x = 0;
            int y = 0;
            do
            {
                x = _size - Mathf.RoundToInt(Random.value * 2 * _size);
                y = _size - Mathf.RoundToInt(Random.value * 2 * _size);
            } while (!CoordChecker(xs, ys, x, y));

            s.x = x;
            s.y = y;
        }
    }

    // Helper for SplatSystems
    private bool CoordChecker( int[] xs, int [] ys, int x, int y)
    {
        for ( int i = 0; i < xs.Length; ++i)
        {
            if ( xs[i] == x && ys[i] == y )
            {
                return false;
            }
        }
        return true;
    }

    // Performs Prim's algorithm to construct an MST
    private void FindNeighbours()
    {
        List<int> connectedNodes = new List<int>();
        connectedNodes.Add(0);
        for (int i = 0; i < m_systems.Length; ++i)
        {
            SolarSystem sys = null;
            int nearestIndex = -1;
            float nearestDistanceSq = float.MaxValue;

            // Find nearest neighbour that is not already connected to one of the connected nodes
            for (int j = 0; j < m_systems.Length; ++j)
            {
                foreach (int nodeIndex in connectedNodes)
                {
                    SolarSystem node = m_systems[nodeIndex];
                    SolarSystem s = m_systems[j];
                    float distanceSq = Mathf.Pow((node.x - s.x), 2) + Mathf.Pow((node.y - s.y), 2);
                    if (!connectedNodes.Contains(j) && distanceSq < nearestDistanceSq)
                    {
                        nearestIndex = j;
                        nearestDistanceSq = distanceSq;
                        sys = node;
                    }
                }
                
            }

            // Set our first neighbour to be our nearest
            if (nearestIndex != -1)
            {
                SolarSystem s = m_systems[nearestIndex];
                sys.neighbours.Add(s);
                s.neighbours.Add(sys);
                connectedNodes.Add(nearestIndex);
            }
        }


        // Add some nearby connections for clustering
        for (int i = 0; i < m_systems.Length; ++i)
        {
            SolarSystem sys = m_systems[i];
            for (int j = 0; j < m_systems.Length; ++j)
            {
                SolarSystem s = m_systems[j];

                if (Random.value < (pathProb / 2f) && !sys.neighbours.Contains(s) && !s.neighbours.Contains(s) && Mathf.Abs(sys.x - s.x) <= maxDist && Mathf.Abs(sys.y - s.y) <= maxDist)
                {
                    sys.neighbours.Add(s);
                    s.neighbours.Add(sys);
                }
            }
        }
        
    }
}
