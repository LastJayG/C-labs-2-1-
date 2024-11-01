public class Passenger
{
    public string name { get; set; } = "Undefined";
    public bool withBenefit { get; set; }
    public Passenger() { }
    public Passenger(string Name, bool WithBenefit)
    {
        name = Name;
        withBenefit = WithBenefit;
    }

    public override bool Equals(object obj)
    {
        if (obj is Passenger other)
        {
            return name == other.name && withBenefit == other.withBenefit;
        }
        return false;
    }

}