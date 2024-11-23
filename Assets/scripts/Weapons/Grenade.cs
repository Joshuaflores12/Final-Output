using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public int currentAmmo;
    public int maxAmmo = 5;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI ReloadUpdater;
    [SerializeField] private GameObject Throwable;
    [SerializeField] private Transform ThrowableHolder;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float launchForce = 1.5f;
    [SerializeField] private float trajectoryTimeStep = 0.05f;
    [SerializeField] private int trajectoryStepCount = 15;
    private Vector2 velocity, startMousePos, currentMousePos;
    private bool isDrawingTrajectory = false;
    [SerializeField] private AudioSource PinSource;
    [SerializeField] private AudioClip pinSound;
    [SerializeField] private AudioSource throwSource;
    [SerializeField] private AudioClip throwclip;

    void Start()
    {
        currentAmmo = maxAmmo;

        Ammo.text = "Ammo: " + currentAmmo;

        ReloadUpdater.text = "";

        lineRenderer.enabled = false;
    }

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
            {
                startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                isDrawingTrajectory = true;

                lineRenderer.enabled = true;

                PinSource.PlayOneShot(pinSound);
            }
            else
            {
                ReloadUpdater.text = "Out of Nades!";
            }
        }

        
        if (Input.GetMouseButton(0) && isDrawingTrajectory)
        {
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            velocity = (startMousePos - currentMousePos) * launchForce;

            DrawTrajectory();
        }

        
        if (Input.GetMouseButtonUp(0) && isDrawingTrajectory)
        {
            isDrawingTrajectory = false;

            lineRenderer.enabled = false;

            ThrowGrenade();
        }
    }

    private void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];

        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;

            Vector3 pos = (Vector2)ThrowableHolder.position + velocity * t + 0.5f * Physics2D.gravity * t * t;

            positions[i] = pos;
        }

        lineRenderer.positionCount = trajectoryStepCount;

        lineRenderer.SetPositions(positions);
    }

    private void ThrowGrenade()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;

            Ammo.text = "" + currentAmmo;

            GameObject grenade = Instantiate(Throwable, ThrowableHolder.position, Quaternion.identity);

            Rigidbody2D rb = grenade.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = velocity; 
            }

            throwSource.PlayOneShot(throwclip); 
        }
        else
        {
            ReloadUpdater.text = "Out of Nades!";
        }
    }
    public void OnEnable()
    {
        ReloadUpdater.text = "";
    }
}
