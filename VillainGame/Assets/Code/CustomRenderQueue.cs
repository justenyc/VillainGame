using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CustomRenderQueue : MonoBehaviour
{

    public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;

    public bool apply = false;

    private void Start()
    {
        apply = true;
        ApplyZtestChange();
    }

    private void ApplyZtestChange()
    {
        if (apply)
        {
            apply = false;

            if (TryGetComponent(out Image img))
            {
                Material existingGlobalMat = img.materialForRendering;
                Material updatedMaterial = new Material(existingGlobalMat);
                updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
                img.material = updatedMaterial;
            }
            else if (TryGetComponent(out Text txt))
            {
                Material existingGlobalMat = txt.materialForRendering;
                Material updatedMaterial = new Material(existingGlobalMat);
                updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
                txt.material = updatedMaterial;
            }
        }
    }
}