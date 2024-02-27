using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExample : MonoBehaviour
{
    [SerializeField] private GameObject targetIndicator;

    // inputs
    private MagicLeapInputs magicLeapInputs;
    private MagicLeapInputs.ControllerActions controllerActions;

    // Start is called before the first frame update
    void Start()
    {
        targetIndicator.SetActive(false);
        magicLeapInputs = new MagicLeapInputs();
        magicLeapInputs.Enable();
        controllerActions = new MagicLeapInputs.ControllerActions(magicLeapInputs);
    }

    // Update is called once per frame
    void Update()
    {
        // raycast from the controller outward
        Ray raycastRay = new Ray(controllerActions.Position.ReadValue<Vector3>(), controllerActions.Rotation.ReadValue<Quaternion>() * Vector3.forward);

        // if ray hits an object on the Planes layer, position the indicator at the hit point and set it as active
        if (Physics.Raycast(raycastRay, out RaycastHit hitInfo, 100, LayerMask.GetMask("Planes")))
        {
            Debug.Log(hitInfo.transform);
            targetIndicator.transform.position = hitInfo.point;
            targetIndicator.transform.rotation = Quaternion.LookRotation(-hitInfo.normal);
            targetIndicator.gameObject.SetActive(true);
        }
    }
}
