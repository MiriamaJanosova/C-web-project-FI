namespace BL.DTOs.Users
/////////////////////////////////////////
/// Kdyz user vleze do nastaveni uctu ///
/////////////////////////////////////////

{
    public class UserShowSettingPage
    {
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        public string Email { get; set; }
        
        public string UserRole { get; set; }
    }
}