using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        // Método de extensão para IConfiguration que retorna a string de conexão "DefaultConnection".
        // O uso de 'this' no parâmetro permite chamar este método diretamente em instâncias de IConfiguration,
        // como se fosse um método nativo da interface.
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection")!;
        }

    }
}
