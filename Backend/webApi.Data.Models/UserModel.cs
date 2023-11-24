using System.ComponentModel.DataAnnotations;

namespace webApi.Data.Models
{
	public class UserModel
	{
		[Key]
		public int UserId { get; set; }

		[Required] 
		public string Username { get; set; } = null!;

		[Required] 
		[EmailAddress] 
		public string Email { get; set; } = null!;

		[Required]
		public string Passwords { get; set; } = null!; //TODO: Ask Svetlio if we can change from plural to single word

		[Required]
		public int Age { get; set; }

		[Required]
		public int Height { get; set; }
	}
}