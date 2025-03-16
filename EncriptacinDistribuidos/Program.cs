// See https://aka.ms/new-console-template for more information

using EncriptacinDistribuidos;
using EncriptacinDistribuidos.Algorithms;
using EncriptacinDistribuidos.Models;
using System.Diagnostics;
using System.Text;


List<Reports> r = new List<Reports>();
List<IAlgorthm> algorthms = new List<IAlgorthm>();

MyRSA rsa = new MyRSA();
MyECC ecc = new MyECC();
MyPBE pbe = new MyPBE();
MySHA sha = new MySHA();
MyAES aes = new MyAES();

algorthms.Add(rsa);
algorthms.Add(ecc);
algorthms.Add(pbe);
algorthms.Add(sha);
algorthms.Add(aes);

//aes
//ecc
//etc
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("Ingrese 1 para leer archivos, 2 para prueba de algoritmos 阿");

int type = int.Parse(Console.ReadLine());
if(type == 1)
{
    LetsDoIt();
}
else
{
    AlgorithmsGo();
}



void LetsDoIt()
{
    string filePath = "../../../../files/enero/InOutHorizontalReport_ATO_CBB_ENERO.txt"; 

    List<Employee> employees = new List<Employee>();
    List<Department> department = new List<Department>();
    List<Assistance> assistances = new List<Assistance>();

    try
    {
        string carpeta = "../../../../files/enero"; // Cambia esto por la ruta de tu carpeta

        string[] archivosTxt = Directory.GetFiles(carpeta, "*.txt");
       
        foreach (string archivo in archivosTxt)
        {

            string name = Path.GetFileName(archivo);
            Console.WriteLine($"Leyendo archivo: {name}");

            using (StreamReader reader = new StreamReader(carpeta+"/"+name))
            {
                string line;
                Employee employee = new Employee();
                while ((line = reader.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    var cells = line.Split(",");

                    if (cells[0].ToLower(). Contains("reporte") || cells[0] == "" || cells[0].ToLower().Contains("entrada"))
                    {
                        continue;
                    }

                    if (cells[0].ToLower().Contains("id"))
                    {
                        employee = new Employee()
                        {
                            Code = cells[1],
                            FullName = aes.EncryptAll( cells[4], cells[1]),
                            Department = aes.EncryptAll(cells[9], cells[1])
                        };

                        // department.Add();
                        //employee.Department = department;
                        employees.Add(employee);

                    }
                    else
                    {
                        Dictionary<int, int> dict = new Dictionary<int, int>()  {
                    { 0, 2 },
                    { 3, 4 },
                    { 5, 6 },
                    { 7, 9 }
                    };
                        foreach (var item in dict)
                        {
                            if (DateTime.TryParse(cells[item.Key], out DateTime dateTime))
                            {
                                Console.WriteLine("Fecha convertida: " + dateTime);
                                if (cells[item.Value] == "")
                                {
                                    cells[item.Value] = "Desconocido";
                                }

                                assistances.Add(new Assistance()
                                {
                                    Employee = employee,
                                    Type = cells[item.Value],
                                    Date = dateTime.Date,
                                    MarkDate = dateTime

                                });
                            }
                            else
                            {
                                Console.WriteLine("El formato de fecha no es válido." + cells[item.Key]);
                                continue;
                            }

                        }
                    }




                }
            }


        }
       



           ShowLists(employees, assistances);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        throw;
    }
}


void ShowLists(List<Employee> employees, List<Assistance> assistances)
{
    //foreach (var employee in employees)
    //{
    //    Console.WriteLine($"Empleado: {employee.FullName}");

    //    foreach (var assistance in assistances.Where(x => x.Employee == employee).ToList())
    //    {
    //        Console.WriteLine($"  - Fecha: {assistance.MarkDate}, Tipo: {assistance.Type}");
    //    }
    //}

    using (var context = new AppDbContext())
    {
        //  context.Database.EnsureCreated(); // Esto crea la BDD si no existe (ideal para pruebas)
        try
        {
            foreach (var employee in employees)
            {

                employee.RegistrationDate = employee.UpdateDate = DateTime.Now;
                employee.Status = 1;
                Employee employeeFInd =  context.Employee.Where(x => x.Code == employee.Code).FirstOrDefault();
                if(employeeFInd == null)
                {
                    context.Employee.Add(employee);
                    employeeFInd = employee;
                    context.SaveChanges();
                    Console.WriteLine("Empleado insertado!");

                }
                else
                {
                    Console.WriteLine("Solo updateo!");

                }


                foreach (var assistance in assistances.Where(x => x.Employee == employee).ToList())
                {
                    assistance.Employee = employeeFInd;
                    context.Add(assistance);
                    context.SaveChanges();
                    Console.Write("ASistencia insertada");
                    Console.WriteLine($"  - Fecha: {assistance.MarkDate}, Tipo: {assistance.Type}");
                }
            }
           
        }
        catch (Exception ex)
        {

            throw;
        }
      
    }
}
void AlgorithmsGo()
{

    string content = ReadFile();
    Console.WriteLine("Ingresa las iteraciones");

    int iterations = int.Parse(Console.ReadLine());

    foreach (var algorithm in algorthms)
    {
        long totalTime = 0;
        Stopwatch swTotal = new Stopwatch();

        // Medición de memoria antes de la ejecución
        long totalMemoryBefore = GC.GetTotalMemory(true); // Convertir bytes a MB

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
        long totalMemoryAfter = GC.GetTotalMemory(true); // Convertir bytes a MB

        double totalTimeSeconds = totalTime / 1000.0;

        r.Add(new Reports()
        {
            algorithmName = algorithm.GetName(),
            totalMemory = (totalMemoryAfter - totalMemoryBefore) / 1024, // MB
            totalTime = totalTimeSeconds,
            beforeProcessor = cpuUsageBefore, // Porcentaje CPU antes
            afterProcessor = cpuUsageAfter // Porcentaje CPU después
        });
    }



    foreach (var report in r.OrderBy(x => x.totalTime))
    {
        Console.WriteLine("=========================================");
        Console.WriteLine($"Algoritmo: {report.algorithmName}");
        Console.WriteLine($"Memoria usada: {report.totalMemory} KB");
        Console.WriteLine($"Tiempo total: {report.totalTime:F3} s"); // 3 decimales de precisión
        Console.WriteLine($"CPU antes: {report.beforeProcessor:F2}%");
        Console.WriteLine($"CPU después: {report.afterProcessor:F2}%");
        Console.WriteLine("=========================================");
    }

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