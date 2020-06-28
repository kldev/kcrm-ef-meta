using System;

namespace KCrm.ConsoleSample {
    static class Constant {
        public const string ProjectTagRootId = "724fd01c-bed0-420d-b592-ea3da3218b1e";
        public const string ProjectTagImportantId = "47380492-388e-4ef3-9b53-12f72978af55";
        public const string ProjectTagGermanLanguageRequiredId = "fc4bf80e-89ba-4811-ba06-02463f53d067";
        public const string ProjectTagEnglishLanguageRequiredId = "23099e57-69e6-4046-8a64-007352b70429";
        public const string ProjectTagDesignOnlyId = "76d7c5c8-36b8-4d04-8c97-dee5a6d09622";

        public const string UserRootId = "4a4f01b9-db2b-4081-86fe-3937e3c688bc";
        public const string UserAdminId = "3d1ef4fe-6b4f-4db9-ba56-506a5e69443c";
        public const string UserJohnId = "10e01324-dc9d-4d45-9f8c-a691a19cf1a8";
        public const string UserAliceId = "6fc3bb1d-1284-4ff4-b1cc-98fd5f4fae0b";

        public const string UserRoleRootId = "28bd4131-d651-4460-bf1e-4b361ebefece";
        public const string UserRoleAdminId = "ac484039-f360-4c72-9566-8f01a5215d8d";
        public const string UserRoleSellerId = "179c593d-e9a6-407b-be43-048aff0f8581";

        public const string ProjectFinTechId = "529ff65d-14a5-45ad-8ea0-ae6248b02f11";
        public const string ProjectErpId = "95775841-76fb-4d38-b244-f35ab165a017";
        public const string ProjectErp2Id = "1cc33c95-6873-40b3-9bc6-aa1c4595a89e";
    }

    static class TagHelper {
        public static Guid RootId => Guid.Parse (Constant.ProjectTagRootId);
        public static Guid ImportantTagId => Guid.Parse (Constant.ProjectTagImportantId);
        public static Guid GermanTagId => Guid.Parse (Constant.ProjectTagGermanLanguageRequiredId);
        public static Guid EnglishTagId => Guid.Parse (Constant.ProjectTagEnglishLanguageRequiredId);
        public static Guid DesignOnly => Guid.Parse (Constant.ProjectTagDesignOnlyId);
    }

    static class UserHelper {
        public static Guid RootId => Guid.Parse (Constant.UserRootId);
        public static Guid AdminId => Guid.Parse (Constant.UserAdminId);
        public static Guid JohnId = Guid.Parse (Constant.UserJohnId);
        public static Guid AliceId = Guid.Parse (Constant.UserAliceId);
    }

    static class UserRoleHelper {
        public static Guid RootRoleId => Guid.Parse (Constant.UserRoleRootId);
        public static Guid AdminRoleId => Guid.Parse (Constant.UserRoleAdminId);
        public static Guid SellerRoleId => Guid.Parse (Constant.UserRoleSellerId);
    }

    static class ProjectHelper {
        public static Guid FinTechProjectOneId => Guid.Parse (Constant.ProjectFinTechId);

        public static Guid ErpProjectOneId => Guid.Parse (Constant.ProjectErpId);
        public static Guid ErpProjectTwoId => Guid.Parse (Constant.ProjectErp2Id);
    }
}
