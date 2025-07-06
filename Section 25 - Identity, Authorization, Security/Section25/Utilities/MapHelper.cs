//using Section25.ViewModels;
//using BusinessLayer.DTO;

//namespace Section25.Utilities
//{
//    public static class MapHelper
//    {
//        public static UserViewModel ToViewModel(this User user)
//        {
//            if (user is null) return null!;

//            return new UserViewModel
//            {
//                Id = user.Id,
//                Name = user.Name,
//                Location = user.Location,
//                Email = user.Email,
//                CreatedOn = user.CreatedOn,
//                ModifiedOn = user.ModifiedOn,
//                isDummy = user.isDummy
//            };
//        }
//        public static User ToEntity(this UserViewModel user)
//        {
//            if (user is null) return null!;

//            return new User
//            {
//                Id = user.Id,
//                Name = user.Name,
//                Location = user.Location,
//                Email = user.Email,
//                CreatedOn = user.CreatedOn,
//                ModifiedOn = user.ModifiedOn,
//                isDummy = user.isDummy
//            };
//        }
//    }
//}
