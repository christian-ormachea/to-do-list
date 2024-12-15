public class Tarea {
    public string Nombre { get; set; }
    public bool Terminada { get; private set; }

    public Tarea(string unNombre)
    {
        if (string.IsNullOrEmpty(unNombre))
        {
            throw new ArgumentException("Ojo! El nombre de la tarea no puede ser vacio!");
        }
        Nombre = unNombre;
        Terminada = false;
    }

    public MarcarTerminada()
    {
        Terminada = true;
    }
}