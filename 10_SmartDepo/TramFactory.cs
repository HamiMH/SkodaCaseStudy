namespace _10_SmartDepo
{
	/// <summary>
	/// Factory to create trams
	/// It must be nested to access private constructor of Tram
	/// </summary>
	public partial class Tram
	{
		public class TramFactory
		{
			private int _numberOfTrams;
			public Tram CreateTram(string name)
			{
				return new Tram(name, _numberOfTrams++);
			} 
			public Tram CreateTram(string name, RouteMission mission)
			{
				return new Tram(name, _numberOfTrams++,mission);
			}
		}
	}
}
