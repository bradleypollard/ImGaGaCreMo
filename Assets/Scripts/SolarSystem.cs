using UnityEngine;
using System.Collections.Generic;

public class SolarSystem {

    public int x;
    public int y;
    public List<SolarSystem> neighbours;

    private int m_numPlanets;

    public SolarSystem( int _numPlanets )
    {
        m_numPlanets = _numPlanets;
        neighbours = new List<SolarSystem>();
    }

}
