using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessibilityManager : MonoBehaviour
{
    public static AccessibilityManager Instance;

    [SerializeField]
    GameObject modal;
    [SerializeField]
    Toggle OneHandedModeToggle;
    [SerializeField]
    Toggle HighVisibilityModeToggle;

    public bool OneHandedMode = false;
    public bool HighVisibilityMode = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<KeyCode> GetNearbyKeys(KeyCode key)
    {
        List<KeyCode> keys = new List<KeyCode>();
        switch(key)
        {
            case KeyCode.A:
            case KeyCode.Q:
            case KeyCode.Z:
                keys.Add(KeyCode.A);
                keys.Add(KeyCode.Q);
                keys.Add(KeyCode.Z);
                break;
            case KeyCode.S:
            case KeyCode.W:
            case KeyCode.X:
                keys.Add(KeyCode.S);
                keys.Add(KeyCode.W);
                keys.Add(KeyCode.X);
                break;
            case KeyCode.D:
            case KeyCode.E:
            case KeyCode.C:
                keys.Add(KeyCode.D);
                keys.Add(KeyCode.E);
                keys.Add(KeyCode.C);
                break;
            case KeyCode.F:
            case KeyCode.R:
            case KeyCode.V:
                keys.Add(KeyCode.F);
                keys.Add(KeyCode.R);
                keys.Add(KeyCode.V);
                break;
            case KeyCode.G:
            case KeyCode.T:
            case KeyCode.B:
                keys.Add(KeyCode.G);
                keys.Add(KeyCode.T);
                keys.Add(KeyCode.B);
                break;
            case KeyCode.H:
            case KeyCode.Y:
            case KeyCode.N:
                keys.Add(KeyCode.H);
                keys.Add(KeyCode.Y);
                keys.Add(KeyCode.N);
                break;
            case KeyCode.J:
            case KeyCode.U:
            case KeyCode.M:
                keys.Add(KeyCode.J);
                keys.Add(KeyCode.U);
                keys.Add(KeyCode.M);
                break;
            case KeyCode.K:
            case KeyCode.I:
            case KeyCode.Comma:
                keys.Add(KeyCode.K);
                keys.Add(KeyCode.I);
                keys.Add(KeyCode.Comma);
                break;
        }
        keys.Remove(key);
        return keys;
    }

    public void ShowModal()
    {
        modal.SetActive(true);
    }

    public void HideModal()
    {
        modal.SetActive(false);
    }

    public void UpdateOneHandedMode()
    {
        OneHandedMode = OneHandedModeToggle.isOn;
    }

    public void UpdateHighVisibilityMode()
    {
        HighVisibilityMode = HighVisibilityModeToggle.isOn;
    }
}
