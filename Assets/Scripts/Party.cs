using UnityEngine;

[RequireComponent(typeof(PartyController))]
public class Party : MonoBehaviour
{
    private PartyController _controller;
    private PartyInput _input;

    private void Awake()
    {
        _controller = GetComponent<PartyController>();
        _input = new PartyInput(_controller);
    }

    private void Update()
    {
        _input.HandleInput();
    }
}