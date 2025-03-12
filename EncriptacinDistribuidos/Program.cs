// See https://aka.ms/new-console-template for more information

using EncriptacinDistribuidos;
using EncriptacinDistribuidos.Algorithms;
using System.Diagnostics;


List<Reports> r = new List<Reports>();
List<IAlgorthm> algorthms = new List<IAlgorthm>();

MyRSA rsa = new MyRSA();
MyECC ecc = new MyECC();
MyPBE pbe = new MyPBE();

algorthms.Add(rsa);
algorthms.Add(ecc);
algorthms.Add(pbe);

//aes
//ecc
//etc



string content = ReadFile();
Console.WriteLine("Ingresa las iteraciones");

int iterations = int.Parse(Console.ReadLine());

foreach (var algorithm in algorthms)
{
    long totalTime = 0;
    Stopwatch swTotal = new Stopwatch();

    // Medición de memoria antes de la ejecución
    long totalMemoryBefore = GC.GetTotalMemory(true) / (1024 * 1024); // Convertir bytes a MB

    PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
    Thread.Sleep(500);

    float cpuUsageBefore = cpuCounter.NextValue();
    Thread.Sleep(500);
    cpuUsageBefore = cpuCounter.NextValue();

    for (int i = 0; i < iterations; i++)
    {
        swTotal.Restart();

        algorithm.ToEncrypt(content);

        swTotal.Stop();

        totalTime += swTotal.ElapsedMilliseconds;
    }

    float cpuUsageAfter = cpuCounter.NextValue();

    // Medición de memoria después de la ejecución
    long totalMemoryAfter = GC.GetTotalMemory(true) / (1024 * 1024); // Convertir bytes a MB

    double totalTimeSeconds = totalTime / 1000.0;

    r.Add(new Reports()
    {
        algorithmName = algorithm.GetName(),
        totalMemory = totalMemoryAfter - totalMemoryBefore, // MB
        totalTime = totalTimeSeconds, 
        beforeProcessor = cpuUsageBefore, // Porcentaje CPU antes
        afterProcessor = cpuUsageAfter // Porcentaje CPU después
    });
}



foreach (var report in r)
{
    Console.WriteLine("=========================================");
    Console.WriteLine($"Algoritmo: {report.algorithmName}");
    Console.WriteLine($"Memoria usada: {report.totalMemory} MB");
    Console.WriteLine($"Tiempo total: {report.totalTime:F3} s"); // 3 decimales de precisión
    Console.WriteLine($"CPU antes: {report.beforeProcessor:F2}%");
    Console.WriteLine($"CPU después: {report.afterProcessor:F2}%");
    Console.WriteLine("=========================================");
}



string ReadFile()
{
    string filePath = "./file.txt"; 

    try
    {
        string fileContent = File.ReadAllText(filePath);
        Console.WriteLine("Contenido del archivo:\n");
        Console.WriteLine(fileContent);
        return fileContent;
    }
    catch (Exception e)
    {
        Console.WriteLine($"Error al leer el archivo: {e.Message}");
        return "";
    }
}



Console.ReadLine();