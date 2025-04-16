using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Interfaces;
using UserService.Models;
using UserService.Utilities.StdDef;

namespace UserService.Data
{
    public class UserSeedData
    {
        public static async Task InitializeAsync(UserDbContext context, bool reseed, IHashingRepository hashingRepository, CancellationToken token)
        {
            if (reseed && context.Users.Any())
            {
                context.RemoveRange(context.Users);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Users', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Users.Any())
            {
                await context.Users.AddRangeAsync(
                    new User
                    {
                        Email = "test1.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test1",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Doctor.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    },
                    new User
                    {
                        Email = "test2.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test2",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Nurse.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    },
                    new User
                    {
                        Email = "test3.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test3",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Patient.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    },
                    new User
                    {
                        Email = "test4.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test4",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserAccountType = [UserAccountType.Admin.ToString()]
                    },
                    new User
                    {
                        Email = "test5.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test5",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Doctor.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    },
                    new User
                    {
                        Email = "test6.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test6",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserAccountType = [UserAccountType.Developer.ToString()]
                    },
                    new User
                    {
                        Email = "test7.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test7",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Patient.ToString()],
                        UserAccountType = [UserAccountType.Admin.ToString()]
                    },
                    new User
                    {
                        Email = "test8.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test8",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Nurse.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    },
                    new User
                    {
                        Email = "test9.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test9",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Nurse.ToString()],
                        UserAccountType = [UserAccountType.Admin.ToString()]
                    },
                    new User
                    {
                        Email = "test10.testsson@gmail.com",
                        PasswordHash = hashingRepository.HashPassword("123"),
                        PhoneNumber = "1234567890",
                        FirstName = "Test10",
                        LastName = "Testsson",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        UserType = [UserType.Patient.ToString()],
                        UserAccountType = [UserAccountType.User.ToString()]
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
