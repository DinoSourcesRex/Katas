using System;
using Microsoft.SqlServer.Dac;
using NUnit.Framework;
using TechTalk.SpecFlow;

/*
 * If you are running NCrunch you cannot run tests using this repository in parallel - they must be run synchronously.
 */
namespace CustomerManagement.Tests.Acceptance
{
    [Binding]
    public static class DatabaseSetup
    {
        private static readonly DacServices DacServices = new DacServices("Data Source=(localdb)\\mssqllocaldb;Integrated Security=true");

        private static readonly DacPackage DacPackage = DacPackage.Load(
            $"{TestContext.CurrentContext.TestDirectory}\\Domain.Application.Database.dacpac");

        private static ScenarioRepository _scenarioRepository;

        [BeforeFeature]
        public static void Setup()
        {
            string dbName = $"SQL_Name_{Guid.NewGuid()}";

            DacServices.Deploy(DacPackage, dbName, true, new DacDeployOptions
            {
                CreateNewDatabase = true,
                VerifyDeployment = false,
                RegisterDataTierApplication = false
            });

            _scenarioRepository = new ScenarioRepository("(localdb)\\mssqllocaldb", dbName);
            Console.WriteLine($"Created Database: {dbName}");

            FeatureContext.Current.Add("Title", dbName);
        }

        [AfterFeature]
        public static void TearDown()
        {
            Console.WriteLine($"Database Deleting: {_scenarioRepository.DbName}");
            _scenarioRepository.DropDatabase();
            Console.WriteLine($"Database Deleted: {_scenarioRepository.DbName}");
        }
    }
}