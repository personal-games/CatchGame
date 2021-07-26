using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{

  public Camera camera;
  private Rigidbody2D rigidbody2D;
  private float maxWidth;
  public Renderer renderer;

  private bool canControl;

  // Use this for initialization
  void Start()
  {
    if (camera == null)
    {
      camera = Camera.main;
    }
    canControl = false;
    renderer = GetComponent<Renderer>();
    Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
    Vector3 targetWidth = camera.ScreenToWorldPoint(upperCorner);
    float hatWidth = renderer.bounds.extents.x;
    maxWidth = targetWidth.x - hatWidth;
    rigidbody2D = GetComponent<Rigidbody2D>();
  }

  // Update is called once per physics timestamp
  void FixedUpdate()
  { if (canControl){
          Vector3 rawPosition = camera.ScreenToWorldPoint(Input.mousePosition);
          Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);
          float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
          targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
          rigidbody2D.MovePosition(targetPosition);
    }
  }

  public void ToggleControl (bool toggle){
    canControl = toggle;
  }
}
