using entity_library.system;
namespace entity_library.media
{
    public class Avatar : Image
    {
        private User? user;
        public virtual User? User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
