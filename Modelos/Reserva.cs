
namespace Planazo.Modelos;

public class Reserva
{
    public int Id { get; private set; }
    public Usuario Usuario { get; private set; }
    public Evento Evento { get; private set; }
    public int CantidadEntradas { get; private set; }
    public decimal ValorTotal { get; private set; }
    public bool Activa { get; private set; }

    public string Estado => Activa ? "Activa" : "Cancelada";

    public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
    {
        if (id <= 0)
            throw new ArgumentException("El ID de la reserva debe ser mayor que cero.");

        if (usuario == null)
            throw new ArgumentException("El usuario es obligatorio.");

        if (evento == null)
            throw new ArgumentException("El evento es obligatorio.");

        if (cantidadEntradas <= 0)
            throw new ArgumentException("Debes reservar al menos una entrada.");

        Id = id;
        Usuario = usuario;
        Evento = evento;
        CantidadEntradas = cantidadEntradas;
        ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
        Activa = true;
    }

    public void Cancelar()
    {
        if (!Activa)
            throw new InvalidOperationException("Esta reserva ya fue cancelada.");

        Evento.LiberarCupos(CantidadEntradas);
        Activa = false;
    }

    public override string ToString()
    {
        return $"Reserva #{Id} | Usuario: {Usuario.Nombre} | " +
               $"Evento: {Evento.Nombre} | Entradas: {CantidadEntradas} | " +
               $"Total: {ValorTotal:C} | Estado: {Estado}";
    }
}
