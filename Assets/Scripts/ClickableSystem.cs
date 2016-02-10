using UnityEngine;
using System.Collections;

public class ClickableSystem : MonoBehaviour {

  public SolarSystem system;
  public GameLogic logic;

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

      // Zoom trigger
      logic.ZoomOnSystem(system, transform);
    }
  }
}
