using System;
using System.Threading;
using System.Threading.Tasks;

namespace PrimerEjemploSemaforo
{
    class Program
    {
        const int cantidadMaximaTareas = 4;
        const int cantidadHilos = 25;
        //Crea un hilos sin ningun recurso libre
        private static Semaphore semaforo = new Semaphore(0, cantidadMaximaTareas);
        private static int contador = 0;

        static void Main(string[] args)
        {
            Task[] tareas = new Task[cantidadHilos];
            for (int i = 0; i < cantidadHilos; i++)
            {
                tareas[i] = new Task(()=>ImprimirTask());
                tareas[i].Start();
            }
            Console.WriteLine($"Liberando {cantidadMaximaTareas} recursos");
            semaforo.Release(cantidadMaximaTareas);
            //Detiene la ejecución del programa hasta que todos los Task terminen.
            Task.WaitAll(tareas);
            Console.WriteLine("Fin ejecución de hilos, presione una tecla para continuar");
            Console.ReadKey();
        }

        static void ImprimirTask()
        {            
            semaforo.WaitOne();
            Console.WriteLine($"Contador: {++contador}");
            Thread.Sleep(1000);
            semaforo.Release();
        }
    }
}
