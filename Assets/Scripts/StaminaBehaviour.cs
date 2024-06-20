using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StaminaBehaviour : MonoBehaviour
{
    public enum State
    {
        NONE,
        WASTING,
        CHARGING
    };

    [Header("PROPERTIES")]
    [SerializeField] private float maxStamina;
    [SerializeField] private float delayForCharge;
    [SerializeField] private float wastingSpeedMultipiler;
    [SerializeField] private float rechargingSpeedMultipiler;

    [Header("INFORMATION")]
    [SerializeField] private float currentStamina;
    [SerializeField] private float rechargeTimer;
    [SerializeField] private float targetStamina;
    [SerializeField] private State state;

    [Header("EVENTS")]
    public UnityEvent<float> UpdateStamina;

    private void OnEnable()
    {
        currentStamina = maxStamina;
        targetStamina = -1;
        state = State.NONE;
    }

    private void Update()
    {
        if (state == State.WASTING)
        {
            if (currentStamina > targetStamina)
            {
                currentStamina -= Time.deltaTime * wastingSpeedMultipiler;
            }
            
            if (currentStamina<= targetStamina)
            {
                currentStamina = targetStamina;
                targetStamina = -1;
                rechargeTimer = 0;
                state = State.CHARGING;
            }
        }
        else if (state == State.CHARGING)
        {
            if (rechargeTimer < delayForCharge)
            {
                rechargeTimer += Time.deltaTime;
            }
            else
            {
                if (currentStamina < maxStamina)
                {
                    currentStamina += Time.deltaTime * rechargingSpeedMultipiler;
                }
                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                    state = State.NONE;
                    SoundManager.Instance.PlayAudio("MaxStamina");
                }
            }
        }

        UpdateStamina.Invoke(currentStamina);
    }

    public bool HaveNeededStamina(int percent)
    {
        if (targetStamina >= 0 && targetStamina <= 1)
        {
            if (percent > targetStamina * 100)
            {
                return false;
            }
            else return true;
        }
        else
        {
            if (percent > currentStamina * 100)
            {
                return false;
            }
            else return true;
        }

    }

    public bool MaxStamina()
    {
        if (currentStamina >= maxStamina)
        {
            return true;
        }
        else return false;
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    public void WasteStamina(int percent)
    {
        if (targetStamina>=0 && targetStamina <= 1)
        {
            targetStamina -= (percent / 100f);
        }
        else
        {
            targetStamina = currentStamina - (percent / 100f);
        }

        state = State.WASTING;
    }
}
