using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.Models;

public interface ILoader
{
    int Id { get; }
    string Status { get; }
    int? EvacuatedCarId { get; }
    bool HasWorked { get; }
    void Load(RacingCar car);
}

public class Loader : ObservableObject, ILoader
{
    private int _id;
    private string _status;
    private int? _evacuatedCarId;
    private bool _hasWorked;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public int? EvacuatedCarId
    {
        get => _evacuatedCarId;
        set => SetProperty(ref _evacuatedCarId, value);
    }

    public bool HasWorked
    {
        get => _hasWorked;
        set => SetProperty(ref _hasWorked, value);
    }

    public Loader(int id)
    {
        Id = id;
        Status = "Ожидает";
        EvacuatedCarId = null;
        HasWorked = false;
    }

    public void Load(RacingCar car)
    {
        if (HasWorked) return;

        Status = "Эвакуирует";
        EvacuatedCarId = car.Id;
        HasWorked = true;

        //имитация времени эвакуации
        Thread.Sleep(9000);

        car.IsCrashed = false;
        car.Condition = "Эвакуирован";
        car.Status = "Снят с трассы";
        car.Stop();

        Status = "Закончил работу";
    }
}
