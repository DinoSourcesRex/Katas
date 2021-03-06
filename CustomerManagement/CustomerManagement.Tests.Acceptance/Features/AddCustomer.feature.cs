﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace CustomerManagement.Tests.Acceptance.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("AddCustomer")]
    [NUnit.Framework.CategoryAttribute("dbdependent")]
    public partial class AddCustomerFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AddCustomer.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "AddCustomer", "\tIn order to manage prospective and existing customer records\r\n\tAs an administrat" +
                    "or of the system\r\n\tI must be able to add a new user", ProgrammingLanguage.CSharp, new string[] {
                        "dbdependent"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "PreviouslyOrdered",
                        "WebCustomer",
                        "LastActive",
                        "FavouriteColours"});
            table1.AddRow(new string[] {
                        "Odin",
                        "False",
                        "False",
                        "02-02-2016",
                        "Red,Blue,Green"});
            table1.AddRow(new string[] {
                        "Thor",
                        "False",
                        "True",
                        "03-02-2016",
                        "Blue,Gold"});
            table1.AddRow(new string[] {
                        "Loki",
                        "False",
                        "False",
                        "01-01-2016",
                        "Black"});
            table1.AddRow(new string[] {
                        "Freya",
                        "True",
                        "True",
                        "12-30-2015",
                        "Yellow"});
            table1.AddRow(new string[] {
                        "Bor",
                        "False",
                        "False",
                        "04-03-2016",
                        "Brown"});
            table1.AddRow(new string[] {
                        "Heimdal",
                        "True",
                        "True",
                        "03-22-2016",
                        "Gold,Amber"});
            table1.AddRow(new string[] {
                        "Hel",
                        "True",
                        "False",
                        "04-04-2016",
                        "Red,Brown,Black"});
            table1.AddRow(new string[] {
                        "Sif",
                        "True",
                        "True",
                        "05-24-2015",
                        "Purple,Green"});
            table1.AddRow(new string[] {
                        "Thrud",
                        "False",
                        "True",
                        "04-23-2016",
                        "Pink,Yellow"});
            table1.AddRow(new string[] {
                        "Mani",
                        "True",
                        "True",
                        "01-05-2016",
                        "Red,Pink"});
            table1.AddRow(new string[] {
                        "Frigg",
                        "True",
                        "True",
                        "06-06-2015",
                        "Black,Brown"});
            table1.AddRow(new string[] {
                        "Eir",
                        "False",
                        "False",
                        "08-27-2016",
                        "Blue,Black"});
            table1.AddRow(new string[] {
                        "Hoenir",
                        "False",
                        "True",
                        "09-29-2016",
                        "White,Black,Grey"});
            table1.AddRow(new string[] {
                        "Lofn",
                        "False",
                        "False",
                        "10-17-2015",
                        "Grey,Purple"});
            table1.AddRow(new string[] {
                        "Skadi",
                        "False",
                        "False",
                        "11-19-2016",
                        "White,Gold"});
            table1.AddRow(new string[] {
                        "Vor",
                        "False",
                        "False",
                        "12-21-2016",
                        "Yellow,White"});
            table1.AddRow(new string[] {
                        "Ull",
                        "False",
                        "False",
                        "09-18-2015",
                        "Black,Brown"});
            table1.AddRow(new string[] {
                        "Tyr",
                        "True",
                        "True",
                        "11-11-2016",
                        "White,Yellow,Brown"});
            table1.AddRow(new string[] {
                        "Sol",
                        "False",
                        "True",
                        "05-14-2016",
                        "Green,Red,Blue"});
            table1.AddRow(new string[] {
                        "Nanna",
                        "False",
                        "True",
                        "09-08-2016",
                        "Green,Orange"});
            table1.AddRow(new string[] {
                        "Kvasir",
                        "True",
                        "True",
                        "11-09-2015",
                        "Yellow,Red"});
            table1.AddRow(new string[] {
                        "Ran",
                        "False",
                        "False",
                        "04-06-2016",
                        "Pink,Blue"});
            table1.AddRow(new string[] {
                        "Hodr",
                        "False",
                        "False",
                        "01-01-2015",
                        "White,Red"});
#line 8
 testRunner.Given("the following records in the database", ((string)(null)), table1, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "PreviouslyOrdered",
                        "WebCustomer",
                        "LastActive",
                        "FavouriteColours"});
            table2.AddRow(new string[] {
                        "Ragnar",
                        "False",
                        "True",
                        "01-01-2017",
                        "Red,Black,Green"});
#line 33
 testRunner.And("the following new customer", ((string)(null)), table2, "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a new customer is added are they on the list?")]
        public virtual void WhenANewCustomerIsAddedAreTheyOnTheList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a new customer is added are they on the list?", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 38
 testRunner.When("I add add new customer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.And("I make a request to get latest customers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.Then("I should see the new customer in the results", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a new customer is added is the user notified")]
        public virtual void WhenANewCustomerIsAddedIsTheUserNotified()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a new customer is added is the user notified", ((string[])(null)));
#line 42
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 43
 testRunner.When("I add add new customer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
 testRunner.And("I make a request to get latest customers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.Then("I should recieve a successful response", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
