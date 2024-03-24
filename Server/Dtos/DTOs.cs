using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Dtos
{
    public class DTOs
    {
        
    }

    public record UserDTO(string email, string password);
    public record TodoDTO(string title, string description, string userId);
    public record UpdateTodoDTO(string id, string title, string description);
    
}