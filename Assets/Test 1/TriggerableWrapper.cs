using HenryLab;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wraps Test scripts as ITriggerableEntity for VRExplorer automated testing.
/// Attach this component to GameObjects with Test scripts, then select
/// the appropriate TriggerType to auto-invoke the correct trigger method.
/// </summary>
public class TriggerableWrapper : MonoBehaviour, ITriggerableEntity
{
    public enum TriggerType
    {
        /// <summary> Calls AnimateDrawer.OpenDrawer() </summary>
        OpenDrawer,
        /// <summary> Calls Collectable.Collect() </summary>
        Collect,
        /// <summary> Calls Lightable.ToggleLight() </summary>
        ToggleLight,
        /// <summary> Calls MovePillow.MovePillowAway() </summary>
        MovePillow,
        /// <summary> Calls ReplaceFlower.PlaceFlower() </summary>
        PlaceFlower,
        /// <summary> Calls FindEscape.Escape() </summary>
        Escape,
        /// <summary> Calls Inspectable.Inspect() </summary>
        Inspect,
        /// <summary> Uses custom UnityEvent callbacks </summary>
        Custom
    }

    [Header("Trigger Configuration")]
    [Tooltip("Select the trigger behavior corresponding to the Test script on this GameObject.")]
    public TriggerType triggerType = TriggerType.Custom;

    [Tooltip("Time in seconds for the triggering phase.")]
    public float triggeringTime = 0.5f;

    [Header("Custom Events (used when TriggerType is Custom)")]
    public UnityEvent onTriggerring;
    public UnityEvent onTriggerred;

    public string Name => Str.Triggerable;

    public float TriggeringTime => triggeringTime;

    private void Awake()
    {
        EntityManager.Instance.RegisterEntity(this);
    }

    /// <summary>
    /// Called when VRExplorer begins triggering this entity.
    /// </summary>
    public void Triggerring()
    {
        if (triggerType == TriggerType.Custom)
        {
            onTriggerring?.Invoke();
            return;
        }

        Debug.Log($"[TriggerableWrapper] Triggerring: {triggerType} on {gameObject.name}");
    }

    /// <summary>
    /// Called when VRExplorer finishes triggering this entity.
    /// Dispatches the configured trigger action.
    /// </summary>
    public void Triggerred()
    {
        switch (triggerType)
        {
            case TriggerType.OpenDrawer:
                InvokeOpenDrawer();
                break;
            case TriggerType.Collect:
                InvokeCollect();
                break;
            case TriggerType.ToggleLight:
                InvokeToggleLight();
                break;
            case TriggerType.MovePillow:
                InvokeMovePillow();
                break;
            case TriggerType.PlaceFlower:
                InvokePlaceFlower();
                break;
            case TriggerType.Escape:
                InvokeEscape();
                break;
            case TriggerType.Inspect:
                InvokeInspect();
                break;
            case TriggerType.Custom:
                onTriggerred?.Invoke();
                break;
        }

        Debug.Log($"[TriggerableWrapper] Triggerred: {triggerType} on {gameObject.name}");
    }

    private void InvokeOpenDrawer()
    {
        var drawer = GetComponent<AnimateDrawer>();
        if (drawer != null)
        {
            drawer.OpenDrawer();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] AnimateDrawer not found on {gameObject.name}");
        }
    }

    private void InvokeCollect()
    {
        var collectable = GetComponent<Collectable>();
        if (collectable != null)
        {
            collectable.Collect();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] Collectable not found on {gameObject.name}");
        }
    }

    private void InvokeToggleLight()
    {
        var lightable = GetComponent<Lightable>();
        if (lightable != null)
        {
            lightable.ToggleLight();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] Lightable not found on {gameObject.name}");
        }
    }

    private void InvokeMovePillow()
    {
        var movePillow = GetComponent<MovePillow>();
        if (movePillow != null)
        {
            movePillow.MovePillowAway();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] MovePillow not found on {gameObject.name}");
        }
    }

    private void InvokePlaceFlower()
    {
        var replaceFlower = GetComponent<ReplaceFlower>();
        if (replaceFlower != null)
        {
            replaceFlower.PlaceFlower();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] ReplaceFlower not found on {gameObject.name}");
        }
    }

    private void InvokeEscape()
    {
        var findEscape = GetComponent<FindEscape>();
        if (findEscape != null)
        {
            findEscape.Escape();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] FindEscape not found on {gameObject.name}");
        }
    }

    private void InvokeInspect()
    {
        var inspectable = GetComponent<Inspectable>();
        if (inspectable != null)
        {
            inspectable.Inspect();
        }
        else
        {
            Debug.LogWarning($"[TriggerableWrapper] Inspectable not found on {gameObject.name}");
        }
    }
}
