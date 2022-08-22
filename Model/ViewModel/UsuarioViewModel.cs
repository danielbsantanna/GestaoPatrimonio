using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string Permissao { get; set; }
        public string Token { get; set; }
    }
}
