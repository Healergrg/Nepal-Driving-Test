using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProManualCarController : MonoBehaviour
{
    [Header("Engine & Transmission")]
    public float idleRPM = 800f;
    public float maxRPM = 7000f;
    // Gears: 0 = Reverse, 1 = Neutral, 2 = 1st, 3 = 2nd, 4 = 3rd, 5 = 4th
    public float[] gearMaxSpeeds = { -15f, 0f, 20f, 40f, 65f, 100f }; 
    public float[] gearAcceleration = { 15f, 0f, 18f, 12f, 8f, 5f }; 
    
    [Header("Brakes & Handling")]
    public float brakingPower = 20f;
    public float handbrakePower = 40f;
    public float turnSpeed = 40f;
    public float coastingDrag = 2f; // How fast you slow down when off the gas

    [Header("Live Telemetry (View Only)")]
    public int currentGear = 2; // Starts in 1st Gear
    public float currentSpeed;
    public float currentRPM;
    public bool isClutchEngaged;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        HandleShifting();
    }

    void FixedUpdate()
    {
        // 1. Read Inputs
        float gasInput = Input.GetKey(KeyCode.W) ? 1f : 0f;
        float brakeInput = Input.GetKey(KeyCode.S) ? 1f : 0f;
        float steerInput = 0f;
        if (Input.GetKey(KeyCode.A)) steerInput = -1f;
        if (Input.GetKey(KeyCode.D)) steerInput = 1f;
        
        isClutchEngaged = Input.GetKey(KeyCode.LeftShift);
        bool handbrake = Input.GetKey(KeyCode.Space);

        // Calculate current forward speed
        currentSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        // 2. Handle Steering
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            float direction = (currentSpeed > 0) ? 1f : -1f;
            // Handbrake makes the car turn sharper (drifting feel)
            float activeTurnSpeed = handbrake ? turnSpeed * 1.5f : turnSpeed; 
            
            float turnAmount = steerInput * activeTurnSpeed * direction * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // 3. Engine & Speed Logic
        float targetSpeed = currentSpeed;

        if (handbrake)
        {
            // Handbrake slows car down rapidly
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, handbrakePower * Time.fixedDeltaTime);
        }
        else if (brakeInput > 0)
        {
            // Foot brake slows car down normally
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakingPower * Time.fixedDeltaTime);
        }
        else if (!isClutchEngaged && currentGear != 1) // If clutch is UP and NOT in Neutral
        {
            float maxSpeedForGear = gearMaxSpeeds[currentGear];
            float accelerationForGear = gearAcceleration[currentGear];

            if (gasInput > 0)
            {
                // Accelerate towards the max speed of the current gear
                targetSpeed = Mathf.MoveTowards(currentSpeed, maxSpeedForGear, accelerationForGear * Time.fixedDeltaTime);
            }
            else
            {
                // Engine braking (slowing down when off the gas but in gear)
                targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, coastingDrag * Time.fixedDeltaTime);
            }
        }
        else
        {
            // Coasting (Clutch is IN or car is in Neutral)
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, (coastingDrag / 2f) * Time.fixedDeltaTime);
        }

        // 4. Calculate Simulated RPM (For audio or UI later)
        if (isClutchEngaged || currentGear == 1)
        {
            // If clutch is in, gas pedal directly controls RPM
            currentRPM = Mathf.Lerp(idleRPM, maxRPM, gasInput);
        }
        else
        {
            // If clutch is out, RPM is tied to wheel speed and current gear
            float speedRatio = Mathf.Abs(currentSpeed) / Mathf.Abs(gearMaxSpeeds[currentGear] + 0.1f);
            currentRPM = Mathf.Lerp(idleRPM, maxRPM, speedRatio);
        }

        // 5. Apply Movement to Rigidbody
        Vector3 newVelocity = transform.forward * targetSpeed;
        newVelocity.y = rb.linearVelocity.y; // Keep gravity working
        rb.linearVelocity = newVelocity;
    }

    private void HandleShifting()
    {
        // Shift Up (E)
        if (Input.GetKeyDown(KeyCode.E) && isClutchEngaged)
        {
            if (currentGear < gearMaxSpeeds.Length - 1)
            {
                currentGear++;
            }
        }
        // Shift Down (Q)
        if (Input.GetKeyDown(KeyCode.Q) && isClutchEngaged)
        {
            if (currentGear > 0)
            {
                currentGear--;
            }
        }
    }
}