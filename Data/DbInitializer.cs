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
            if (!context.Permissions.Any())
            {
                var permissions = new Permission[]
                {
                    new Permission {Level=1, Name="admin" },
                    new Permission {Level=2, Name="students" }
                };

                foreach (Permission perm in permissions)
                {
                    context.Permissions.Add(perm);
                }

                context.SaveChanges();
            }

            /*
             * Create User Data
             */
            if (!context.Users.Any())
            {
                var users = new User[]
                {
                    new User {Email="prof1@school.com", Password="test123", PermissionLevel=1 },
                    new User {Email="prof2@school.com", Password="test123", PermissionLevel=1 },
                    new User {Email="student3@school.com", Password="test123", PermissionLevel=2 }
                };

                foreach (User user in users) {
                    context.Users.Add(user);
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
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity attributes").FactorSubCategoryId,
                    Position = 1

                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity list").FactorSubCategoryId,
                    Position = 2
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Assumption log").FactorSubCategoryId,
                    Position = 3
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Basis of Estimates").FactorSubCategoryId,
                    Position = 4
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Change log").FactorSubCategoryId,
                    Position = 5
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost estimates").FactorSubCategoryId,
                    Position = 6
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost forecasts").FactorSubCategoryId,
                    Position = 7
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Duration estimates").FactorSubCategoryId,
                    Position = 8
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Issue log").FactorSubCategoryId,
                    Position = 9
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Lessons learned register").FactorSubCategoryId,
                    Position = 10
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestone list").FactorSubCategoryId,
                    Position = 11
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Physical resource assignments").FactorSubCategoryId,
                    Position = 12
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project calendars").FactorSubCategoryId,
                    Position = 13
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project communication").FactorSubCategoryId,
                    Position = 14
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project schedule").FactorSubCategoryId,
                    Position = 15
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project schedule network diagram").FactorSubCategoryId,
                    Position = 16
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project scope statement").FactorSubCategoryId,
                    Position = 17
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project team assignments").FactorSubCategoryId,
                    Position = 18
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality control measurements").FactorSubCategoryId,
                    Position = 19
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality metrics").FactorSubCategoryId,
                    Position = 20

                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Quality report").FactorSubCategoryId,
                    Position = 21
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements documentation").FactorSubCategoryId,
                    Position = 22
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements traceability matrix").FactorSubCategoryId,
                    Position = 23
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource breakdown structure").FactorSubCategoryId,
                    Position = 24
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource calendars").FactorSubCategoryId,
                    Position = 25
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource requirements").FactorSubCategoryId,
                    Position = 26
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Risk register").FactorSubCategoryId,
                    Position = 27
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Risk report").FactorSubCategoryId,
                    Position = 28
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule data").FactorSubCategoryId,
                    Position = 29
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule forecasts").FactorSubCategoryId,
                    Position = 30
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder register").FactorSubCategoryId,
                    Position = 31
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Team charter").FactorSubCategoryId,
                    Position = 32
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "none").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Test and evaluation documents").FactorSubCategoryId,
                    Position = 33
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope Statement").FactorSubCategoryId,
                    Position = 34
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId,
                    Position = 35
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Definition of Scope").FactorSubCategoryId,
                    Position = 36
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Logging Requirement Activities").FactorSubCategoryId,
                    Position = 37
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId,
                    Position = 38
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Requirements Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Developing a Requirements Traceability Matrix").FactorSubCategoryId,
                    Position = 39
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestones List").FactorSubCategoryId,
                    Position = 40
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity Sequencing").FactorSubCategoryId,
                    Position = 41
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Schedule Baseline").FactorSubCategoryId,
                    Position = 42
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Basis of Estimates").FactorSubCategoryId,
                    Position = 43
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Budget Breakdown").FactorSubCategoryId,
                    Position = 44
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Cost Baseline").FactorSubCategoryId,
                    Position = 45
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Establish Quality Metrics").FactorSubCategoryId,
                    Position = 46
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Plan Quality Control").FactorSubCategoryId,
                    Position = 47
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Quality Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Plan Quality Assurance").FactorSubCategoryId,
                    Position = 48
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Breakdown Structure").FactorSubCategoryId,
                    Position = 49
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Tracking").FactorSubCategoryId,
                    Position = 50
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Acquisition Plan").FactorSubCategoryId,
                    Position = 51

                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Resource Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Calendars").FactorSubCategoryId,
                    Position = 52
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Communication Schedule").FactorSubCategoryId,
                    Position = 53
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Establish Communication Methods").FactorSubCategoryId,
                    Position = 54
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Communications Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Communications Requirements").FactorSubCategoryId,
                    Position = 55
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Developing a Statement of Work").FactorSubCategoryId,
                    Position = 56
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Contracting Terms and Conditions").FactorSubCategoryId,
                    Position = 57
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Procurement Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Deliverables List").FactorSubCategoryId,
                    Position = 58
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Register").FactorSubCategoryId,
                    Position = 59
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Communication Plan").FactorSubCategoryId,
                    Position = 60
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Power Models Salience Power Influence").FactorSubCategoryId,
                    Position = 61
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Stakeholder Engagement Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Stakeholder Categorization").FactorSubCategoryId,
                    Position = 62
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Standardized Change Process").FactorSubCategoryId,
                    Position = 63
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Impact Assessments").FactorSubCategoryId,
                    Position = 64
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Change Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Change Log").FactorSubCategoryId,
                    Position = 65
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Requirements Change Requests").FactorSubCategoryId,
                    Position = 66
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Standardized Configuration Controls").FactorSubCategoryId,
                    Position = 67
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Item Logging").FactorSubCategoryId,
                    Position = 68
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Configuration Management Plan").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId,
                    Position = 69
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project Scope Statement").FactorSubCategoryId,
                    Position = 70
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Breakdown Structure").FactorSubCategoryId,
                    Position = 71
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Scope Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope Change Log").FactorSubCategoryId,
                    Position = 72
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Activity Cost Estimates").FactorSubCategoryId,
                    Position = 73
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Cost Estimates").FactorSubCategoryId,
                    Position = 74
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Cost Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Work Package Estimates").FactorSubCategoryId,
                    Position = 75
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Gantt Chart").FactorSubCategoryId,
                    Position = 76
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Milestone Chart").FactorSubCategoryId,
                    Position = 77
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Schedule Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Allocations").FactorSubCategoryId,
                    Position = 78
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Scope, Cost, Schedule Baselines").FactorSubCategoryId,
                    Position = 79
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Resource Performance Reports").FactorSubCategoryId,
                    Position = 80
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Performance Measurement Baseline").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Percent Complete Analysis").FactorSubCategoryId,
                    Position = 81
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Life Cycle Description").FactorSubCategoryId,
                    Position = 82
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Framework for Project Lifecycle").FactorSubCategoryId,
                    Position = 83
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Lifecycle Analysis").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Project Influences").FactorSubCategoryId,
                    Position = 84
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Agile").FactorSubCategoryId,
                    Position = 85
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Waterfall").FactorSubCategoryId,
                    Position = 86
                });
                factors.Add(new Factor {
                    FactorMainCategoryFk = context.FactorMainCategories.First(c => c.FactorMainCategoryDesc == "Project Development Approach").FactorMainCategoryId,
                    FactorSubCategoryFk = context.FactorSubCategories.First(c => c.FactorSubCategoryDesc == "Hybrid").FactorSubCategoryId,
                    Position = 87
                });


                foreach (var factor in factors)
                {
                    context.Factors.Add(factor);
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
                    new Project {Name="Project2", Success="Yes"},
                    new Project {Name="Project3", Success="Yes"},
                };

                foreach (Project project in projects)
                {
                    context.Projects.Add(project);
                }

                context.SaveChanges();
            }

            var proj2 = context.Projects.Where(c => c.Name == "Project2").Single();
            var proj3 = context.Projects.Where(c => c.Name == "Project3").Single();

            List<Document> documents = new List<Document>();

            if (!context.Documents.Any())
            {
                documents.Add(new Document { Name = "DocumentExample1", Url = "proj2/doc1.example.com", ProjectFk = proj2.ProjectId });
                documents.Add(new Document { Name = "DocumentExample2", Url = "proj2/doc2.example.com", ProjectFk = proj2.ProjectId });
                documents.Add(new Document { Name = "DocumentExample3", Url = "proj3/doc3.example.com", ProjectFk = proj3.ProjectId });
                documents.Add(new Document { Name = "DocumentExample4", Url = "proj3/doc4.example.com", ProjectFk = proj3.ProjectId });
                documents.Add(new Document { Name = "Doc2", Url = "proj1/doc2.aws.amazon.com", ProjectFk = proj2.ProjectId });
                documents.Add(new Document { Name = "Doc3", Url = "proj1/doc3.aws.amazon.com", ProjectFk = proj2.ProjectId });
                documents.Add(new Document { Name = "Doc4", Url = "proj1/doc4.aws.amazon.com", ProjectFk = proj3.ProjectId });
                documents.Add(new Document { Name = "Doc5", Url = "proj1/doc5.aws.amazon.com", ProjectFk = proj3.ProjectId });
                foreach (Document document in documents)
                {
                    context.Documents.Add(document);
                }

                context.SaveChanges();
            }

            var doc1 = context.Documents.Where(c => c.DocumentId == 1).Single();
            var doc2 = context.Documents.Where(c => c.DocumentId == 2).Single();
            var doc3 = context.Documents.Where(c => c.DocumentId == 3).Single();
            var doc4 = context.Documents.Where(c => c.DocumentId == 4).Single();
            var doc5 = context.Documents.Where(c => c.DocumentId == 5).Single();

            var fac1 = context.Factors.Where(c => c.FactorId == 1).Single();
            var fac2 = context.Factors.Where(c => c.FactorId == 2).Single();
            var fac3 = context.Factors.Where(c => c.FactorId == 3).Single();
            var fac4 = context.Factors.Where(c => c.FactorId == 4).Single();
            var fac5 = context.Factors.Where(c => c.FactorId == 5).Single();
            var fac6 = context.Factors.Where(c => c.FactorId == 6).Single();

            // seeding FavorProjs 
            if (!context.FavorProjs.Any())
            {
                List<FavorProj> favorProjs = new List<FavorProj>();

                favorProjs.Add(new FavorProj { ProjectId = proj2.ProjectId, Url = "~/Project/ViewProject/" + proj2.ProjectId });
                favorProjs.Add(new FavorProj { ProjectId = proj3.ProjectId, Url = "~/Project/ViewProject/" + proj3.ProjectId });

                foreach (FavorProj favorProj in favorProjs)
                {
                    context.FavorProjs.Add(favorProj);
                }

                context.SaveChanges();
            }

            // seeding FavorDocs 
            if (!context.FavorDocs.Any())
            {
                List<FavorDoc> favorDocs = new List<FavorDoc>();

                favorDocs.Add(new FavorDoc { DocumentId = 1 });
                favorDocs.Add(new FavorDoc { DocumentId = 2 });
                favorDocs.Add(new FavorDoc { DocumentId = 3 });

                foreach (FavorDoc favordoc in favorDocs)
                {
                    context.FavorDocs.Add(favordoc);
                }

                context.SaveChanges();
            }

            // Create Project Factor Relations
            if (!context.DocumentFactorRels.Any())
            {
                List<DocumentFactorRel> docFactors = new List<DocumentFactorRel>() {
                new DocumentFactorRel(){ DocumentFk = 1, FactorFk = 1 },
                new DocumentFactorRel(){ DocumentFk = 1, FactorFk = 2 },
                new DocumentFactorRel(){ DocumentFk = 1, FactorFk = 3 },
                new DocumentFactorRel(){ DocumentFk = 2, FactorFk = 40 },
                new DocumentFactorRel(){ DocumentFk = 2, FactorFk = 54 },
                new DocumentFactorRel(){ DocumentFk = 3, FactorFk = 62 },
                new DocumentFactorRel(){ DocumentFk = 3, FactorFk = 10 },
                new DocumentFactorRel(){ DocumentFk = 4, FactorFk = 62 },
                new DocumentFactorRel(){ DocumentFk = 5, FactorFk = 10 }
            };

                foreach (DocumentFactorRel rel in docFactors)
                {
                    context.DocumentFactorRels.Add(rel);
                }

                context.SaveChanges();
            }
        }
    }
}
