// https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio

using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

using Psim.Materials;
using Psim.ModelComponents;

namespace Psim.IOManagers
{
    static class InputManager
    {
        public static Model InitializeModel(string path)
        {
            JObject modelData = LoadJson(path);
            // This model can only handle 1 material
            Material material = GetMaterial(modelData["materials"][0]);
            Model model = GetModel(material, modelData["settings"]);
            AddSensors(model, modelData["sensors"]);
            AddCells(model, modelData["cells"]);
            return model;
        }
        private static JObject LoadJson(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                JObject modelData = JObject.Parse(json);
                return modelData;
            }
        }

        private static void AddCells(Model m, JToken cellData)
        {
            IList<JToken> cellTokens = cellData.Children().ToList();
            foreach(var token in cellTokens)
            {
                m.AddCell((double)token["length"], (double)token["width"], (int)token["sensorID"]);
                System.Console.WriteLine($"Successfully added a {token["length"]} X {token["width"]} cell to the model. The cell is linked to sensor {token["sensorID"]}");
            }
        }

        private static void AddSensors(Model m, JToken sensorData)
        {
            IList<JToken> sensorTokens = sensorData.Children().ToList();
            foreach (var token in sensorTokens)
            {
                m.AddSensor((int)token["id"], (double)token["t_init"]);
                System.Console.WriteLine($"Successfully added sensor {token["id"]} to the model. The sensor's intial temperature is {token["t_init"]}");
            }
        }

        private static Model GetModel(Material material, JToken settingsData)
        {
            var highTemp = (double)settingsData["high_temp"];
            var lowTemp = (double)settingsData["low_temp"];
            var simTime = (double)settingsData["sim_time"];

            System.Console.WriteLine($"Successfully created a model {highTemp} {lowTemp} {simTime}");

            return new Model(material, highTemp, lowTemp, simTime);
        }

        private static Material GetMaterial(JToken materialData)
        {
            var dData = GetDispersionData(materialData["d_data"]);
            var rData = GetRelaxationData(materialData["r_data"]);
            return new Material(dData, rData);
        }

        private static DispersionData GetDispersionData(JToken dData)
        {
            var wMaxLa = (double)dData["max_freq_la"];
            var wMaxTa = (double)dData["max_freq_ta"];
            var laData = dData["la_data"].ToObject<double[]>();
            var taData = dData["ta_data"].ToObject<double[]>();
            return new DispersionData(laData, wMaxLa, taData, wMaxTa);
        }

        private static RelaxationData GetRelaxationData(JToken rData)
        {
            var bl = (double)rData["b_l"];
            var btn = (double)rData["b_tn"];
            var btu = (double)rData["b_tu"];
            var bi = (double)rData["b_i"];
            var w = (double)rData["w"];
            return new RelaxationData(bl, btn, btu, bi, w);
        }
    }
}
