using System;
using ClaimBaseTest.Model.BaseEntityModel;
namespace ClaimBaseTest.Model.Employee
{
	public class Employee 
	{
		public int EmployeeID { get; set; }
		public string? LastName { get; set; }
		public string? FirstName { get; set; }
		public string? Qualification { get; set; }
		public string? Address { get; set; }
		public string? City { get; set; }
		public string? Gender { get; set; }
		public string? EmployeeDocument { get; set; }
	}
}

