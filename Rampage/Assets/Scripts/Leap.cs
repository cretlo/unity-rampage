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
  public CharacterStateManager stateManager;
  private bool _jumping = false;

  // Start is called before the first frame update
  void Start()
  {
    _xrRb = GetComponent<Rigidbody>();
    stateManager = GetComponent<CharacterStateManager>();
  }


  // Update is called once per frame
  void Update()
  {
    bool leftTriggerPressed = leftTrigger.action.IsPressed();
    bool rightTriggerPressed = rightTrigger.action.IsPressed();

    _isFalling = _xrRb.velocity.y == 0 ? false : true;

    //print("Velocity: " + _xrRb.velocity + ", IsFalling: " + _isFalling.ToString());

    if (!_isFalling && rightTriggerPressed && !_jumping)
    {
      print("Jump");
      deactivateCompoonents();
      activatePhysics();
      _xrRb.AddForce(Vector3.up * 20, ForceMode.Impulse);
      _jumping = true;
    }
  }

  void activatePhysics()
  {
    _xrRb.isKinematic = false;
    _xrRb.useGravity = true;
  }
  void deactivateCompoonents()
  {
    GetComponent<ContinuousMoveProviderBase>().enabled = false;
    GetComponent<ContinuousTurnProviderBase>().enabled = false;
    GetComponent<CharacterController>().enabled = false;
    GetComponent<LocomotionSystem>().enabled = false;
  }

  void FixedUpdate()
  {

  }


}
