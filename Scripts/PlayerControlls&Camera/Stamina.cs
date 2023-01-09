using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float stamina = 100;
    [SerializeField] private float staminaGain = 25;
    [SerializeField] private float lowStaminaGain = 25;
    [SerializeField] private float runStaminaDrain = 10;
    [SerializeField] private float jupStaminaDrain = 25;
    private float staminaStartValue;
    [SerializeField] private Slider slider;

    [HideInInspector] public bool HaveStamina;

    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        staminaStartValue = stamina;

        if(slider != null)
        {
            slider.maxValue = staminaStartValue;
            slider.value = stamina;
        }
    }

    private void Update()
    {
        UpdateStamina();
    }

    void UpdateStamina()
    {
        if(slider != null)
        {
            slider.value = stamina;
        }

        if (playerMovement.IsRuning() && playerMovement.IsGrounded() && stamina >= 0)
            stamina -= runStaminaDrain * Time.deltaTime;
        else if (!playerMovement.IsRuning() && playerMovement.IsGrounded() && stamina <= staminaStartValue / 3)
            stamina += (lowStaminaGain * Time.deltaTime);
        else if (!playerMovement.IsRuning() && playerMovement.IsGrounded() && stamina <= staminaStartValue)
            stamina += staminaGain * Time.deltaTime;

        if (stamina <= 0)
            HaveStamina = false;
        else if (!HaveStamina)
        {
            if (stamina >= jupStaminaDrain)
            {
                HaveStamina = true;
            }
        }
    }

    public void JumpDrain(bool onGround)
    {
        if (onGround && stamina >= jupStaminaDrain)
        stamina -= jupStaminaDrain;
    }
    
}
