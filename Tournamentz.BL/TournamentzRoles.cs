namespace Tournamentz.BL
{
    using System;

    public static class TournamentzRoles
    {
        public const string AdminText = "Admin";
        public const string UserText = "User";

        public const string Admin = "817EF2BA-3863-4C3A-954E-CBBA8809F361";
        public const string User = "3E2DDD01-0243-4E38-B563-80BCA9EF7BA7";

        public static readonly Guid AdminGuid = new Guid(Admin);
        public static readonly Guid UserGuid = new Guid(User);
    }
}