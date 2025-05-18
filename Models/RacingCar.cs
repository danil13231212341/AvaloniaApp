using System;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.Models;

public class RacingCar : ObservableObject
{
    private int _id;
    private string _name;
    private int _distance;
    private string _status;
    private string _condition;
    private int _speed;
    private bool _isTireWornOut;
    private bool _isCrashed;
    private bool _isInPitStop;
    private bool _isRunning = true;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public int Distance
    {
        get => _distance;
        set => SetProperty(ref _distance, value);
    }

    public string Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }

    public string Condition
    {
        get => _condition;
        set => SetProperty(ref _condition, value);
    }

    public int Speed
    {
        get => _speed;
        set => SetProperty(ref _speed, value);
    }

    public bool IsTireWornOut
    {
        get => _isTireWornOut;
        set => SetProperty(ref _isTireWornOut, value);
    }

    public bool IsCrashed
    {
        get => _isCrashed;
        set => SetProperty(ref _isCrashed, value);
    }

    public bool IsInPitStop
    {
        get => _isInPitStop;
        set => SetProperty(ref _isInPitStop, value);
    }

    public event Action<RacingCar>? OnTireWornOut;
    public event Action<RacingCar>? OnCrash;

    private readonly Random _random = new();

    public RacingCar(int id, string name, int speed)
    {
        Id = id;
        Name = name;
        Speed = speed;
        Distance = 0;
        Status = "Едет по трассе";
        Condition = "Нормальное";
        IsTireWornOut = false;
        IsCrashed = false;
        IsInPitStop = false;
    }

    public void Drive()
    {
        while (_isRunning)
        {
            Thread.Sleep(3000);

            if (IsInPitStop || IsCrashed) continue;

            Distance += Speed;

            //проверка на износ покрышек (5% вероятность)
            if (_random.Next(1, 101) <= 5 && !IsTireWornOut)
            {
                IsTireWornOut = true;
                Condition = "Стерлись покрышки";
                Status = "Заехал в бокс";
                IsInPitStop = true;
                OnTireWornOut?.Invoke(this);
            }

            //проверка на аварию (2% вероятность)
            if (_random.Next(1, 101) <= 2)
            {
                IsCrashed = true;
                Condition = "Авария";
                Status = "Ожидает эвакуации";
                OnCrash?.Invoke(this);
            }
        }
    }

    public void Stop()
    {
        _isRunning = false;
    }
}
