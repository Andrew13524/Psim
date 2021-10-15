using System;

using Psim.Particles;
using Psim.ModelComponents;
using Psim.Materials;

namespace Psim
{
	class Program
	{
		static void Main(string[] args)
		{
			DispersionData dData;
			dData.LaData = new double[] { -2.22e-7, 9260.0, 0.0 };
			dData.TaData = new double[] { -2.28e-7, 5240.0, 0.0 };
			dData.WMaxLa = 7.63916048e13;
			dData.WMaxTa = 3.0100793072e13;

			RelaxationData rData;
			rData.Bl = 1.3e-24;
			rData.Btn = 9e-13;
			rData.Btu = 1.9e-18;
			rData.BI = 1.2e-45;
			rData.W = 2.42e13;

			Material silicon = new Material(in dData, in rData);

			Sensor s1 = new Sensor(1, silicon, 300);
			Cell c1 = new Cell(10, 10, s1);
			Sensor s2 = new Sensor(2, silicon, 300);
			Cell c2 = new Cell(1, 1, s2);

			// Test transition surface (HandlePhonon)
			// Test emit surface (HandlePhonon)

			Console.WriteLine("\t\t***Testing Transition Surface (right)***");
			Phonon p1 = new Phonon(1);
			double px = 10;
			p1.SetCoords(px, 1);
			p1.SetDirection(0.5, 0.5);
			Console.WriteLine($"Position before {px}");
			TransitionSurface ts = new TransitionSurface(SurfaceLocation.right, c1);
			Cell c3 = ts.HandlePhonon(p1);
			p1.GetCoords(out px, out double py);
			Console.WriteLine($"Position after {px}");
			Console.WriteLine($"New cell: {c3}");

			Console.WriteLine("\t\t***Testing Transition Surface (top)***");
			py = 10;
			p1.SetCoords(1, py);
			p1.SetDirection(0.5, 0.5);
			Console.WriteLine($"Position before {py}");
			ts = new TransitionSurface(SurfaceLocation.top, c1);
			Cell c4 = ts.HandlePhonon(p1);
			p1.GetCoords(out px, out py);
			Console.WriteLine($"Position after {py}");
			Console.WriteLine($"New cell: {c4}");

			Console.WriteLine("\t\t***Testing Transition Surface (left)***");
			px = 0;
			p1.SetCoords(px, 1);
			p1.SetDirection(0.5, 0.5);
			Console.WriteLine($"Position before {px}");
			ts = new TransitionSurface(SurfaceLocation.left, c1);
			Cell c5 = ts.HandlePhonon(p1);
			p1.GetCoords(out px, out py);
			Console.WriteLine($"Position after {px}");
			Console.WriteLine($"New cell: {c5}");

			Console.WriteLine("\t\t***Testing Transition Surface (bot)***");
			py = 0;
			p1.SetCoords(1, py);
			p1.SetDirection(0.5, 0.5);
			Console.WriteLine($"Position before {py}");
			ts = new TransitionSurface(SurfaceLocation.bot, c1);
			Cell c6 = ts.HandlePhonon(p1);
			p1.GetCoords(out px, out py);
			Console.WriteLine($"Position after {py}");
			Console.WriteLine($"New cell: {c6}");

			// Test emit surface (HandlePhonon)
			Console.WriteLine("***\n\n\t\t***Testing Emit Surface (right)***");
			Phonon p2 = new Phonon(1);
			p2.DriftTime = 10;
			Console.WriteLine("Phonon properties prior to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
			EmitSurface es = new EmitSurface(SurfaceLocation.right, c1, 300);
			es.HandlePhonon(p2);
			Console.WriteLine("Phonon properties after to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");

			Console.WriteLine("\t\t***Testing Emit Surface (top)***");
			p2 = new Phonon(1);
			p2.DriftTime = 10;
			Console.WriteLine("Phonon properties prior to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
			es = new EmitSurface(SurfaceLocation.top, c1, 300);
			es.HandlePhonon(p2);
			Console.WriteLine("Phonon properties after to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");

			Console.WriteLine("\t\t***Testing Emit Surface (left)***");
			p2 = new Phonon(1);
			p2.DriftTime = 10;
			Console.WriteLine("Phonon properties prior to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
			es = new EmitSurface(SurfaceLocation.left, c1, 300);
			es.HandlePhonon(p2);
			Console.WriteLine("Phonon properties after to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");

			Console.WriteLine("\t\t***Testing Emit Surface (bot)***");
			p2 = new Phonon(1);
			p2.DriftTime = 10;
			Console.WriteLine("Phonon properties prior to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
			es = new EmitSurface(SurfaceLocation.bot, c1, 300);
			es.HandlePhonon(p2);
			Console.WriteLine("Phonon properties after to emit surface collision");
			Console.WriteLine($"Active: {p2.Active}");
			Console.WriteLine($"Drift Time: {p2.DriftTime}");
		}
	}
}
