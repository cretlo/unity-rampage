using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using System.Threading.Tasks;

public class Leap : MonoBehaviour
{
  public InputActionProperty leftTrigger;
  public InputActionProperty rightTrigger;
  private Rigidbody _xrRb;
  private bool _isFalling;
  private CharacterStateManager _characterStateManager;
  private bool _jumping = false;
  private bool _attemptingToJump = false;

  public Transform leftController;
  public Transform rightController;

  private Vector3 leftControllerStartPos;
  private Vector3 rightControllerStartPos;

  // Start is called before the first frame update
  void Start()
  {
    _characterStateManager = GetComponent<CharacterStateManager>();
    _xrRb = GetComponent<Rigidbody>();
  }


  // Update is called once per frame
  void Update()
  {
    bool leftTriggerPressed = leftTrigger.action.IsPressed();
    bool rightTriggerPressed = rightTrigger.action.IsPressed();

    _isFalling = _xrRb.velocity.y > 0 ? true : false;

    //print("Velocity: " + _xrRb.velocity + ", IsFalling: " + _isFalling.ToString());
    if (leftTriggerPressed && rightTriggerPressed && !_attemptingToJump)
    {
      print(leftTrigger.reference.name);
      _attemptingToJump = true;
      leftControllerStartPos = leftController.position;
      rightControllerStartPos = rightController.position;

    }

    if (_attemptingToJump && !leftTriggerPressed && !rightTriggerPressed)
    {
      _attemptingToJump = false;
      Vector3 rightVector = rightControllerStartPos - rightController.position;
      Vector3 leftVector = leftControllerStartPos - leftController.position;

      CharacterStateManager.isLeaping = true;

      _characterStateManager.DeactivateOpenXRComponents();
      _characterStateManager.ActivatePhysics();
      _xrRb.AddForce((rightVector + leftVector) * 5, ForceMode.Impulse);
      _jumping = true;


    }



    // if (!_isFalling && rightTriggerPressed && !CharacterStateManager.isLeaping && !CharacterStateManager.isClimbing)
    // {



    //   CharacterStateManager.isLeaping = true;

    //   _characterStateManager.DeactivateOpenXRComponents();
    //   _characterStateManager.ActivatePhysics();
    //   _xrRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
    //   _jumping = true;
    // }
  }

  void FixedUpdate()
  {

  }

  void OnCollisionEnter(Collision collision)
  {
    // If we hit something allow moving
    _characterStateManager.DeactivatePhysics();
    _characterStateManager.ActivateOpenXRComponents();
    CharacterStateManager.isLeaping = false;


  }


}
