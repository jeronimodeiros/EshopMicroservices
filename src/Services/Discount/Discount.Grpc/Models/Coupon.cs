namespace Discount.Grpc.Models;

public class Coupon
{
    public int Id { get; set; }
    public string ProductName { get; set; } = default!;
    /*  = default!; le dice que tenga la variable su valor por default que es, 
     *  en el caso de los string, el Null, si fuera bool, su default es false, si fuera int,
     *  su default es 0, etc. y el ! se introdujo en C#8 y le dice al compilador, que,
     *  a pesar de ser una variable no nuleable, el desarrollador está seguro de que tendrá 
     *  un valor no nulo en tiempo de ejecución. Esto suprime cualquier advertencia de 
     *  nulabilidad que el compilador podría generar. 
     */
    public string Description { get; set; } = default!;
    public int Amount { get; set; }
}

