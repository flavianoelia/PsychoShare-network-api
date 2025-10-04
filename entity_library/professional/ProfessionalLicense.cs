namespace entity_library.professional
{
    public class ProfessionalLicense
    {
        private long id;
        private string licenseNumber = "";
        private string verifiedName = "";
        private string verifiedDni = "";
        private bool isVerified = false;
        private long userId;

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }

        public string VerifiedName
        {
            get { return verifiedName; }
            set { verifiedName = value; }
        }

        public string VerifiedDni
        {
            get { return verifiedDni; }
            set { verifiedDni = value; }
        }

        public bool IsVerified
        {
            get { return isVerified; }
            set { isVerified = value; }
        }

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }
    }
}