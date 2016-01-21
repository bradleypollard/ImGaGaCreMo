using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{

  public Galaxy galaxy;

  public GameObject solarSystemParent;
  public GameObject pathParent;
  public GameObject solarSystem;
  public GameObject path;

  public Material[] starMaterials = new Material[SolarSystem.s_starTypes.Length];

  private bool mouseDown = false;

  // Use this for initialization
  void Start()
  {
    galaxy = new Galaxy();
    Debug.Log(galaxy.DebugGalaxy());

    SolarSystem[] systems = galaxy.GetSystems();
    foreach (SolarSystem s in systems)
    {
      Debug.Log(s.DebugSystem());
      Vector3 pos = new Vector3(s.x, s.y, 0f);

      GameObject system = (GameObject)Instantiate(solarSystem, pos, Quaternion.identity);
      system.transform.SetParent(solarSystemParent.transform);
      Material[] mats = system.GetComponent<Renderer>().materials;
      mats[0] = starMaterials[s.starType];
      system.GetComponent<Renderer>().materials = mats;

      foreach (SolarSystem neighbour in s.neighbours)
      {
        GameObject line = (GameObject)Instantiate(path, pos, Quaternion.identity);
        line.transform.SetParent(pathParent.transform);
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.SetPositions(new Vector3[]{ Vector3.zero, new Vector3(neighbour.x, neighbour.y, 0f) - pos});
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(2))
    {
      mouseDown = true;
      Cursor.visible = false;
      Cursor.lockState = CursorLockMode.Locked;

    }
    else if (Input.GetMouseButtonUp(2))
    {
      mouseDown = false;
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }
    
    if (mouseDown)
    {
      Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * 0.1f;
    }
  }
}
