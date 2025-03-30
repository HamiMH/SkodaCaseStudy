namespace _10_SmartDepo;

/// <summary>
/// Tram representation
/// /// </summary>
public partial class Tram
{
	//Tram should be created by TramFactory
	private Tram(string name,int id)
	{
		Name = name;
		Id = id;
		HasMission= false;
	} 
	private Tram(string name,int id, RouteMission routeMission)
	{
		Name = name;
		Id = id;
		HasMission= true;
		Mission = routeMission;
	}
	public string Name { get;  }
	public int Id { get;  }

	public bool HasMission { get; private set; }
	public RouteMission? Mission { get; private set; }	

	public void SetMission(RouteMission routeMission)
	{
		HasMission = true;
		Mission = routeMission;
	}
	public void RemoveMission()
	{
		HasMission = false;
		Mission = null;
	}
}



