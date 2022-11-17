using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;

[DataContract]
public class RoadCenter : ITrackingCenter
{
    [EnumMember]
    private List<Camera> cameras = new List<Camera>();

    public void AddCamera(int id, int maxSpeed)
    {
        cameras.Add(new Camera(id, maxSpeed));
    }

    public void CheckCarSpeed(int forCameraId, int carNumber, int carSpeed)
    {
        foreach (var camera in cameras)
        {
            if (camera.id == forCameraId)
            {
                camera.AddPenalty(carNumber, carSpeed);
                break;
            }
        }
    }
        
    public void GetData(string inFilePath)
    {
        var format = new DataContractJsonSerializer(typeof(List<Camera>));
        using (var stream = new FileStream(inFilePath, FileMode.Create, FileAccess.Write))
        {
            var formatter = new DataContractJsonSerializer(typeof(List<Camera>));
            format.WriteObject(stream, cameras);
        }

        var text = File.ReadAllText(inFilePath);
        File.WriteAllText(inFilePath, "{\"cameras\":" + text + "}");
    }
}