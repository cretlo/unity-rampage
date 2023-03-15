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
  private bool _attemptingToLeap = false;

  public Transform leftController;
  public Transform rightController;

  private Vector3 leftControllerStartPos;
  private Vector3 rightControllerStartPos;

  // Start is called before the first frame update
  void Awake()
  {
    _characterStateManager = GetComponent<CharacterStateManager>();
    _xrRb = GetComponent<Rigidbody>();
  }

  void Start()
  {

  }


  // Update is called once per frame
  void Update()
  {
    bool leftTriggerPressed = leftTrigger.action.IsPressed();
    bool rightTriggerPressed = rightTrigger.action.IsPressed();

    _isFalling = _xrRb.velocity.y > 0 ? true : false;

    // Dont allow any leaping if climbing
    if (CharacterStateManager.isClimbing || CharacterStateManager.isLeaping)
    {
      return;
    }

    //print("Velocity: " + _xrRb.velocity + ", IsFalling: " + _isFalling.ToString());
    if (leftTriggerPressed && rightTriggerPressed && !_attemptingToLeap)
    {
      _attemptingToLeap = true;
      leftControllerStartPos = leftController.position;
      rightControllerStartPos = rightController.position;

    }

    if (_attemptingToLeap && !leftTriggerPressed && !rightTriggerPressed)
    {
      _attemptingToLeap = false;
      Vector3 rightVector = rightControllerStartPos - rightController.position;
      Vector3 leftVector = leftControllerStartPos - leftController.position;


      _characterStateManager.DeactivateOpenXRComponents();
      _characterStateManager.ActivatePhysics();
      _xrRb.AddForce((rightVector + leftVector) * 5, ForceMode.Impulse);

      CharacterStateManager.isLeaping = true;

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
    // If we hit something, and not climbing, allow movement
    if (!CharacterStateManager.isClimbing)
    {
      _characterStateManager.DeactivatePhysics();
      _characterStateManager.ActivateOpenXRComponents();
      CharacterStateManager.isLeaping = false;


    }


  }


}
