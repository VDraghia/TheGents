using ProjectManagementCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementCollection.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PmcAppDbContext context)
        {
            context.Database.EnsureCreated();


            /*
             * Create User Data
             */
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User {FirstName="Prof1FirstName", LastName="Prof1LastName", Email="prof1@school.com" },
                    new User {FirstName="Prof2FirstName", LastName="Prof2LastName", Email="prof2@school.com" },
                    new User {FirstName="Student1FirstName", LastName="Student1LastName", Email="student3@school.com" }
                };

                foreach (User user in users) {
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }


            /*
             * Create Project Data
             */
            if (!context.Projects.Any())
            {
                var projects = new Project[]
                {
                            new Project {Name="Project1", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2016,1,1), Client="client1", Location="loc1", Success=true},
                            new Project {Name="Project2", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2018,1,1), Client="client1", Location="loc2", Success=false},
                            new Project {Name="Project3", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2019,1,1), Client="client2", Location="loc1", Success=false},
                };

                foreach (Project project in projects)
                {
                    context.Projects.Add(project);
                }

                context.SaveChanges();
            }
            //          {
            //                new Document {FirstName="Prof1FirstName", LastName="Prof1LastName", Email="prof1@school.com" },
            //                new Document { FirstName="Prof2FirstName", LastName="Prof2LastName", Email="prof2@school.com" },
            //                new Document { FirstName="Student1FirstName", LastName="Student1LastName", Email="student3@school.com" }
            //};

            List<FactorMainCategory> mainCategories = new List<FactorMainCategory>();

            if (!context.FactorMainCategories.Any())
            {

                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Generic" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Scope Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Requirements Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Schedule Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Cost Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Quality Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Resource Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Communications Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Procurement Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Stakeholder Engagement Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Change Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Configuration Management Plan" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Scope Baseline" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Cost Baseline" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Schedule Baseline" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Performance Measurement Baseline" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Project Lifecycle Analysis" });
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "Project Development Approach" });

                foreach (FactorMainCategory mainCategory in mainCategories)
                {
                    context.FactorMainCategories.Add(mainCategory);
                }
                context.SaveChanges();
            }

            List<FactorSubCategory> subCategories = new List<FactorSubCategory>();

            if (!context.FactorSubCategories.Any())
            {

                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 1, FactorSubCategoryDesc = "Activity attributes" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 2, FactorSubCategoryDesc = "Activity list" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 3, FactorSubCategoryDesc = "Assumption log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 4, FactorSubCategoryDesc = "Basis of estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 5, FactorSubCategoryDesc = "Change log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 6, FactorSubCategoryDesc = "Cost estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 7, FactorSubCategoryDesc = "Cost forecasts" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 8, FactorSubCategoryDesc = "Duration estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 9, FactorSubCategoryDesc = "Issue log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 10, FactorSubCategoryDesc = "Lessons learned register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 11, FactorSubCategoryDesc = "Milestone list" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 12, FactorSubCategoryDesc = "Physical resource assignments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 13, FactorSubCategoryDesc = "Project calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 14, FactorSubCategoryDesc = "Project communication" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 15, FactorSubCategoryDesc = "Project schedule" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 16, FactorSubCategoryDesc = "Project schedule network diagram" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 17, FactorSubCategoryDesc = "Project scope statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 18, FactorSubCategoryDesc = "Project team assignments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 19, FactorSubCategoryDesc = "Quality control measurements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 20, FactorSubCategoryDesc = "Quality metrics" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 21, FactorSubCategoryDesc = "Quality report" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 22, FactorSubCategoryDesc = "Requirements documentation" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 23, FactorSubCategoryDesc = "Requirements traceability matrix" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 24, FactorSubCategoryDesc = "Resource breakdown structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 25, FactorSubCategoryDesc = "Resource calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 26, FactorSubCategoryDesc = "Resource requirements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 27, FactorSubCategoryDesc = "Risk register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 28, FactorSubCategoryDesc = "Risk report" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 29, FactorSubCategoryDesc = "Schedule data" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 30, FactorSubCategoryDesc = "Schedule dataDup" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 31, FactorSubCategoryDesc = "Stakeholder register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 32, FactorSubCategoryDesc = "Team charter" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 33, FactorSubCategoryDesc = "Test and evaluation documents" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 34, FactorSubCategoryDesc = "Scope Statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 35, FactorSubCategoryDesc = "Work Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 36, FactorSubCategoryDesc = "Definition of Scope" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 37, FactorSubCategoryDesc = "Logging Requirement Activities" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 38, FactorSubCategoryDesc = "Requirements Change Requests" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 39, FactorSubCategoryDesc = "Developing a Requirements Traceability Matrix" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 40, FactorSubCategoryDesc = "Milestones List" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 41, FactorSubCategoryDesc = "Activity Sequencing" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 42, FactorSubCategoryDesc = "Schedule Baseline" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 43, FactorSubCategoryDesc = "Basis of Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 44, FactorSubCategoryDesc = "Budget Breakdown" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 45, FactorSubCategoryDesc = "Cost Baseline" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 46, FactorSubCategoryDesc = "Establish Quality Metrics" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 47, FactorSubCategoryDesc = "Plan Quality Control" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 48, FactorSubCategoryDesc = "Plan Quality Assurance" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 49, FactorSubCategoryDesc = "Resource Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 50, FactorSubCategoryDesc = "Resource Tracking" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 51, FactorSubCategoryDesc = "Resource Acquisition Plan" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 52, FactorSubCategoryDesc = "Resource Calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 53, FactorSubCategoryDesc = "Communication Schedule" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 54, FactorSubCategoryDesc = "Establish Communication Methods" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 55, FactorSubCategoryDesc = "Stakeholder Communications Requirements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 56, FactorSubCategoryDesc = "Developing a Statement of Work" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 57, FactorSubCategoryDesc = "Contracting Terms and Conditions" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 58, FactorSubCategoryDesc = "Deliverables List" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 59, FactorSubCategoryDesc = "Stakeholder Register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 60, FactorSubCategoryDesc = "Stakeholder Communication Plan" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 61, FactorSubCategoryDesc = "Stakeholder Power Models(Salience); Power/Influence); etc)" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 62, FactorSubCategoryDesc = "Stakeholder Categorization" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 63, FactorSubCategoryDesc = "Standardized Change Process" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 64, FactorSubCategoryDesc = "Impact Assessments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 65, FactorSubCategoryDesc = "Change Log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 66, FactorSubCategoryDesc = "Requirements Change Requests" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 67, FactorSubCategoryDesc = "Standardized Configuration Controls" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 68, FactorSubCategoryDesc = "Item Logging" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 69, FactorSubCategoryDesc = "Resource Allocations" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 70, FactorSubCategoryDesc = "Project Scope Statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 71, FactorSubCategoryDesc = "Work Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 72, FactorSubCategoryDesc = "Scope Change Log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 73, FactorSubCategoryDesc = "Activity Cost Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 74, FactorSubCategoryDesc = "Resource Cost Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 75, FactorSubCategoryDesc = "Work Package Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 76, FactorSubCategoryDesc = "Gantt Chart" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 77, FactorSubCategoryDesc = "Milestone Chart" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 78, FactorSubCategoryDesc = "Resource Allocations" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 79, FactorSubCategoryDesc = "Scope); Cost); Schedule Baselines" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 80, FactorSubCategoryDesc = "Resource Performance Reports" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 81, FactorSubCategoryDesc = "Percent Complete Analysis" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 82, FactorSubCategoryDesc = "Life Cycle Description" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 83, FactorSubCategoryDesc = "Framework for Project Lifecycle" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 84, FactorSubCategoryDesc = "Project Influences" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 85, FactorSubCategoryDesc = "Agile" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 86, FactorSubCategoryDesc = "Waterfall" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryId = 87, FactorSubCategoryDesc = "Hybrid" });

                foreach (FactorSubCategory subCategory in subCategories)
                {
                    context.FactorSubCategories.Add(subCategory);
                }

                context.SaveChanges();
            }

            List<Factor> factors = new List<Factor>();

            if (!context.Factors.Any())
            {

                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Activity attributes").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Activity list").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Assumption log").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Basis of estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Change log").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Cost estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Cost forecasts").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Duration estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Issue log").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Lessons learned register").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Milestone list").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Physical resource assignments").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project calendars").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project communication").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project schedule").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project schedule network diagram").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project scope statement").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project team assignments").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Quality control measurements").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Quality metrics").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Quality report").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Requirements documentation").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Requirements traceability matrix").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource breakdown structure").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource calendars").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource requirements").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Risk register").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Risk report").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Schedule data").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Schedule dataDup").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder register").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Team charter").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Generic").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Test and evaluation documents").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Scope Statement").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Definition of Scope").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Logging Requirement Activities").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Developing a Requirements Traceability Matrix").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Milestones List").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Activity Sequencing").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Schedule Baseline").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Basis of Estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Budget Breakdown").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Cost Baseline").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Establish Quality Metrics").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Plan Quality Control").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Plan Quality Assurance").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Breakdown Structure").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Tracking").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Acquisition Plan").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Calendars").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Communication Schedule").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Establish Communication Methods").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder Communications Requirements").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Developing a Statement of Work").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Contracting Terms and Conditions").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Deliverables List").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder Register").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder Communication Plan").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder Power Models(Salience, Power/Influence, etc)").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Stakeholder Categorization").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Standardized Change Process").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Impact Assessments").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Change Log").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Standardized Configuration Controls").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Item Logging").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project Scope Statement").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Scope Change Log").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Activity Cost Estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Cost Estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Work Package Estimates").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Gantt Chart").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Milestone Chart").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Scope, Cost, Schedule Baselines").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Resource Performance Reports").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Percent Complete Analysis").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Life Cycle Description").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Framework for Project Lifecycle").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Project Influences").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Agile").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Waterfall").FactorSubCategoryId
                });
                factors.Add( new Factor {
                    FactorMainCategoryFk = mainCategories.Single(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = subCategories.Single(c => c.FactorSubCategoryDesc == "Hybrid").FactorSubCategoryId
                });

                foreach (var factor in factors)
                {
                    context.Factors.Add(factor);
                }
                context.SaveChanges();
            }

            List<Document> documents = new List<Document>();

            if (!context.Documents.Any())
            {

                documents.Add(new Document { Name = "Doc1", Url = "doc1.aws.amazon.com", ProjectFk = Int32.Parse(context.Projects.Where(c => c.ProjectId == 1).Single().ToString())});
                documents.Add(new Document {Name="Doc2", Url="doc2.aws.amazon.com", ProjectFk = Int32.Parse(context.Projects.Where(c => c.ProjectId == 1).Single().ToString())});
                documents.Add(new Document {Name="Doc3", Url="doc3.aws.amazon.com", ProjectFk = Int32.Parse(context.Projects.Where(c => c.ProjectId == 2).Single().ToString())});

                foreach (Document document in documents)
                {
                    context.Documents.Add(document);
                }

                context.SaveChanges();
            }

        }
    }
}
