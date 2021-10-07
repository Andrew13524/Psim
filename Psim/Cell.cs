/* Lab Question (Test 2) 
 * 
 * Here we have used a List to hold the phonons. Given that we will need to remove phonons
 * from many different locations of the List (front, middle, back) do you think
 * this is an appropriate data structure to use? Keep in mind, we will also need repeatedly
 * iterate over the List and the List could contain many phonons. Random access is not required.
 * Justify your choice of a different data structure or explain why a List is a good choice.
 * 
 * Can you think of a clever way to remove an element from the middle of a List without having
 * to shift the memory contents of the List? 
 */

using System;
using System.Collections.Generic;

using Psim.Geometry2D;
using Psim.Particles;

namespace Psim.ModelComponents
{
	public enum SurfaceLocation
	{
		left = 0,
		top = 1,
		right = 2,
		bot = 3
	}

	public class Cell : Rectangle
	{
		private const int NUM_SURFACES = 4;
		private List<Phonon> phonons = new List<Phonon>();
		private List<Phonon> incomingPhonons = new List<Phonon>();
		private ISurface[] surfaces = new ISurface[NUM_SURFACES];
		public List<Phonon> Phonons { get { return phonons; } }

		public Cell(double length, double width)
			: base(length, width)
		{
			for (int i = 0; i < NUM_SURFACES; i++)
            {
				surfaces[i] = new BoundarySurface((SurfaceLocation)i, this);
            }
		}

		public void DriftPhonons(double time)
        {
			foreach(var p in phonons)
            {
				p.Drift(time);
            }
        }
		/// <summary>
		/// Adds a phonon to the main phonon 'array' of the cell.
		/// </summary>
		/// <param name="p">The phonon that will be added</param>
		public void AddPhonon(Phonon p)
		{
			phonons.Add(p);
		}

		/// <summary>
		/// Adds a phonon to the incoming phonon 'array' of the cell
		/// The incoming phonon will come from the phonons 'array' of another cell
		/// </summary>
		/// <param name="p">The phonon that will be added</param>
		public void AddIncPhonon(Phonon p)
		{
			incomingPhonons.Add(p);
		}

		/// <summary>
		/// Merges the incoming phonons with the existing phonons and clears the incoming phonons
		/// </summary>
		public void MergeIncPhonons()
		{
			phonons.AddRange(incomingPhonons);
			incomingPhonons.Clear();
		}

		/// <summary>
		/// Returns the surface at SurfaceLocation loc
		/// </summary>
		/// <param name="loc">The SurfaceLocation of the surface to be returned</param>
		/// <returns>The surface at location loc</returns>
		public ISurface GetSurface(SurfaceLocation loc)
		{
			return surfaces[(int)loc];
		}

		/// <summary>
		/// Moves a phonon to the surface that it will impact first.
		/// The phonon will be moved to the surface and the surface
		/// it impacts is returned
		/// </summary>
		/// <param name="p">The phonon to be moved</param>
		/// <returns>The surface that the phonon collides with or null if it doesnt impact</returns>
		public SurfaceLocation? MoveToNearestSurface(Phonon p)
		{
			SurfaceLocation? HitSurface(double p, double d, char axis) // Method that returns a Surface location if px or py come into contact with it
            {
				if(d > 0)
                {
					if (p >= Length && axis == 'x')
						return SurfaceLocation.right;
					else if (p >= Width && axis == 'y')
						return SurfaceLocation.top;
                }
				else if(d < 0)
                {
					if (p <= 0 && axis == 'x')
						return SurfaceLocation.left;
					else if (p <= 0 && axis == 'y')
						return SurfaceLocation.bot;
                }
				return null;
            }
			void UpdatePhonon(SurfaceLocation? hitSurface, double x, double y) // Method that updates phonons direction & coords upon hitting a side
            {
				if      (hitSurface == SurfaceLocation.top)   y = Width;
				else if (hitSurface == SurfaceLocation.bot)   y = 0;
				else if (hitSurface == SurfaceLocation.right) x = Length;
				else if (hitSurface == SurfaceLocation.left)  x = 0;

				p.SetCoords(x, y);

				BoundarySurface boundarySurface = new BoundarySurface(hitSurface, this);
				boundarySurface.HandlePhonon(p);
            }

			p.GetDirection(out double dx, out double dy);
			p.GetCoords(out double px, out double py);

			if (dx == 0 && dy == 0)
				return null;

			while (true) // Continusously add dx/dy to px/py until one side hits
            {
				SurfaceLocation? HitSurfaceX = HitSurface(px, dx, 'x');
				SurfaceLocation? HitSurfaceY = HitSurface(py, dy, 'y');

				if (HitSurfaceX != null || HitSurfaceY != null)
				{
					if (HitSurfaceX != null && HitSurfaceY != null) // If hits a corner, randomly select a side
					{
						Random rand = new Random();
						var randValue = rand.Next(0, 2);

						if (randValue == 0)
						{
							UpdatePhonon(HitSurfaceX, px, py);
							return HitSurfaceX;
						}
						else
						{
							UpdatePhonon(HitSurfaceY, px, py);
							return HitSurfaceY;
						}
					}
					else if (HitSurfaceX != null)
					{
						UpdatePhonon(HitSurfaceX, px, py);
						return HitSurfaceX;
					}
					else if (HitSurfaceY != null)
					{
						UpdatePhonon(HitSurfaceY, px, py);
						return HitSurfaceY;
					}
				}

				px += dx;
				py += dy;
			}
		}

		public override string ToString()
		{
			return string.Format("{0,-7} {1,-7}", phonons.Count, incomingPhonons.Count);
		}
	}
}
