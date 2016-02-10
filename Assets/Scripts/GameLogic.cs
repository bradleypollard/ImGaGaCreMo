using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
  public Galaxy galaxy; // The main galaxy

  public GameObject solarSystemParent; // Parenting object to group systems
  public GameObject pathParent; // Parenting object to group paths

  public float zoomScale = 15f;

  // Prefabs and materials
  public GameObject solarSystem;
  public GameObject path;
  public Material[] starMaterials = new Material[SolarSystem.s_starTypes.Length];
  public Material transparentMaterial, pathMaterial;


  // Private Mmembers
  private SolarSystem zoomedSystem = null;
  private Transform zoomedSystemTransform = null;
  private float zoomInterpolation = 0;
  private Vector3 cameraInitialPos = Vector3.zero;

  private bool middleMouseDown = false;

  // Use this for initialization
  void Start()
  {
    galaxy = new Galaxy();
    Debug.Log(galaxy.DebugGalaxy());

    SolarSystem[] systems = galaxy.GetSystems();
    foreach (SolarSystem s in systems)
    {
      Vector3 pos = new Vector3(s.x, s.y, 0f);

      // Create solar system object
      GameObject system = (GameObject)Instantiate(solarSystem, pos, Quaternion.identity);
      system.transform.SetParent(solarSystemParent.transform);
      Material[] mats = system.GetComponent<Renderer>().materials;
      mats[0] = starMaterials[s.starType];
      system.GetComponent<Renderer>().materials = mats;
      ClickableSystem c = system.GetComponent<ClickableSystem>();
      c.logic = this;
      c.system = s;

      foreach (SolarSystem neighbour in s.neighbours)
      {
        // Create path object
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
    // Panning
    if (zoomedSystem == null)
    {
      if (Input.GetMouseButtonDown(2))
      {
        middleMouseDown = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      }
      else if (Input.GetMouseButtonUp(2))
      {
        middleMouseDown = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
      }
    
      // Middle click hold to pan
      if (middleMouseDown)
      {
        Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f) * 0.1f;
      }
    }

    if (zoomedSystem != null)
    {
      if (Input.GetMouseButtonDown(1))
      {
        // Right click to zoom out
        zoomInterpolation = -0.01f;
      }

      if (zoomInterpolation > 0 && zoomInterpolation < 1)
      {
        // Zoom in
        InterpolateZoomIn(Time.deltaTime);
      }
      if (zoomInterpolation < 0 && zoomInterpolation > -1)
      {
        // Zoom out
        InterpolateZoomOut(-Time.deltaTime);
      }
      
      if (zoomInterpolation > 1)
      {
        zoomInterpolation = 0;
      }
      if (zoomInterpolation < -1)
      {
        zoomedSystem = null;
        zoomedSystemTransform = null;
        zoomInterpolation = 0;
        solarSystemParent.transform.localScale = Vector3.one;
        pathParent.transform.localScale = Vector3.one;
        cameraInitialPos = Vector3.zero;
      }
    }
  }

  private void InterpolateZoomIn(float timeStep)
  {
    solarSystemParent.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * zoomScale, zoomInterpolation);
    pathParent.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * zoomScale, zoomInterpolation);
    Camera.main.transform.position = Vector3.Lerp(cameraInitialPos, new Vector3(zoomedSystemTransform.position.x, zoomedSystemTransform.position.y, -10), zoomInterpolation);

    foreach (Transform t in solarSystemParent.transform)
    {
      t.localScale = Vector3.Lerp(Vector3.one * 0.7f, Vector3.one / zoomScale, zoomInterpolation);
    }
    foreach (LineRenderer lr in pathParent.GetComponentsInChildren<LineRenderer>())
    {
      lr.material.Lerp(pathMaterial, transparentMaterial, zoomInterpolation);
    }

    zoomInterpolation += timeStep;
  }

  private void InterpolateZoomOut(float timeStep)
  {
    solarSystemParent.transform.localScale = Vector3.Lerp(Vector3.one * zoomScale, Vector3.one, -zoomInterpolation);
    pathParent.transform.localScale = Vector3.Lerp(Vector3.one * zoomScale, Vector3.one, -zoomInterpolation);
    Camera.main.transform.position = Vector3.Lerp(new Vector3(zoomedSystemTransform.position.x, zoomedSystemTransform.position.y, -10f), cameraInitialPos, zoomInterpolation);

    foreach (Transform t in solarSystemParent.transform)
    {
      t.localScale = Vector3.Lerp(Vector3.one / zoomScale, Vector3.one * 0.7f, -zoomInterpolation);
    }
    foreach (LineRenderer lr in pathParent.GetComponentsInChildren<LineRenderer>())
    {
      lr.material.Lerp(transparentMaterial, pathMaterial, -zoomInterpolation);
    }

    zoomInterpolation += timeStep;
  }

  public void ZoomOnSystem(SolarSystem s, Transform t)
  {
    zoomInterpolation = 0.01f;
    zoomedSystem = s;
    zoomedSystemTransform = t;
    cameraInitialPos = Camera.main.transform.position;
  }
}
