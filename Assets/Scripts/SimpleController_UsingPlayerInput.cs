using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

// Use a separate PlayerInput component for setting up input.
public class SimpleController_UsingPlayerInput : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float xSpeed = 10f;
    [SerializeField] private float xRange = 9f;
    [SerializeField] private float ySpeed = 10f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private GameObject[] guns;

    [Header("Screen Position Based")]
    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float positionYawFactor = 5f;

    [Header("Control Throw Based")]
    [SerializeField] private float controlPitchFactor = -20f;
    [SerializeField] private float controlRollFactor = -35;


    private bool isControlEnabled = true;
    private Vector2 m_Move;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //print("fire");
        switch (context.phase)
        {
            case InputActionPhase.Started:
                ToggleGuns(true);
                break;

            case InputActionPhase.Canceled:
                ToggleGuns(false);
                break;
        }
    }

    public void OnPlayerDeath() // Called by string reference
    {
        isControlEnabled = false;
    }

    //public void OnGUI()
    //{
    //    if (m_Charging)
    //        GUI.Label(new Rect(100, 100, 200, 100), "Charging...");
    //}

    public void Update()
    {
        // Update orientation first, then move. Otherwise move orientation will lag
        if (isControlEnabled)
        {
            ProccessTranslation(m_Move);
            ProccessRotation(m_Move);
        }
    }

    private void ProccessTranslation(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
            return;

        var xSpeedFrameIndependent = xSpeed * Time.deltaTime;
        var ySpeedFrameIndependen = ySpeed * Time.deltaTime;
        var xOffset = direction.x * xSpeedFrameIndependent;
        var yOffset = direction.y * ySpeedFrameIndependen;
        var xFuturePosition = Mathf.Clamp(xOffset + transform.localPosition.x, -xRange, xRange);
        var yFuturePosition = Mathf.Clamp(yOffset + transform.localPosition.y, -yRange, yRange);
        transform.localPosition = new Vector3(xFuturePosition, yFuturePosition, transform.localPosition.z);
    }


    private void ProccessRotation(Vector2 direction)
    {

        var pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        var pitchDueToControlThrow = direction.y * controlPitchFactor;
        var pitch = pitchDueToPosition + pitchDueToControlThrow;

        var yaw = transform.localPosition.x * positionYawFactor;

        var roll = direction.x * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ToggleGuns(bool isActive)
    {
        foreach (var gun in guns)
        {
            gun.SetActive(isActive);
        }
    }
}
