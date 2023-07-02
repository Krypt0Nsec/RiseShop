﻿using System.ComponentModel.DataAnnotations;

namespace Rise.Services.AddressAPI.Models

{
	public class City
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }
	}
}
