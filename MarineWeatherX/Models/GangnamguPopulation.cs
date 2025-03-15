using System;
using System.Collections.Generic;

namespace MarineWeatherX.Models;

public partial class GangnamguPopulation
{
    public string? AdministrativeAgency { get; set; }

    public int? TotalPopulation { get; set; }

    public int? MalePopulation { get; set; }

    public int? FemalePopulation { get; set; }

    public double? SexRatio { get; set; }

    public int? NumberOfHouseholds { get; set; }

    public double? NumberOfPeoplePerHousehold { get; set; }

    public int Id { get; set; }
}
