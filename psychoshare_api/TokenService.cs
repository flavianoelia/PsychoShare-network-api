using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Crea el token JWT completo a partir de un objeto User.
    /// </summary>
    public string CreateToken(entity_library.system.User user)
    {
        // ----------------------------------------------------
        // PASO 1: DEFINICIÓN DE LA IDENTIDAD (PAYLOAD / CLAIMS)
        // ----------------------------------------------------
        
        // 1.1. Crea una lista de 'Claims' (declaraciones sobre el usuario).
        // Estas declaraciones son la información que se codificará en el cuerpo del token.
        var userClaims = new List<Claim>
        {
            // Claim 1: Email del usuario (Útil para identificarlo sin ir a la DB)
            new Claim(ClaimTypes.Email, user.Email),
            
            // Claim 2: ID único del usuario (Fundamental para la autorización)
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            
            // Si tuvieras roles, añadirías: new Claim(ClaimTypes.Role, user.Rol)
        };

        // Asigna las claims creadas a una nueva identidad.
        var identidadDelSujeto = new ClaimsIdentity(userClaims);


        // ----------------------------------------------------
        // PASO 2: CREACIÓN DE LA FIRMA DIGITAL (SIGNATURE)
        // ----------------------------------------------------
        
        // 2.1. Obtiene la Clave Secreta desde el archivo de configuración (appsettings.json)
        // La clave se convierte a bytes usando UTF8.
        string jwtKey = _config["Jwt:Key"] 
            ?? throw new ArgumentNullException("La clave 'Jwt:Key' no fue encontrada en la configuración.");
            
        var claveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        // 2.2. Define las credenciales de firma: la clave y el algoritmo criptográfico.
        // HmacSha512Signature es el algoritmo estándar y seguro para firmar el token.
        var credencialesDeFirma = new SigningCredentials(
            claveSecreta, 
            SecurityAlgorithms.HmacSha512Signature
        );


        // ----------------------------------------------------
        // PASO 3: DESCRIPTOR DEL TOKEN (REGLAS Y METADATOS)
        // ----------------------------------------------------
        
        // 3.1. Crea un objeto que describe TODAS las propiedades del token a generar.
        var descriptorDelToken = new SecurityTokenDescriptor
        {
            // El Sujeto del token es la identidad que creamos en el Paso 1.
            Subject = identidadDelSujeto, 
            
            // Define el tiempo de expiración (Ej: 7 días desde ahora).
            Expires = DateTime.Now.AddDays(7), 
            
            // Asigna las credenciales de firma creadas en el Paso 2.
            SigningCredentials = credencialesDeFirma, 
            
            // Quién EMITE el token (el nombre de tu API, tomado de appsettings).
            Issuer = _config["Jwt:Issuer"],
            
            // Quién DEBE USAR el token (el cliente, tomado de appsettings).
            Audience = _config["Jwt:Audience"] 
        };


        // ----------------------------------------------------
        // PASO 4: CREACIÓN Y SERIALIZACIÓN FINAL
        // ----------------------------------------------------

        // 4.1. Instancia la clase que sabe cómo crear el objeto token.
        var manejadorDeToken = new JwtSecurityTokenHandler();

        // 4.2. Crea el objeto SecurityToken usando el descriptor (es el objeto C# del JWT).
        var objetoToken = manejadorDeToken.CreateToken(descriptorDelToken);

        // 4.3. Convierte el objeto token en la cadena de texto final (Header.Payload.Signature)
        // y la retorna para ser enviada al cliente.
        return manejadorDeToken.WriteToken(objetoToken);
    }
}