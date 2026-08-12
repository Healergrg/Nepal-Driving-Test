// using UnityEngine;
// using TMPro; // <-- NEW: Required to talk to TextMeshPro UI!

// [RequireComponent(typeof(Rigidbody))]
// public class ProManualCarController : MonoBehaviour
// {
//     [Header("Engine & Transmission")]
//     public float idleRPM = 800f;
//     public float maxRPM = 7000f;
//     // Gears: 0 = Reverse, 1 = Neutral, 2 = 1st, 3 = 2nd, 4 = 3rd, 5 = 4th
//     public float[] gearMaxSpeeds = { -15f, 0f, 20f, 40f, 65f, 100f }; 
//     public float[] gearAcceleration = { 15f, 0f, 18f, 12f, 8f, 5f }; 
    
//     [Header("Brakes & Handling")]
//     public float brakingPower = 20f;
//     public float handbrakePower = 40f;
//     public float turnSpeed = 40f;
//     public float coastingDrag = 2f; 

//     [Header("Live Telemetry (View Only)")]
//     public int currentGear = 2; // Starts in 1st Gear
//     public float currentSpeed;
//     public float currentRPM;
//     public bool isClutchEngaged;

//     [Header("Dashboard UI")] // <-- NEW SECTION
//     public TextMeshProUGUI gearTextDisplay; 
//     // This array translates the gear number (0-5) into text on the screen!
//     private string[] gearNames = { "R", "N", "1", "2", "3", "4" }; 

//     private Rigidbody rb;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         rb.centerOfMass = new Vector3(0, -0.5f, 0);
//     }

//     void Update()
//     {
//         HandleShifting();
//         UpdateDashboard(); // <-- NEW: Updates the UI every frame
//     }

//     void FixedUpdate()
//     {
//         // 1. Read Inputs
//         float gasInput = Input.GetKey(KeyCode.W) ? 1f : 0f;
//         float brakeInput = Input.GetKey(KeyCode.S) ? 1f : 0f;
//         float steerInput = 0f;
//         if (Input.GetKey(KeyCode.A)) steerInput = -1f;
//         if (Input.GetKey(KeyCode.D)) steerInput = 1f;
        
//         isClutchEngaged = Input.GetKey(KeyCode.LeftShift);
//         bool handbrake = Input.GetKey(KeyCode.Space);

//         currentSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

//         // 2. Handle Steering
//         if (Mathf.Abs(currentSpeed) > 0.5f)
//         {
//             float direction = (currentSpeed > 0) ? 1f : -1f;
//             float activeTurnSpeed = handbrake ? turnSpeed * 1.5f : turnSpeed; 
            
//             float turnAmount = steerInput * activeTurnSpeed * direction * Time.fixedDeltaTime;
//             Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
//             rb.MoveRotation(rb.rotation * turnRotation);
//         }

//         // 3. Engine & Speed Logic
//         float targetSpeed = currentSpeed;

//         if (handbrake)
//         {
//             targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, handbrakePower * Time.fixedDeltaTime);
//         }
//         else if (brakeInput > 0)
//         {
//             targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakingPower * Time.fixedDeltaTime);
//         }
//         else if (!isClutchEngaged && currentGear != 1) 
//         {
//             float maxSpeedForGear = gearMaxSpeeds[currentGear];
//             float accelerationForGear = gearAcceleration[currentGear];

//             if (gasInput > 0)
//             {
//                 targetSpeed = Mathf.MoveTowards(currentSpeed, maxSpeedForGear, accelerationForGear * Time.fixedDeltaTime);
//             }
//             else
//             {
//                 targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, coastingDrag * Time.fixedDeltaTime);
//             }
//         }
//         else
//         {
//             targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, (coastingDrag / 2f) * Time.fixedDeltaTime);
//         }

//         // 4. Calculate Simulated RPM
//         if (isClutchEngaged || currentGear == 1)
//         {
//             currentRPM = Mathf.Lerp(idleRPM, maxRPM, gasInput);
//         }
//         else
//         {
//             float speedRatio = Mathf.Abs(currentSpeed) / Mathf.Abs(gearMaxSpeeds[currentGear] + 0.1f);
//             currentRPM = Mathf.Lerp(idleRPM, maxRPM, speedRatio);
//         }

//         // 5. Apply Movement to Rigidbody
//         Vector3 newVelocity = transform.forward * targetSpeed;
//         newVelocity.y = rb.linearVelocity.y; 
//         rb.linearVelocity = newVelocity;
//     }

//     private void HandleShifting()
//     {
//         if (Input.GetKeyDown(KeyCode.E) && isClutchEngaged)
//         {
//             if (currentGear < gearMaxSpeeds.Length - 1)
//             {
//                 currentGear++;
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.Q) && isClutchEngaged)
//         {
//             if (currentGear > 0)
//             {
//                 currentGear--;
//             }
//         }
//     }

//     // <-- NEW: Function to change the text on screen
//     private void UpdateDashboard() 
//     {
//         if (gearTextDisplay != null)
//         {
//             // Changes the text to say "GEAR: 1", "GEAR: R", etc.
//             gearTextDisplay.text = "GEAR: " + gearNames[currentGear]; 
//         }
//     }
// }
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class ProManualCarController : MonoBehaviour
{
    [Header("Engine & Transmission")]
    public float idleRPM = 800f;
    public float maxRPM = 7000f;
    public float[] gearMaxSpeeds = { -15f, 0f, 20f, 40f, 65f, 100f }; 
    public float[] gearAcceleration = { 15f, 0f, 18f, 12f, 8f, 5f }; 
    
    [Header("Brakes & Handling")]
    public float brakingPower = 20f;
    public float handbrakePower = 40f;
    public float turnSpeed = 40f;
    public float coastingDrag = 2f; 

    [Header("Live Telemetry (View Only)")]
    public int currentGear = 2; 
    public float currentSpeed;
    public float currentRPM;
    public bool isClutchEngaged;

    [Header("Dashboard UI")]
    public TextMeshProUGUI gearTextDisplay; 
    private string[] gearNames = { "R", "N", "1", "2", "3", "4" }; 

    // <-- NEW: Camera System Variables -->
    [Header("Camera System")]
    public GameObject[] carCameras; // Drag your camera objects into this array in the Inspector
    private int currentCameraIndex = 0;
    private bool wasCameraBtnPressed = false;
    // <-- END NEW -->

    private Rigidbody rb;

    private bool wasGearUpPressed = false;
    private bool wasGearDownPressed = false;
    private bool wasReversePressed = false; // <-- NEW: Tracker for the Reverse button

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        HandleShifting();
        UpdateDashboard();
        HandleCameraSwap(); // <-- NEW: Added to Update loop
    }

    void FixedUpdate()
    {
        // 1. Read Inputs
        float gasInput = (Input.GetKey(KeyCode.W) || MobileInputBridge.CheckButton("Gas")) ? 1f : 0f;
        float brakeInput = (Input.GetKey(KeyCode.S) || MobileInputBridge.CheckButton("Brake")) ? 1f : 0f;
        
        float steerInput = 0f;
        if (Input.GetKey(KeyCode.A) || MobileInputBridge.CheckButton("Left")) steerInput = -1f;
        if (Input.GetKey(KeyCode.D) || MobileInputBridge.CheckButton("Right")) steerInput = 1f;
        
        isClutchEngaged = Input.GetKey(KeyCode.LeftShift) || MobileInputBridge.CheckButton("Clutch");
        bool handbrake = Input.GetKey(KeyCode.Space) || MobileInputBridge.CheckButton("Handbrake");

        currentSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        // 2. Handle Steering
        if (Mathf.Abs(currentSpeed) > 0.5f)
        {
            float direction = (currentSpeed > 0) ? 1f : -1f;
            float activeTurnSpeed = handbrake ? turnSpeed * 1.5f : turnSpeed; 
            
            float turnAmount = steerInput * activeTurnSpeed * direction * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turnAmount, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }

        // 3. Engine & Speed Logic
        float targetSpeed = currentSpeed;

        if (handbrake)
        {
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, handbrakePower * Time.fixedDeltaTime);
        }
        else if (brakeInput > 0)
        {
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakingPower * Time.fixedDeltaTime);
        }
        else if (!isClutchEngaged && currentGear != 1) 
        {
            float maxSpeedForGear = gearMaxSpeeds[currentGear];
            float accelerationForGear = gearAcceleration[currentGear];

            if (gasInput > 0)
            {
                targetSpeed = Mathf.MoveTowards(currentSpeed, maxSpeedForGear, accelerationForGear * Time.fixedDeltaTime);
            }
            else
            {
                targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, coastingDrag * Time.fixedDeltaTime);
            }
        }
        else
        {
            targetSpeed = Mathf.MoveTowards(currentSpeed, 0f, (coastingDrag / 2f) * Time.fixedDeltaTime);
        }

        // 4. Calculate Simulated RPM
        if (isClutchEngaged || currentGear == 1)
        {
            currentRPM = Mathf.Lerp(idleRPM, maxRPM, gasInput);
        }
        else
        {
            float speedRatio = Mathf.Abs(currentSpeed) / Mathf.Abs(gearMaxSpeeds[currentGear] + 0.1f);
            currentRPM = Mathf.Lerp(idleRPM, maxRPM, speedRatio);
        }

        // 5. Apply Movement to Rigidbody
        Vector3 newVelocity = transform.forward * targetSpeed;
        newVelocity.y = rb.linearVelocity.y; 
        rb.linearVelocity = newVelocity;
    }

    private void HandleShifting()
    {
        bool mobileGearUp = MobileInputBridge.CheckButton("GearUp");
        bool mobileGearDown = MobileInputBridge.CheckButton("GearDown");
        bool mobileReverse = MobileInputBridge.CheckButton("Reverse"); // <-- NEW: Added Reverse check

        // Shift UP 
        if ((Input.GetKeyDown(KeyCode.E) || (mobileGearUp && !wasGearUpPressed)) && isClutchEngaged)
        {
            if (currentGear < gearMaxSpeeds.Length - 1)
            {
                currentGear++;
            }
        }

        // Shift DOWN 
        if ((Input.GetKeyDown(KeyCode.Q) || (mobileGearDown && !wasGearDownPressed)) && isClutchEngaged)
        {
            if (currentGear > 0)
            {
                currentGear--;
            }
        }

        // <-- NEW: Dedicated Reverse Toggle Button Logic -->
        // This allows mobile players to instantly swap between 1st Gear and Reverse without holding a clutch!
        if (mobileReverse && !wasReversePressed)
        {
            if (currentGear != 0) 
                currentGear = 0; // Put in Reverse (R)
            else 
                currentGear = 2; // Put back in 1st Gear (1)
        }
        // <-- END NEW -->

        wasGearUpPressed = mobileGearUp;
        wasGearDownPressed = mobileGearDown;
        wasReversePressed = mobileReverse; // <-- NEW
    }

    // <-- NEW: Camera Switching Method -->
    private void HandleCameraSwap()
    {
        bool mobileCameraBtn = MobileInputBridge.CheckButton("CameraSwap");
        
        if (Input.GetKeyDown(KeyCode.C) || (mobileCameraBtn && !wasCameraBtnPressed))
        {
            if (carCameras.Length > 0)
            {
                // Turn off current camera
                carCameras[currentCameraIndex].SetActive(false);
                // Move to the next camera, loop back to 0 if at the end
                currentCameraIndex = (currentCameraIndex + 1) % carCameras.Length;
                // Turn on new camera
                carCameras[currentCameraIndex].SetActive(true);
            }
        }
        wasCameraBtnPressed = mobileCameraBtn;
    }
    // <-- END NEW -->

    private void UpdateDashboard() 
    {
        if (gearTextDisplay != null)
        {
            gearTextDisplay.text = "GEAR: " + gearNames[currentGear]; 
        }
    }
}