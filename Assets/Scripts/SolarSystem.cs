using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SolarSystem
{

  public int x;
  public int y;
  public List<SolarSystem> neighbours;
  public int starType;

  public static string[] s_starTypes = { "White Dwarf", "Red Giant", "Yellow Dwarf", "Blue Giant" };

  private int m_numPlanets;
  private Planet[] m_planets;

  public SolarSystem()
  {
    neighbours = new List<SolarSystem>();

    starType = Random.Range(0, s_starTypes.Length);
    m_numPlanets = Random.Range(3, 8);
    m_planets = new Planet[m_numPlanets];

    GeneratePlanets();
  }

  // Test Function
  public string DebugSystem()
  {
    return ("Number of planets: " + m_numPlanets + ", Type of star: " + s_starTypes[starType]);
  }

  public Planet[] GetPlanets()
  {
    return m_planets;
  }

  public void GeneratePlanets()
  {
    // Start by generating distance of each planet from sun
    float[] distances = new float[m_numPlanets];
    for (int i = 0; i < m_numPlanets; ++i)
    {
      float rand = Random.value;

      if (rand < 0.2f)
      {
        distances[i] = Random.Range(0.1f, 0.75f);
      }
      else if (rand < 0.3f)
      {
        distances[i] = Random.Range(0.75f, 1.25f);
      }
      else if (rand < 0.9f)
      {
        distances[i] = Random.Range(4f, 25f);
      }
      else
      {
        distances[i] = Random.Range(35.0f, 50.0f);
      }
    }

    System.Array.Sort(distances);

    for (int i = 0; i < m_numPlanets; ++i)
    {
      m_planets[i] = new Planet(distances[i], starType);
    }
  }

}
