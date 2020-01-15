using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values
{

    public event EventHandler OnValuesChanged;

    public static int STAT_MIN = 0;
    public static int STAT_MAX = 20;

    public enum Type
    {
        Sugar,
        CarbonFootprint,
        Fat,
        Carbohydrates,
        Salt,
    }

    private SingleValue sugarValue;
    private SingleValue carbonFootprintValue;
    private SingleValue fatValue;
    private SingleValue carbohydratesValue;
    private SingleValue saltValue;

    public Values(int sugar, int carbonFootprint, int fat, int carbohydrates, int salt)
    {
        sugarValue = new SingleValue(sugar);
        carbonFootprintValue = new SingleValue(carbonFootprint);
        fatValue = new SingleValue(fat);
        carbohydratesValue = new SingleValue(carbohydrates);
        saltValue = new SingleValue(salt);
    }


    private SingleValue getSingleValue(Type valueType)
    {
        switch (valueType)
        {
            default:
            case Type.Sugar: return sugarValue;
            case Type.CarbonFootprint: return carbonFootprintValue;
            case Type.Fat: return fatValue;
            case Type.Carbohydrates: return carbohydratesValue;
            case Type.Salt: return saltValue;
        }
    }

    public void setValue(Type valueType, int valueAmount)
    {
        getSingleValue(valueType).SetValueAmount(valueAmount);
        if (OnValuesChanged != null) OnValuesChanged(this, EventArgs.Empty);
    }

    public void increaseValueAmount(Type valueType)
    {
        setValue(valueType, getValueAmount(valueType) + 1);
    }

    public void decreaseValueAmount(Type valueType)
    {
        setValue(valueType, getValueAmount(valueType) - 1);
    }

    public int getValueAmount(Type valueType)
    {
        return getSingleValue(valueType).GetValueAmount();
    }

    public float getValueAmountNormalized(Type valueType)
    {
        return getSingleValue(valueType).GetValueAmountNormalized();
    }
   



    /*
     * Represents a Single Value of any Type
     * */
    private class SingleValue
    {

        private int value;

        public SingleValue(int valueAmount)
        {
            SetValueAmount(valueAmount);
        }

        public void SetValueAmount(int valueAmount)
        {
            value = Mathf.Clamp(valueAmount, STAT_MIN, STAT_MAX);
        }

        public int GetValueAmount()
        {
            return value;
        }

        public float GetValueAmountNormalized()
        {
            return (float)value / STAT_MAX;
        }

        public static implicit operator int(SingleValue v)
        {
            throw new NotImplementedException();
        }
    }
}
