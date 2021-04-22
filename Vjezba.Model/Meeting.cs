#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vjezba.Model
{
	public class Meeting
	{
		[Key]
		public int ID { get; set; }

		public MeetingType Type { get; set; }

		public DateTime? BeginTime { get; set; }

		public DateTime? EndTime { get; set; }

		public Status Status { get; set; }

		public string? Location { get; set; }

		public string? Comments { get; set; }

		[ForeignKey("Client")]
		public int ClientID { get; set; }
	}

	public enum MeetingType
	{
		InPerson, VideoCall
	}

	public enum Status
	{
		Scheduled, Cancelled
	}
}
