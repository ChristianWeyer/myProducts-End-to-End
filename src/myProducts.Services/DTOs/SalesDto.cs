using System.Collections.Generic;

namespace MyProducts.Services.DTOs
{
	public class SalesDto
	{
		public string Key { get; set; }
		public IEnumerable<SalesValueDto> Values { get; set; } 
	}
}