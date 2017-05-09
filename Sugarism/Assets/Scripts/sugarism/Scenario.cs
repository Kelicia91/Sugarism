using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scenario
{
    private Sugarism.Scenario _model;

    private List<Scene> _sceneList;

    public Scenario(Sugarism.Scenario model)
    {
        if (null == model)
        {
            Log.Error("Not Found Sugarism.Scenario");
            return;
        }

        _model = model;

        _sceneList = new List<Scene>();
        foreach(Sugarism.Scene mScene in _model.SceneList)
        {
            Scene scene = new Scene(mScene);
            _sceneList.Add(scene);
        }
    }

    public void Execute()
    {
        Log.Debug(ToString());

        foreach(Scene scene in _sceneList)
        {
            scene.Execute();
        }
    }
}
