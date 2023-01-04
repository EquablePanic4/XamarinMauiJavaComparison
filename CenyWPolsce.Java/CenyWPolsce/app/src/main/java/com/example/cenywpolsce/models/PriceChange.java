package com.example.cenywpolsce.models;

public class PriceChange {
    public PriceChange()
    {

    }

    public PriceChange(String id, String nazwa, double startPrice, double endPrice)
    {
        ID = id;
        Nazwa = nazwa;
        StartPrice = startPrice;
        EndPrice = endPrice;
    }

    public String ID;
    public String Nazwa;
    public double StartPrice;
    public double EndPrice;

    private double _change;
    private boolean _changeCalculated = false;
    public double Change() {
        if (_changeCalculated == false)
        {
            _change = ((EndPrice - StartPrice) / StartPrice) * 100;
            _changeCalculated = true;
        }

        return _change;
    }
}
