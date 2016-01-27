using UnityEngine;
using System.Collections;

public class Planet
{
  public float distance; // AU
  public int planetType;

  public static string[] s_planetTypes = new string[] { "Lava", "Rocky", "Habitable", "Ocean", "Ice Giant", "Gas Giant" };

  private float[] m_typeDistances = new float[] { 0.75f, 1.25f, 25.0f, 50.0f };

  public Planet(float distance, int starType)
  {
    this.distance = distance;

    GeneratePlanetType(starType);
  }

  // Test Function
  public string DebugPlanet()
  {
    return ("Distance from sun: " + string.Format("{0:F1}", distance) + "AU, Type of planet: " + s_planetTypes[planetType]);
  }

  private void GeneratePlanetType(int starType)
  {
    float rand = Random.value;

    if (SolarSystem.s_starTypes[starType] == "Yellow Dwarf")
    {
      if (distance < m_typeDistances[0])
      {
        if (rand < 0.5f)
        {
          planetType = 0;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[1])
      {
        if (rand < 0.5f)
        {
          planetType = 2;
        }
        else if (rand < 0.9f)
        {
          planetType = 3;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[2])
      {
        if (rand < 0.5f)
        {
          planetType = 4;
        }
        else
        {
          planetType = 5;
        }
      }
      else if (distance < m_typeDistances[3])
      {
        planetType = 1;
      }
    }
    else if (SolarSystem.s_starTypes[starType] == "White Dwarf")
    {
      // Could be hot, idk?
      if (distance < m_typeDistances[0])
      {
        if (rand < 0.7f)
        {
          planetType = 0;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[1])
      {
          planetType = 0;
      }
      else if (distance < m_typeDistances[2])
      {
        if (rand < 0.2f)
        {
          planetType = 4;
        }
        else
        {
          planetType = 5;
        }
      }
      else if (distance < m_typeDistances[3])
      {
        planetType = 1;
      }
    }
    else if (SolarSystem.s_starTypes[starType] == "Red Giant")
    {
      // same heat as yellow
      if (distance < m_typeDistances[0])
      {
        if (rand < 0.8f)
        {
          planetType = 0;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[1])
      {
        if (rand < 0.3f)
        {
          planetType = 2;
        }
        else if (rand < 0.6f)
        {
          planetType = 3;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[2])
      {
        if (rand < 0.3f)
        {
          planetType = 4;
        }
        else
        {
          planetType = 5;
        }
      }
      else if (distance < m_typeDistances[3])
      {
        planetType = 1;
      }
    }
    else if (SolarSystem.s_starTypes[starType] == "Blue Giant")
    {
      // 2x as hot as yellow
      if (distance < m_typeDistances[0]/2)
      {
        if (rand < 0.5f)
        {
          planetType = 0;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[1]/2)
      {
        if (rand < 0.2f)
        {
          planetType = 2;
        }
        else
        {
          planetType = 1;
        }
      }
      else if (distance < m_typeDistances[2])
      {
        if (rand < 0.1f)
        {
          planetType = 4;
        }
        else
        {
          planetType = 5;
        }
      }
      else if (distance < m_typeDistances[3])
      {
        planetType = 1;
      }
    }
  }
}
