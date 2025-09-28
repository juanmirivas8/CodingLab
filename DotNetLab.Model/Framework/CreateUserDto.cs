using System.ComponentModel.DataAnnotations;

namespace DotNetLab.Model.Framework;

public class CreateUserDto
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public Guid CreatedBy { get; set; }
    
    [Required]
    public Guid LastModifiedBy { get; set; }
}
