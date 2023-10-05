using System.ComponentModel.DataAnnotations;

namespace ApiAuth.Dtos;
public class AuthDto
{
    [Required]
    public string Code { get; set; }

    [Required]
    public long Id { get; set; }
}