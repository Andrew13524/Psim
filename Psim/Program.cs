using Psim.IOManagers;
using System.Collections.Generic;

namespace Psim
{
	class Program
	{
		static void Main(string[] args)
		{
			List<int> l = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            for (int i = l.Count-1; i < l.Count; i++)
            {
				if(l[i] % 2 == 1)
                {
					l[i] = l[l.Count - 1];
					l.RemoveAt(l.Count - 1);
                }
			}
            foreach (var item in l)
            {
				System.Console.WriteLine(item);
            }
			
			const string path = "../../../model.json";
			Model model = InputManager.InitializeModel(path);

			var watch = new System.Diagnostics.Stopwatch();
			watch.Start();

			model.RunSimulation("../../../");

			watch.Stop();
			System.Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds / 1000} [s]");
			
		}
	}
}
