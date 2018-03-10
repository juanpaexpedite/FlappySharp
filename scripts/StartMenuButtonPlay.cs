using Godot;
using System;

public class StartMenuButtonPlay : TextureButton
{
     StageManager manager;
    public override void _Ready()
    {
        manager = (StageManager)GetNode("/root/CurrentStageManager");
    }

    private void OnButtonPlayPressed()
    {
        //TODO: Solve the 
        //ERROR: godot_icall_1_43: Parameter ' ptr ' is null.
        //At: modules\mono\glue\mono_glue.gen.cpp:399

        if(manager!=null)
        {
            GD.Print("Changing Scene");
            manager.ChangeStage(manager.StageGamePath);
        }

        //INFORMATION: Just for testing purposes without fadein,fadeout
        //GD.Print("Reload Scene");
        //GetTree().ReloadCurrentScene();
    }
}
