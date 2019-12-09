using System;
using System.IO;


namespace PTSZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;
            string filePath;
            Instance instance;

            do {
                Console.Clear();

                option = DisplayMenu();

                Console.Clear();

                switch (option)
                {
                    case 1:
                        Directory.CreateDirectory("Instances");
                        Console.WriteLine( "Feature has been blocked for safety. Enter..." );
                        Console.ReadLine();
                        //Instance.GenerateSetToFiles();
                        break;
                    case 2:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        Directory.CreateDirectory("Results/List");
                        SolverGreedy.RunAndSave(instance, filePath.Replace(".txt", ".out.txt").Replace("Instances", "Results/List"));
                        break;
                    case 3:
                        Console.WriteLine("Not implemented yet. Enter...");
                        Console.ReadLine();
                        break;
                    case 4:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        Directory.CreateDirectory("Results/Dummy");
                        SolverDummy.RunAndSave(instance, filePath.Replace(".txt", ".out.txt").Replace("Instances", "Results/Dummy"));
                        break;
                    case 5:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        string filePathResults = FilesHelper.SelectFile("Results");
                        int delayTime;
                        bool isValid = Validator.Validate( instance, filePathResults, out delayTime );
                        Console.WriteLine(String.Format( "Validator resilt {0}! - delay is equal to {1}", isValid ? "PASS" : "FAILED", delayTime ));
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Not implemented yet. \n Enter to continue...");
                        Console.ReadLine();
                        break;
                    case 7:
                        Console.WriteLine("Not implemented yet. \n Enter to continue...");
                        Console.ReadLine();
                        break;
                    case 8:
                        Console.WriteLine("Not implemented yet. \n Enter to continue...");
                        Console.ReadLine();
                        break;

                }
            } while (option != 0);
        }

        static public int DisplayMenu()
        {
            Console.WriteLine("PTSZ Program");
            Console.WriteLine();
            Console.WriteLine("1. Generate data.");
            Console.WriteLine("2. Load and solve from file - greedy");
            Console.WriteLine("3. Load and solve from file - dummy");
            Console.WriteLine("4. Load and solve from file - heurestic");
            Console.WriteLine("5. Verify solution");
            Console.WriteLine("6. Run greedy for all files");
            Console.WriteLine("7. Run dummy for all files");
            Console.WriteLine("8. Run heurestic for all files");
            Console.WriteLine("0. Exit");
            string result = Console.ReadLine();

            return Convert.ToInt32(result);
        }
    }
}
