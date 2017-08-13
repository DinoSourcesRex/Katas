using System;
using System.IO;
using Microsoft.SqlServer.Dac;
using NCrunch.Framework;
using TechTalk.SpecFlow;

/*
 * If you are running NCrunch you cannot run tests using this repository in parallel - they must be run synchronously.
 */
namespace CustomerManagement.Tests.Acceptance
{
    [Binding]
    public static class DatabaseSetup
    {
        public static ScenarioRepository ScenarioRepository;

        private static readonly DacServices DacServices = new DacServices("Data Source=(localdb)\\mssqllocaldb;Integrated Security=true");

        private static DacPackage GetDacPackage(string packageName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            int removeAtIndex = basePath.IndexOf($"\\{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}");
            basePath = $"{basePath.Remove(removeAtIndex)}\\dacpac";

            if (NCrunchEnvironment.NCrunchIsResident())
            {
                basePath = Path.GetDirectoryName(NCrunchEnvironment.GetOriginalSolutionPath()) + $"\\dacpac";
            }

            return DacPackage.Load($"{basePath}\\{packageName}.dacpac");
        }

        [BeforeFeature("dbdependent")]
        public static void Setup()
        {
            string dbName = $"SQL_Name_{DateTime.Now:yyyyMMdd HH:mm:ss.FFF}";
            string dacPackName = "CustomerManagement.Database";

            //There is a post-build event on the database project that pulls the dacpack into the desired place
            DacServices.Deploy(GetDacPackage(dacPackName), dbName, true, new DacDeployOptions
            {
                CreateNewDatabase = true,
                VerifyDeployment = false,
                RegisterDataTierApplication = false
            });

            ScenarioRepository = new ScenarioRepository("(localdb)\\mssqllocaldb", dbName);
            Console.WriteLine($"Created Database: {dbName}");

            FeatureContext.Current.Add("Title", dbName);
        }

        [AfterFeature("dbdependent")]
        public static void TearDown()
        {
            Console.WriteLine($"Database Deleting: {ScenarioRepository.DbName}");
            ScenarioRepository.DropDatabase();
            Console.WriteLine($"Database Deleted: {ScenarioRepository.DbName}");
        }

        public static void ClearDate()
        {
            Console.WriteLine($"Truncating Transactions for: {ScenarioRepository.DbName}");
            ScenarioRepository.ClearTables();
            Console.WriteLine($"Transactions Truncared for: {ScenarioRepository.DbName}");
        }
    }
}