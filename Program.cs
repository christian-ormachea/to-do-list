using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program {
    public static List<Tarea> tareasExistentes = new List<Tarea>();

    public static void Main(string[] args){
        Console.WriteLine("Hola que tal, bienvenido al to-do list de Ormachea Christian, para comenzar ingrese la contraseña");
        string contraseniaIngresada = Console.ReadLine();         
    VerificarContrasenia(contraseniaIngresada);
    }

    public static void VerificarContrasenia(string contrasenia){
        string contra = "contra.json";
        if(!File.Exists(contra)){
            CrearContrasenia();
        } else if(contrasenia == ContenidoDeContra()){
            ProgramaPrincipal();
        } else {
            Console.WriteLine("Errrorrrr! Contrasenia incorrecta!");
        }
    }

    public static string ContenidoDeContra(){
        try
        {
            string jsonString = File.ReadAllText("contra.json");
            string contenido = JsonSerializer.Deserialize<string>(jsonString);
            return contenido;
        }
        catch
        {
            Console.WriteLine("Error grave al leer la contrasenia");
        }
    }

    public static void CrearContrasenia(){
        Console.WriteLine("Veo que es tu primera vez aqui! Porfavor ingrese una contraseña para crear un to-do list!!");

        string contraNueva = Console.ReadLine();

        var contraEnJson = JsonSerializer.Serialize(contraNueva);
        File.WriteAllText("contra.json", contraEnJson);

        Console.WriteLine("Contrasenia creada exitosamente!");
    }

    public static void CreadorDeTareas(List<Tarea> tareasExistentes)
    {
        if (File.Exists("Tarea.json"))
        {
            string contenidoJson = File.ReadAllText("Tarea.json");
            if (!string.IsNullOrEmpty(contenidoJson))
            {
                tareasExistentes = JsonSerializer.Deserialize<List<Tarea>>(contenidoJson);
            }
            else
            {
                tareasExistentes = new List<Tarea>();
            }
        }
        else
        {
            tareasExistentes = new List<Tarea>();
        }
        Console.WriteLine("Ingrese el nombre de la tarea nueva: ");
        string nombreDeTarea = Console.ReadLine();
        Tarea tareaNueva = new Tarea(nombreDeTarea);

        // Agregar la nueva tarea a la lista
        tareasExistentes.Add(tareaNueva);

        // Sobrescribir el archivo con la lista actualizada
        var tareasActualizadasEnJson = JsonSerializer.Serialize(tareasExistentes, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("Tarea.json", tareasActualizadasEnJson);

        Console.WriteLine("Tarea creada exitosamente.");
    }

    public static void VerListaDeTareas(List<Tarea> tareasExistentes)
    {
        foreach (var tarea in tareasExistentes)
        {
            Console.WriteLine($"tarea: {tarea.Nombre} terminada: {tarea.Terminada}");
        }
    }


    public static void ProgramaPrincipal(){
        int opcion = 0;        
        do 
        {
            Console.WriteLine("Bienvenido! Que desea hacer a continuacion? ");
            Console.WriteLine("1. Agregar una tarea nueva.");
            Console.WriteLine("2. Ver la lista de tareas. ");
            Console.WriteLine("3. Marcar como hecha una tarea.");
            Console.WriteLine("4. Salir");
            opcion = int.Parse(Console.ReadLine());            
            switch (opcion)
            {
                case 1:
                    CreadorDeTareas(tareasExistentes);
                    break;
                case 2:
                    VerListaDeTareas(tareasExistentes);               
                    break;
                case 3:
                    Console.WriteLine("Ingrese la tarea que desea marcar como terminada.");
                    string tareaTerminada = Console.ReadLine();
                    foreach (var tarea in tareasExistentes)
                    {
                        if (tareaTerminada == tarea.Nombre)
                        {
                            tarea.MarcarComoTerminada();
                        }
                    }                 
                    break;

                default:
                    Console.WriteLine("Error, elija una opcion valida!");
                    break;
            }
        } while(opcion != 4); 
        
        
        
    }
}
