using System;
namespace WeGameSdk.Api
{
    public class SubmitInfo
    {
        /**
         * 游戏上报日志类型
         */
        public static string SUBMIT_TYPE = "sub_type";

        public static string SUBMIT_TYPE_USER_CREATE = "createRole";
        public static string SUBMIT_TYPE_LEVEL_UP = "levelUp";
        public static string SUBMIT_TYPE_LOGIN_GAME = "enterServer";
        public static string SUBMIT_TYPE_EXIT_GAME = "exitServer";

        /**
         * 游戏用于标识角色的id
         */
        public static string GAME_ROLE_ID = "role_id";

        /**
         * 游戏用于标识角色的名称
         */
        public static string GAME_ROLE_NAME = "role_name";

        /**
         * 当前登录玩家的职业ID
         */
        public static string GAME_PROFESSION_ID = "profession_id";

        /**
         * 当前登录玩家的职业名称
         */
        public static string GAME_PROFESSION = "profession";

        /**
         * 当前登录玩家的性别
         */
        public static string GAME_GENDER = "gender";

        public static string GANDER_TYPE_MALE = "male";
        public static string GANDER_TYPE_FEMALE = "female";
        public static string GANDER_TYPE_OTHER = "other";

        /**
         * 战斗力数值
         */
        public static string GAME_POWER = "power";

        /**
         * 当前用户 VIP 等级
         */
        public static string GAME_VIP = "vip";

        /**
         * 游戏角色等级
         */
        public static string GAME_GRADE = "grade";

        /**
         * 游戏角色所在的服务器id
         */
        public static string GAME_SERVICE_ID = "service_id";

        /**
         * 游戏角色所在的服务器名称
         */
        public static string GAME_SERVICE_NAME = "service_id_name";


        /**
         * 游戏公会:玩家在游戏中所属公会,如果没有加入公会可不设置此属性
         */
        public static string GAME_SOCIATY = "game_sociaty";

        /**
         * 游戏公会id
         */
        public static string GAME_SOCIATY_ID = "game_sociaty_id";
    }
}
