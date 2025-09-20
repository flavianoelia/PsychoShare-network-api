public interface DAOFollowing
{
    public void Save(Following following);

    public List<User> GetContactsFromUser(long userId);
}