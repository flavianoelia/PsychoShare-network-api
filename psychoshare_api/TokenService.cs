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
    public string CreateToken(entity_library.system.User user)
    {
        // ----------------------------------------------------
        //STEP 1: DEFINITION OF THE IDENTITY (PAYLOAD / CLAIMS)
        // ----------------------------------------------------

        // Claims that go to be codified for the body of the token
        var userClaims = new List<Claim>
        {
            // Claim 1: Email form User
            new Claim(ClaimTypes.Email, user.Email),
            
            // Claim 2: ID from User
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            
            // Cuando tengamos roles: new Claim(ClaimTypes.Role, user.Rol)
        };

        // Assign created claims to a new identity.
        var subjectIdentity = new ClaimsIdentity(userClaims);

        // ----------------------------------------------------
        // STEP 2: CREATION OF THE SIGNATURE
        // ----------------------------------------------------
        
        // 2.1. Get key from appssetingDevelopment.json
        string jwtKey = _config["Jwt:Key"] 
            ?? throw new ArgumentNullException("La clave 'Jwt:Key' no fue encontrada en la configuración.");
            
        var claveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        // 2.2. Define credentiasl of the signature: pass and algorithm.
        var credencialesDeFirma = new SigningCredentials(
            claveSecreta, 
            SecurityAlgorithms.HmacSha512Signature
        );

        // ----------------------------------------------------
        // STEP 3: DESCRIPTOR OF TOKEN (RULES AND METADATA)
        // ----------------------------------------------------
        
        // 3.1. Create an object that describes ALL the properties of the token to be generated.
        var descriptorDelToken = new SecurityTokenDescriptor
        {
            // The Subject of the token is the identity we created in Step 1.
            Subject = subjectIdentity, 
            
            // Define the expiration time (In: 7 days from now).
            Expires = DateTime.Now.AddDays(7), 
            
            // Assign the signing credentials created in Step 2.
            SigningCredentials = credencialesDeFirma, 
            
            // Who ISSUES the token (your API name, taken from appsettings).
            Issuer = _config["Jwt:Issuer"],
            
            // Who MUST USE the token (the client, taken from appsettings).
            Audience = _config["Jwt:Audience"] 
        };


        // ----------------------------------------------------
        // STEP 4: FINAL CREATION AND SERIALIZATION
        // ----------------------------------------------------

        // 4.1. Instantiate the class that knows how to create the token object.
        var manejadorDeToken = new JwtSecurityTokenHandler();

        // 4.2. Create the Security Token object using the descriptor (it's the C# object of the JWT).
        var objetoToken = manejadorDeToken.CreateToken(descriptorDelToken);

        // 4.3. Convert the token object into the final text string (Header.Payload.Signature)
        // and returns it to be sent to the customer.
        return manejadorDeToken.WriteToken(objetoToken);
    }
}