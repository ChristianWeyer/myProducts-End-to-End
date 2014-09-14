using System.Collections.Generic;

namespace MyProducts.Services.DTOs
{
	public class SalesDto
	{
		string Key { get; set; }
		IEnumerable<SalesValueDto> Values { get; set; } 
	}
}