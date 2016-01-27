using UnityEngine;
using System.Collections;

public class ClickableSystem : MonoBehaviour {

  public SolarSystem system;

	void OnMouseOver()
  {
    if (Input.GetMouseButtonDown(0))
    {
      // Debug
      Debug.Log(system.DebugSystem());
      foreach (Planet p in system.GetPlanets())
      {
        Debug.Log(p.DebugPlanet());
      }
    }
  }
}
