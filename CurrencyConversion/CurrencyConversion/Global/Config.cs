namespace CurrencyConversion.Global
{
    public class Config
    {
        // Variáveis estáticas contendo as credenciais de banco
        public static string dbHost = string.Empty;
        public static string dbPort = string.Empty;
        public static string dbUser = string.Empty;
        public static string dbName = string.Empty;
        public static string dbPass = string.Empty;

        // Método para leitura de arquivo, de configurações
        public void LoadConfigurations()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            try
            {
                dbHost = config.GetValue<string>("Database:DbHost");
                dbPort = config.GetValue<string>("Database:DbPort");
                dbUser = config.GetValue<string>("Database:DbUser");
                dbName = config.GetValue<string>("Database:DbName");
                dbPass = config.GetValue<string>("Database:DbPass");
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao acessar o banco de dados. " + ex.Message);
            }
        }
    }
}
