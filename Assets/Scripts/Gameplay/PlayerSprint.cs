using Core;
using Model;
using UnityEngine;

public class PlayerSprint : Simulation.Event<PlayerSprint>
{
    private readonly IsoModel model = Simulation.GetModel<IsoModel>();

    public override void Execute()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && model.player.staminaSlider.value > 0)
        {
            model.player.moveSpeed = Mathf.Lerp(model.player.moveSpeed, model.player.sprintSpeed, 1f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || model.player.staminaSlider.value <= 0)
        {
            model.player.moveSpeed = model.player.originalSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            model.player.staminaSlider.value -= Time.deltaTime * model.player.decreaseSpeed;
            model.player.staminaSlider.value = Mathf.Clamp(model.player.staminaSlider.value, 0f, 100f);

            // handle the opacity of the stamina bar
            model.player.background.opacity += Time.deltaTime * 1f;
            model.player.fill.opacity = model.player.background.opacity;
        }
        else
        {
            model.player.staminaSlider.value += Time.deltaTime * model.player.recoverySpeed;
            model.player.staminaSlider.value = Mathf.Clamp(model.player.staminaSlider.value, 0f, 100f);

            // hide the stamina bar
            model.player.background.opacity -= Time.deltaTime * 1f;
            model.player.fill.opacity = model.player.background.opacity;
        }
    }
}
