using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Logic
    {
        public static string getS() {
            // вот так хэшировать
            string hashed = SecurePasswordHasher.Hash("123123");
            // а так проверить, что правильный пароль
            bool isCorrect = SecurePasswordHasher.Verify("123123", hashed);
            return "";
        }
    }
}
