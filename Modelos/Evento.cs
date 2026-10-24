
namespace Planazo.Modelos;

public abstract class Evento
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public DateTime Fecha { get; private set; }
    public decimal PrecioBase { get; private set; }
    public int CapacidadMaxima { get; private set; }
    public int CuposDisponibles { get; private set; }

    protected Evento(
        int id,
        string nombre,
        DateTime fecha,
        decimal precioBase,
        int capacidadMaxima)
    {
        if (id <= 0)
            throw new ArgumentException("El ID debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (precioBase < 0)
            throw new ArgumentException("El precio no puede ser negativo.");

        if (capacidadMaxima <= 0)
            throw new ArgumentException("La capacidad debe ser mayor que cero.");

        Id = id;
        Nombre = nombre;
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public void ReservarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        if (cantidad > CuposDisponibles)
            throw new InvalidOperationException("No hay suficientes cupos disponibles.");

        CuposDisponibles -= cantidad;
    }

    public void LiberarCupos(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor que cero.");

        if (CuposDisponibles + cantidad > CapacidadMaxima)
            throw new InvalidOperationException("No se pueden liberar mas cupos de los permitidos.");

        CuposDisponibles += cantidad;
    }

    public override string ToString()
    {
        return $"ID: {Id} | {Nombre} | Fecha: {Fecha:dd/MM/yyyy} | " +
               $"Precio: {CalcularPrecioFinal():C} | " +
               $"Disponibles: {CuposDisponibles}/{CapacidadMaxima}";
    }
}
