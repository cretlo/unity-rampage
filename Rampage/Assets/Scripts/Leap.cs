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

  public Transform leftController;
  public Transform rightController;

  private Rigidbody _xrRb;
  private bool _isFalling;
  private CharacterStateManager _characterStateManager;
  private bool _attemptingToLeap = false;
  private Vector3 _leftControllerStartPos;
  private Vector3 _rightControllerStartPos;

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
    bool _leftTriggerPressed = leftTrigger.action.IsPressed();
    bool _rightTriggerPressed = rightTrigger.action.IsPressed();

    _isFalling = _xrRb.velocity.y > 0 ? true : false;

    // Dont allow any leaping if climbing
    if (CharacterStateManager.isClimbing || CharacterStateManager.isLeaping)
    {
      return;
    }

    //print("Velocity: " + _xrRb.velocity + ", IsFalling: " + _isFalling.ToString());
    if (_leftTriggerPressed && _rightTriggerPressed && !_attemptingToLeap)
    {
      _attemptingToLeap = true;
      _leftControllerStartPos = leftController.position;
      _rightControllerStartPos = rightController.position;

    }

    if (_attemptingToLeap && !_leftTriggerPressed && !_rightTriggerPressed)
    {
      _attemptingToLeap = false;
      Vector3 rightVector = _rightControllerStartPos - rightController.position;
      Vector3 leftVector = _leftControllerStartPos - leftController.position;


      _characterStateManager.LeapingState();
      _xrRb.AddForce((rightVector + leftVector) * 5, ForceMode.Impulse);


    }



    // if (!_isFalling && rightTriggerPressed && !CharacterStateManager.isLeaping && !CharacterStateManager.isClimbing)
    // {
  }

  void FixedUpdate()
  {

  }

  void OnCollisionEnter(Collision collision)
  {
    // If we hit something, and not climbing, allow movement
    if (!CharacterStateManager.isClimbing)
    {
      _characterStateManager.NormalState();
    }


  }


}
