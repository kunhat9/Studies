﻿using System;

namespace WebAdmin.Helpers
{
    public static class StringHelper
    {
        public static string Reverse(string str)
        {
            char[] array = str.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }
    }
}