﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vjezba.Model
{
	public class Client
	{
		[Key]
		public int ID { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public char Gender { get; set; }

		public string Address { get; set; }

		public string PhoneNumber { get; set; }

		[ForeignKey("City")]
		public int? CityID { get; set; }

		public virtual City City { get; set; }

		public virtual ICollection<Meeting> Meetings { get; set; }

		public string FullName => $"{FirstName} {LastName}";
	}
}
