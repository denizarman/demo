using Demo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.DatabaseContext
{
    public static class DataGenerator
    {
        #region Fields

        private static DateTime _seedTime = DateTime.Now;

        #endregion

        #region Public Methods

        public static void Initialize(DemoContext context)
        {
            SeedRoles(context);
            SeedUsers(context);
            SeedDepartments(context);
            SeedStudents(context);
        }

        #endregion

        #region Private Methods

        private static void SeedRoles(DemoContext context)
        {
            if (context.Roles.Any())
            {
                Console.WriteLine("Roles have already been seeded...");
                return;
            }

            try
            {
                context.Roles.AddRange(
                    new Role()
                    {
                        CreatedAt = _seedTime,
                        Name = "admin"
                    },
                    new Role()
                    {
                        CreatedAt = _seedTime,
                        Name = "user"
                    });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has been occur at Role seeding... {0}", ex.Message);
            }
        }

        private static void SeedUsers(DemoContext context)
        {
            if (context.Users.Any())
            {
                Console.WriteLine("Users have already been seeded...");
                return;
            }

            try
            {
                context.Users.AddRange(
                    new User()
                    {
                        CreatedAt = _seedTime,
                        Username = "admin",
                        Password = "admin",
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Roles = new List<Role>()
                        {
                            context.Roles.Where(x => x.Name == "admin").FirstOrDefault(),
                            context.Roles.Where(x => x.Name == "user").FirstOrDefault()
                        }
                    },
                    new User()
                    {
                        CreatedAt = _seedTime,
                        Username = "user1",
                        Password = "user1",
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Roles = new List<Role>()
                        {
                            context.Roles.Where(x => x.Name == "user").FirstOrDefault()
                        }
                    },
                    new User()
                    {
                        CreatedAt = _seedTime,
                        Username = "user2",
                        Password = "user2",
                        FirstName = "FirstName",
                        LastName = "LastName",
                        Roles = new List<Role>()
                        {
                            context.Roles.Where(x => x.Name == "user").FirstOrDefault()
                        }
                    });

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has been occur at User seeding... {0}", ex.Message);
            }
        }

        private static void SeedDepartments(DemoContext context)
        {
            if (context.Departments.Any())
            {
                Console.WriteLine("Departments have already been seeded...");
                return;
            }

            try
            {
                context.Departments.AddRange(
                    new Department()
                    {
                        CreatedAt = _seedTime,
                        CreatedBy = 1,
                        Name = "Computer Engineering",
                        Code = "CENG",
                    },
                    new Department()
                    {
                        CreatedAt = _seedTime,
                        CreatedBy = 1,
                        Name = "Industrial Engineering",
                        Code = "IENG",
                    },
                    new Department()
                    {
                        CreatedAt = _seedTime,
                        CreatedBy = 1,
                        Name = "Electronical Engineering",
                        Code = "EENG",
                    }) ;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has been occur at Department seeding... {0}", ex.Message);
            }
        }

        private static void SeedStudents(DemoContext context)
        {
            if (context.Students.Any())
            {
                Console.WriteLine("Students have already been seeded...");
                return;
            }

            try
            {
                List<Student> studentList = new List<Student>();
                int id = 1;
                
                for (int i = 0; i < 50; i++)
                {
                    foreach (var dept in context.Departments)
                    {
                        Random rand = new Random(DateTime.Now.Millisecond);
                        studentList.Add(new Student()
                        {
                            //Id = id,
                            CreatedAt = _seedTime,
                            CreatedBy = 1,
                            BirthDate = new DateTime(1990, rand.Next(1, 12), rand.Next(1, 28)),
                            DepartmantId = dept.Id,
                            FirstName = "Student-FirstName-" + id,
                            LastName = "Student-LastName-" + id,
                            PlaceOfBirth = "Student-BirthPlace-" + id,
                            StudentNumber = _seedTime.Year + "3" + dept.Id + id
                        }) ;
                        id++;
                        Task.Delay(rand.Next(1, 50)).Wait();
                    }
                }
                context.Students.AddRange(studentList);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception has been occur at Department seeding... {0}", ex.Message);
            }
        }

        #endregion
    }
}
