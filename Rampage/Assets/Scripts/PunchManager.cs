using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PunchManager : MonoBehaviour
{
  Rigidbody rb;
  public Transform hand;
  public InputActionProperty primaryButton;
  public Collider punchCollider;
  private ParticleManager _particleManager;
  private bool _canPunch;
  private Vector3 _handsDir;
  void Awake()
  {
    transform.position = hand.position; // Start at rig position
    rb = GetComponent<Rigidbody>();
    _canPunch = false;
    _particleManager = GameObject.FindWithTag("ParticleManager").GetComponent<ParticleManager>();

    // Give error if a Particle Manager is not initialized
    if (_particleManager == null)
    {
      Debug.LogError("Couldn't find particle manager script on particle manager object");
    }

  }
  // Start is called before the first frame update
  void Start()
  {
  }

  void Update()
  {

    _handsDir = hand.position - rb.position;
    if (primaryButton.action.ReadValue<float>() == 1)
    {
      _canPunch = true;
    }
    else
    {
      _canPunch = false;
    }
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    //Moves physics hands to controller with controller rotation
    rb.MovePosition(hand.transform.position);
    rb.MoveRotation(hand.rotation);
  }

  void OnTriggerExit(Collider other)
  {
    if (other.transform.tag == "WallChunk")
    {
      punchCollider.isTrigger = false;
    }

  }

  void OnCollisionEnter(Collision collider)
  {

    if (_canPunch)
    {
      // if (collision.transform.tag != "WallChunk") { return; }
      Vector3 forceOfHit = collider.impulse / Time.fixedDeltaTime;
      Vector3 clampedForce = Vector3.ClampMagnitude(forceOfHit, 100);
      ContactPoint contact = collider.GetContact(0);

      // print("Unclamped Force: " + forceOfHit + ", Clamped Force: " + clampedForce);
      collider.gameObject.GetComponent<Rigidbody>().AddRelativeForce(clampedForce * 25, ForceMode.Force);
      // collision.gameObject.GetComponent<Rigidbody>().AddRelativeForce(final * 50, ForceMode.Force);

      // Spawn a hit particle
      _particleManager.SpawnPunchParticles(contact.point, contact.normal);

      // Joint damage to the WallChunk
      ConfigurableJoint[] joints = collider.gameObject.GetComponents<ConfigurableJoint>();

      if (joints.Length == 0) { return; }

      for (int i = 0; i < joints.Length; i++)
      {
        joints[i].breakForce -= 50;
      }

    }
    else
    {
      if (collider.transform.tag == "WallChunk")
      {
        punchCollider.isTrigger = true;
        // WallChunk wallChunk = other.gameObject.GetComponent<WallChunk>();
        // if (!wallChunk.isBroken)
        // {
        //   punchCollider.enabled = false;
        // }
      }

    }

  }
}
