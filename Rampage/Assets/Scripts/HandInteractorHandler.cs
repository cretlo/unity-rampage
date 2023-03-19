using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class HandInteractorHandler : MonoBehaviour
{
  XRDirectInteractor _directInteractor;
  XRPokeInteractor _pokeInteractor;
  // Start is called before the first frame update
  void Awake()
  {
    _directInteractor = GetComponent<XRDirectInteractor>();
    _pokeInteractor = GetComponent<XRPokeInteractor>();

  }

  void Start()
  {
    DirectState();

    // _directInteractor = GetComponent<XRDirectInteractor>();
    // _pokeInteractor = GetComponent<XRPokeInteractor>();




  }
  void OnEnable()
  {
    MenuHandler.MenuOpen += PokeState;
    MenuHandler.MenuClosed += DirectState;

  }

  void OnDisable()
  {
    MenuHandler.MenuOpen -= PokeState;
    MenuHandler.MenuClosed -= DirectState;

  }

  void DirectState()
  {
    _pokeInteractor = GetComponent<XRPokeInteractor>();
    _directInteractor = GetComponent<XRDirectInteractor>();

    // Already in state
    if (_directInteractor != null)
    {
      return;
    }

    // Check if theres a poke interactor added before we attempt to remove
    if (_pokeInteractor != null)
    {
      DestroyImmediate(_pokeInteractor);
    }

    // Add the appropriate component
    _directInteractor = gameObject.AddComponent<XRDirectInteractor>();
  }

  void PokeState()
  {

    _pokeInteractor = GetComponent<XRPokeInteractor>();
    _directInteractor = GetComponent<XRDirectInteractor>();

    // Already in state
    if (_pokeInteractor != null)
    {
      return;
    }

    // Check if theres a direct interactor added before we attempt to remove
    if (_directInteractor != null)
    {
      DestroyImmediate(_directInteractor);
    }

    // Add appropriate components
    _pokeInteractor = gameObject.AddComponent<XRPokeInteractor>();
    _pokeInteractor.pokeDepth = 0.5f;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
