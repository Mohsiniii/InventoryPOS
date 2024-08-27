namespace InventoryPOS.Models
{
    public class User
    {
        private string _id;
        private string _name { get; set; }
        private string _email { get; set; }
        private bool _isLoggedIn { get; set; }

        public User()
        {
            _isLoggedIn = false;
        }

        public User(string tmpUserID)
        {
            _isLoggedIn = false;
            if(tmpUserID != null)
            {
                _id = tmpUserID.Trim();
                _isLoggedIn = true;
            }
        }

        public bool IsLoggedIn()
        {
            return _isLoggedIn;
        }

        public bool login(string email, string password)
        {
            _isLoggedIn = true;
            return _isLoggedIn;
        }
    }
}
