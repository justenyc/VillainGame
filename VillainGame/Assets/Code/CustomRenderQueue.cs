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
            Image image = GetComponent<Image>();
            Material existingGlobalMat = image.materialForRendering;
            Material updatedMaterial = new Material(existingGlobalMat);
            updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
            image.material = updatedMaterial;
        }
    }
}