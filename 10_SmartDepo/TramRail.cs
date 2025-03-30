using static _10_SmartDepo.Tram;

namespace _10_SmartDepo
{
	/// <summary>
	/// Represents a tram rail
	/// Trail is represented by two queues, one for trams with mission and one for trams without mission
	/// </summary>
	public class TramRail
	{

		private Queue<Tram> _withMission = new Queue<Tram>();
		private Queue<Tram> _withoutMission = new Queue<Tram>();
		private TramFactory _tramFactory;


		private Random _random = new Random();
		private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

		public TramRail(TramFactory tramFactory)
		{
			_tramFactory = tramFactory;
		}
		/// <summary>
		/// Initialize tram rail with trams
		/// </summary>
		/// <param name="numberOfTramsWithMission"></param>
		/// <param name="numberOfTramsWithoutMission"></param>
		public void InitializeTramRail(int numberOfTramsWithMission, int numberOfTramsWithoutMission)
		{
			_withMission.Clear();
			_withoutMission.Clear();
			for (int i = 0; i < numberOfTramsWithMission; i++)
			{
				_withMission.Enqueue(_tramFactory.CreateTram("Tram with mission", new RouteMission(_random.Next())));
			}
			for (int i = 0; i < numberOfTramsWithoutMission; i++)
			{
				_withoutMission.Enqueue(_tramFactory.CreateTram("Tram without mission"));
			}
		}
		/// <summary>
		/// Functioons for basic operations on tram rail
		/// </summary>
		private void AddNewTram()
		{
			_withoutMission.Enqueue(_tramFactory.CreateTram("Tram without mission"));

		}
		private bool SetMissionForTram(RouteMission routeMission)
		{
			if (_withoutMission.Count == 0)
			{
				return false;
			}

			Tram tram = _withoutMission.Dequeue();
			tram.SetMission(routeMission);
			_withMission.Enqueue(tram);
			return true;
		}
		private bool SendTram()
		{
			if (_withMission.Count == 0)
			{
				return false;
			}
			Tram tram = _withMission.Dequeue();
			return true;
		}

		/// <summary>
		/// Async functions for basic operations on tram rail
		/// </summary>
		/// <returns></returns>
		public async Task AddNewTramAsync()
		{
			await _semaphore.WaitAsync();
			try
			{
				AddNewTram();
			}
			finally
			{
				_semaphore.Release();
			}
		}
		public async Task AddNewTram5sAsync()
		{
			await _semaphore.WaitAsync();
			try
			{
				await Task.Delay(5000);
				AddNewTram();
			}
			finally
			{
				_semaphore.Release();
			}
		}

		public async Task<bool> SetMissionForTramAsync(RouteMission routeMission)
		{
			await _semaphore.WaitAsync();
			try
			{
				return SetMissionForTram(routeMission);
			}
			finally
			{
				_semaphore.Release();
			}
		}
		public async Task<bool> SetMissionForTram5sAsync(RouteMission routeMission)
		{
			await _semaphore.WaitAsync();
			try
			{
				await Task.Delay(5000);
				return SetMissionForTram(routeMission);
			}
			finally
			{
				_semaphore.Release();
			}
		}

		public async Task<bool> SendTramAsync()
		{
			await _semaphore.WaitAsync();
			try
			{
				return SendTram();
			}
			finally
			{
				_semaphore.Release();
			}
		}
		public async Task<bool> SendTram5sAsync()
		{
			await _semaphore.WaitAsync();
			try
			{
				await Task.Delay(5000);
				return SendTram();
			}
			finally
			{
				_semaphore.Release();
			}
		}

		/// <summary>
		/// Enumarotors for tram rail queues
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Tram> GetTramsWithMission()
		{
			return _withMission.AsEnumerable();
		}
		public IEnumerable<Tram> GetTramsWithoutMission()
		{
			return _withoutMission.AsEnumerable();
		}
		public IEnumerable<Tram> GetAllTrams()
		{
			return _withMission.Concat(_withoutMission);
		}
	}
}