using Microsoft.EntityFrameworkCore;
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
            context.Database.EnsureDeleted();
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
                    new Project {Name="Project1", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2016,1,1), Client="client1", Location="loc1", Success="Yes", Uploader_id=1},
                    new Project {Name="Project2", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2018,1,1), Client="client1", Location="loc2", Success="No", Uploader_id=1 },
                    new Project {Name="Project3", Uploaded= new DateTime(2020,1,1), DateCompleted=new DateTime(2019,1,1), Client="client2", Location="loc1", Success="Yes", Uploader_id=2},
                };

                foreach (Project project in projects)
                {
                    context.Projects.Add(project);
                }

                context.SaveChanges();
            }




            List<FactorMainCategory> mainCategories = new List<FactorMainCategory>();

            if (!context.FactorMainCategories.Any())
            {
                mainCategories.Add(new FactorMainCategory { FactorMainCategoryDesc = "none" });
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
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Activity attributes" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Activity list" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Assumption log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Basis of Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Change log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Cost estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Cost forecasts" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Duration estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Issue log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Lessons learned register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Milestone list" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Physical resource assignments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project communication" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project schedule" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project schedule network diagram" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project scope statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project team assignments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Quality control measurements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Quality metrics" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Quality report" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Requirements documentation" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Requirements traceability matrix" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource breakdown structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource requirements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Risk register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Risk report" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Schedule data" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Schedule forecasts" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Team charter" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Test and evaluation documents" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Scope Statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Work Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Definition of Scope" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Logging Requirement Activities" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Requirements Change Requests" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Developing a Requirements Traceability Matrix" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Milestones List" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Activity Sequencing" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Schedule Baseline" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Basis of Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Budget Breakdown" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Cost Baseline" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Establish Quality Metrics" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Plan Quality Control" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Plan Quality Assurance" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Tracking" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Acquisition Plan" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Calendars" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Communication Schedule" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Establish Communication Methods" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder Communications Requirements" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Developing a Statement of Work" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Contracting Terms and Conditions" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Deliverables List" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder Register" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder Communication Plan" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder Power Models Salience Power Influence" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Stakeholder Categorization" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Standardized Change Process" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Impact Assessments" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Change Log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Requirements Change Requests" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Standardized Configuration Controls" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Item Logging" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Allocations" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project Scope Statement" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Work Breakdown Structure" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Scope Change Log" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Activity Cost Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Cost Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Work Package Estimates" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Gantt Chart" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Milestone Chart" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Allocations" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Scope, Cost, Schedule Baselines" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Resource Performance Reports" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Percent Complete Analysis" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Life Cycle Description" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Framework for Project Lifecycle" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Project Influences" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Agile" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Waterfall" });
                subCategories.Add(new FactorSubCategory { FactorSubCategoryDesc = "Hybrid" });

                foreach (FactorSubCategory subCategory in subCategories)
                {
                    context.FactorSubCategories.Add(subCategory);
                }

                context.SaveChanges();
            }


            List<Factor> factors = new List<Factor>();

            if (!context.Factors.Any())
            {
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity attributes").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity list").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Assumption log").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Basis of Estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Change log").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost forecasts").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Duration estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Issue log").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Lessons learned register").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestone list").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Physical resource assignments").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project calendars").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project communication").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project schedule").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project schedule network diagram").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project scope statement").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project team assignments").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality control measurements").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality metrics").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality report").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements documentation").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements traceability matrix").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource breakdown structure").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource calendars").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource requirements").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Risk register").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Risk report").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule data").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule forecasts").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder register").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Team charter").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Test and evaluation documents").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope Statement").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Definition of Scope").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Logging Requirement Activities").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Developing a Requirements Traceability Matrix").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestones List").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity Sequencing").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule Baseline").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Basis of Estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Budget Breakdown").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost Baseline").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Establish Quality Metrics").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Plan Quality Control").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Plan Quality Assurance").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Breakdown Structure").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Tracking").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Acquisition Plan").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Calendars").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Communication Schedule").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Establish Communication Methods").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Communications Requirements").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Developing a Statement of Work").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Contracting Terms and Conditions").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Deliverables List").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Register").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Communication Plan").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Power Models Salience Power Influence").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Categorization").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Standardized Change Process").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Impact Assessments").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Change Log").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Standardized Configuration Controls").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Item Logging").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project Scope Statement").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope Change Log").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity Cost Estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Cost Estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Package Estimates").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Gantt Chart").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestone Chart").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope, Cost, Schedule Baselines").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Performance Reports").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Percent Complete Analysis").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Life Cycle Description").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Framework for Project Lifecycle").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project Influences").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Agile").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Waterfall").FactorSubCategoryId
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Hybrid").FactorSubCategoryId
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

                documents.Add(new Document { Name = "Doc1", Url = @"/pdf/project1.pdf", ProjectDocFk = 1 });
                documents.Add(new Document { Name = "Doc2", Url = @"/pdf/project2.pdf", ProjectDocFk = 1 });
                documents.Add(new Document { Name = "Doc3", Url = @"/pdf/project1.pdf", ProjectDocFk = 2 });
                documents.Add(new Document { Name = "Doc4", Url = @"/pdf/project3.pdf", ProjectDocFk = 2 });

                foreach (Document document in documents)
                {
                    context.Documents.Add(document);
                }

                context.SaveChanges();
            }

            var doc1 = context.Documents.Where(c => c.DocumentId == 1).Single();
            var doc2 = context.Documents.Where(c => c.DocumentId == 2).Single();
            var doc3 = context.Documents.Where(c => c.DocumentId == 3).Single();

            var fac1 = context.Factors.Where(c => c.FactorId == 1).Single();
            var fac2 = context.Factors.Where(c => c.FactorId == 2).Single();
            var fac3 = context.Factors.Where(c => c.FactorId == 3).Single();
            var fac4 = context.Factors.Where(c => c.FactorId == 4).Single();
            var fac5 = context.Factors.Where(c => c.FactorId == 5).Single();
            var fac6 = context.Factors.Where(c => c.FactorId == 6).Single();

            var proj1 = context.Projects.Where(c => c.ProjectId == 1).Single();
            var proj2 = context.Projects.Where(c => c.ProjectId == 2).Single();
            var proj3 = context.Projects.Where(c => c.ProjectId == 3).Single();


            // Create Project Factor Relations
            if (!context.ProjectFactorRels.Any())
            {
                List<ProjectFactorRel> projFactors = new List<ProjectFactorRel>() {
                new ProjectFactorRel(){ ProjectFk = 1, FactorFk = 1 },
                new ProjectFactorRel(){ ProjectFk = 1, FactorFk = 2 },
                new ProjectFactorRel(){ ProjectFk = 1, FactorFk = 3 },
                new ProjectFactorRel(){ ProjectFk = 2, FactorFk = 40 },
                new ProjectFactorRel(){ ProjectFk = 2, FactorFk = 54 },
                new ProjectFactorRel(){ ProjectFk = 2, FactorFk = 62 },
                new ProjectFactorRel(){ ProjectFk = 2, FactorFk = 10 }
            };

                foreach (ProjectFactorRel rel in projFactors)
                {
                    context.ProjectFactorRels.Add(rel);
                }

                context.SaveChanges();
            }


            //proj1.Documents = new List<Document> { doc1, doc2 };
            //proj2.Documents = new List<Document>{ doc2, doc3 };
            //proj3.Documents = new List<Document>{ doc1, doc3 };

            context.Projects.Update(proj1);
            context.Projects.Update(proj2);
            context.Projects.Update(proj3);

            context.SaveChanges();
        
        }
    }
}
