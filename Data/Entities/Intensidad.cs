using System;
using System.ComponentModel.DataAnnotations;

public class Intensidad
{
	public int Id { get; set; }
    [Range(1, 5)]
    public int Grado { get; set; }
	

}
