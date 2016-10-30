using System;
using System.Text.RegularExpressions;

namespace Validation
{
    public class Validation
    {
        public static bool checkEmail(String Email)
        {
            bool Isvalid = false;
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(Email))
            {
                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool checkPhoneNo(String phone)
        {
            bool Isvalid = false;
            Regex r = new Regex(@"^(03)([0-9]{9})$");
            if (r.IsMatch(phone))
            {
                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool checkname(String name)
        {
            bool Isvalid = false;
            Regex r = new Regex(@"^[A-Za-z.'\-\p{L}\p{Zs}\p{Lu}\p{Ll}\']+$");
            if (r.IsMatch(name))
            {
                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool checkUOB(String UoB)
        {
            bool Isvalid = false;
            Regex r = new Regex(@"^([0-9]{8})$");
            if (r.IsMatch(UoB))
            {
                Isvalid = true;
            }
            return Isvalid;
        }

    }

}


